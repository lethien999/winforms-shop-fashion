using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.DTO;
using System.Drawing;
using WinFormsFashionShop.Business.Composition;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.Presentation.Helpers;
using WinFormsFashionShop.Presentation.Services;

namespace WinFormsFashionShop.Presentation.Forms
{
    /// <summary>
    /// Dialog for viewing order details.
    /// </summary>
    public partial class OrderDetailDialog : Form
    {
        private readonly OrderDTO _order;
        private readonly IOrderService _orderService;
        private readonly ErrorHandlerService _errorHandler;
        private readonly PaymentApiClientWithRetry _apiClient;

        public OrderDetailDialog(OrderDTO order)
        {
            _order = order ?? throw new ArgumentNullException(nameof(order));
            var services = ServicesComposition.Create();
            _orderService = services.OrderService;
            _errorHandler = new ErrorHandlerService();
            _apiClient = new PaymentApiClientWithRetry(ApiConfig.BaseUrl);
            InitializeComponent();
            
            // Assign event handlers to buttons (from Designer)
            btnPayVietQR.Click += BtnPayVietQR_Click;
            btnCheckPayment.Click += BtnCheckPayment_Click;
            btnCancelOrder.Click += BtnCancelOrder_Click;
            
            InitializeControls();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _apiClient?.Dispose();
            base.OnFormClosing(e);
        }

        private void InitializeControls()
        {
            Text = $"Chi tiết đơn hàng - {_order.OrderCode}";

            // Update info labels
            lblOrderCode.Text = $"Mã đơn: {_order.OrderCode}";
            lblDate.Text = $"Ngày đơn: {_order.OrderDate:dd/MM/yyyy HH:mm}";
            lblCustomer.Text = $"Khách hàng: {(_order.CustomerName ?? "Khách lẻ")}";
            lblStaff.Text = $"Nhân viên: {(_order.UserName ?? "")}";
            lblPayment.Text = $"Phương thức TT: {(_order.PaymentMethod ?? "")}";
            
            // Status label
            string statusText = _order.Status == OrderStatus.Paid ? "Đã thanh toán" : 
                               _order.Status == OrderStatus.Cancelled ? "Đã hủy" : 
                               _order.Status.ToString();
            lblStatus.Text = $"Trạng thái: {statusText}";
            lblStatus.ForeColor = _order.Status == OrderStatus.Paid ? Color.Green : 
                                 _order.Status == OrderStatus.Cancelled ? Color.Red : 
                                 Color.Orange;

            // Show bank transfer info if available
            if (_order.PaymentMethod == PaymentMethod.Transfer && 
                !string.IsNullOrWhiteSpace(_order.Notes) && 
                _order.Notes.StartsWith("CHUYỂN KHOẢN:"))
            {
                lblBankTransferInfo.Text = $"Thông tin CK:\n{_order.Notes}";
                lblBankTransferInfo.Visible = true;
                pnlInfo.Height = 240; // Increase height to fit bank transfer info
            }
            else
            {
                lblBankTransferInfo.Visible = false;
            }

            // Setup grid columns
            SetupGridColumns();

            // Populate grid
            if (_order.Items != null)
            {
                gridItems.DataSource = _order.Items.Select(i => new
                {
                    ProductCode = i.ProductCode ?? "",
                    ProductName = i.ProductName ?? "",
                    i.Quantity,
                    i.UnitPrice,
                    i.LineTotal
                }).ToList();
            }

            // Update total label
            lblTotal.Text = $"Tổng tiền: {_order.TotalAmount:N0} VNĐ";

            // Setup action buttons for Pending orders
            SetupPendingOrderActions();

            // Setup print button for Paid orders
            SetupPrintButton();

            // Wire up event handlers
            btnClose.Click += (s, e) => Close();
            btnPrint.Click += BtnPrint_Click;
        }

        /// <summary>
        /// Setup action buttons for pending orders
        /// </summary>
        private void SetupPendingOrderActions()
        {
            // Only show actions if order is Pending
            if (_order.Status != OrderStatus.Pending)
            {
                pnlActions.Visible = false;
                btnPayVietQR.Visible = false;
                btnCheckPayment.Visible = false;
                btnCancelOrder.Visible = false;
                return;
            }

            // Show action panel
            pnlActions.Visible = true;

            // Setup buttons based on payment method
            if (_order.PaymentMethod == PaymentMethod.VietQR)
            {
                // VietQR payment: Show "Thanh toán VietQR" and "Kiểm tra thanh toán"
                btnPayVietQR.Visible = true;
                btnCheckPayment.Visible = true;
            }
            else
            {
                btnPayVietQR.Visible = false;
                btnCheckPayment.Visible = false;
            }

            // Cancel button always visible for pending orders
            btnCancelOrder.Visible = true;
        }

        /// <summary>
        /// Setup print button for paid orders
        /// </summary>
        private void SetupPrintButton()
        {
            // Only show print button if order is Paid
            if (_order.Status == OrderStatus.Paid)
            {
                btnPrint.Visible = true;
            }
            else
            {
                btnPrint.Visible = false;
            }
        }

        /// <summary>
        /// Handle VietQR payment button click
        /// </summary>
        private void BtnPayVietQR_Click(object? sender, EventArgs e)
        {
            try
            {
                // Reload order to get latest status
                var currentOrder = _orderService.GetOrderById(_order.Id);
                if (currentOrder == null)
                {
                    _errorHandler.ShowError("Không tìm thấy đơn hàng!");
                    return;
                }

                // Check if already paid
                if (currentOrder.Status == OrderStatus.Paid)
                {
                    _errorHandler.ShowInfo("Đơn hàng đã được thanh toán!");
                    // Refresh dialog
                    DialogResult = DialogResult.OK;
                    return;
                }

                // Process VietQR payment
                var orderDescription = currentOrder.OrderCode.Length > 23
                    ? $"DH {currentOrder.OrderCode.Substring(0, 21)}"
                    : $"DH {currentOrder.OrderCode}";

                using var qrPaymentDialog = new QRCodePaymentDialog(currentOrder.Id, currentOrder.TotalAmount, orderDescription);
                if (qrPaymentDialog.ShowDialog(this) == DialogResult.OK && qrPaymentDialog.IsPaymentConfirmed)
                {
                    _errorHandler.ShowSuccess("Thanh toán thành công!");
                    // Refresh and close
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Lỗi khi xử lý thanh toán: {ex.Message}");
            }
        }

        /// <summary>
        /// Handle check payment button click
        /// CRITICAL: Sử dụng RecheckPaymentAsync để đảm bảo workflow đúng:
        /// - KHÔNG tạo payment link mới nếu đã có PayOSOrderCode
        /// - Chỉ check status từ PayOS và update database nếu cần
        /// </summary>
        private async void BtnCheckPayment_Click(object? sender, EventArgs e)
        {
            Button? btn = sender as Button;
            try
            {
                if (btn != null)
                {
                    btn.Enabled = false;
                    btn.Text = "⏳ Đang kiểm tra...";
                }

                // CRITICAL: Sử dụng RecheckPaymentAsync thay vì GetPaymentStatusAsync
                // RecheckPaymentAsync đảm bảo KHÔNG tạo payment link mới, chỉ check status từ PayOS
                var statusResponse = await _apiClient.RecheckPaymentAsync(_order.Id);

                if (!statusResponse.Success || statusResponse.Data == null)
                {
                    var errorMsg = statusResponse.Message ?? "Lỗi không xác định";
                    
                    // Log PayOS Order Code nếu có để debug
                    if (statusResponse.Data != null && statusResponse.Data.PayOSOrderCode.HasValue)
                    {
                        errorMsg += $"\n\nPayOS Order Code: {statusResponse.Data.PayOSOrderCode.Value}";
                        errorMsg += $"\nNếu đã thanh toán trên web, có thể PayOS Order Code trong database không khớp.";
                        errorMsg += $"\nVui lòng thử lại sau vài giây hoặc kiểm tra lại.";
                    }
                    
                    _errorHandler.ShowWarning(errorMsg);
                    return;
                }

                var status = statusResponse.Data.Status?.ToUpper() ?? "";
                var payOSOrderCode = statusResponse.Data.PayOSOrderCode;

                // Reload order from database to get latest status
                var updatedOrder = _orderService.GetOrderById(_order.Id);
                if (updatedOrder == null)
                {
                    _errorHandler.ShowError("Không tìm thấy đơn hàng!");
                    return;
                }

                if (status == "PAID" || updatedOrder.Status == OrderStatus.Paid)
                {
                    _errorHandler.ShowSuccess("Đơn hàng đã được thanh toán thành công!");
                    
                    // Update UI to reflect paid status
                    RefreshOrderDisplay(updatedOrder);
                    
                    // Hide action buttons since order is now paid
                    pnlActions.Visible = false;
                    
                    // Show print button since order is now paid
                    btnPrint.Visible = true;
                    
                    // Close dialog after a short delay
                    var closeTimer = new System.Windows.Forms.Timer { Interval = 2000 };
                    closeTimer.Tick += (s, e) =>
                    {
                        closeTimer.Stop();
                        closeTimer.Dispose();
                        DialogResult = DialogResult.OK;
                        Close();
                    };
                    closeTimer.Start();
                }
                else
                {
                    var infoMsg = "Đơn hàng chưa được thanh toán.";
                    if (payOSOrderCode.HasValue)
                    {
                        infoMsg += $"\n\nPayOS Order Code: {payOSOrderCode.Value}";
                        infoMsg += $"\nStatus từ PayOS: {status}";
                    }
                    infoMsg += "\n\nNếu bạn đã thanh toán trên web, vui lòng đợi vài giây và thử lại.";
                    _errorHandler.ShowInfo(infoMsg);
                }
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Lỗi khi kiểm tra thanh toán: {ex.Message}");
            }
            finally
            {
                if (btn != null)
                {
                    btn.Enabled = true;
                    btn.Text = "🔄 Kiểm tra thanh toán";
                }
            }
        }

        /// <summary>
        /// Refresh order display with updated order data
        /// </summary>
        private void RefreshOrderDisplay(OrderDTO order)
        {
            // Update status label
            string statusText = order.Status == OrderStatus.Paid ? "Đã thanh toán" :
                               order.Status == OrderStatus.Cancelled ? "Đã hủy" :
                               order.Status.ToString();
            lblStatus.Text = $"Trạng thái: {statusText}";
            lblStatus.ForeColor = order.Status == OrderStatus.Paid ? Color.Green :
                                 order.Status == OrderStatus.Cancelled ? Color.Red :
                                 Color.Orange;
        }

        /// <summary>
        /// Handle cancel order button click
        /// </summary>
        private void BtnCancelOrder_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!ErrorHandler.ShowConfirmation(
                    $"Bạn có chắc chắn muốn hủy đơn hàng {_order.OrderCode}?\n\n" +
                    "Hành động này sẽ:\n" +
                    "- Hủy đơn hàng\n" +
                    "- Hoàn trả số lượng tồn kho\n\n" +
                    "Hành động này không thể hoàn tác!",
                    "Xác nhận hủy đơn hàng"))
                {
                    return;
                }

                _orderService.CancelOrder(_order.Id);
                _errorHandler.ShowSuccess("Đã hủy đơn hàng thành công!");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Lỗi khi hủy đơn hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// Handle print button click - opens invoice preview dialog
        /// </summary>
        private void BtnPrint_Click(object? sender, EventArgs e)
        {
            try
            {
                // Reload order to get latest data
                var currentOrder = _orderService.GetOrderById(_order.Id);
                if (currentOrder == null)
                {
                    _errorHandler.ShowError("Không tìm thấy đơn hàng!");
                    return;
                }

                // Show invoice preview dialog
                using var previewDialog = new InvoicePreviewDialog(currentOrder);
                previewDialog.ShowDialog(this);
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Lỗi khi in hóa đơn: {ex.Message}");
            }
        }

        private void SetupGridColumns()
        {
            gridItems.Columns.Clear();
            gridItems.AutoGenerateColumns = false; // Disable auto-generation to prevent redundant columns
            
            gridItems.Columns.Add("ProductCode", "Mã SP");
            gridItems.Columns["ProductCode"].DataPropertyName = "ProductCode";
            gridItems.Columns["ProductCode"].Width = 100;
            
            gridItems.Columns.Add("ProductName", "Tên SP");
            gridItems.Columns["ProductName"].DataPropertyName = "ProductName";
            gridItems.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            gridItems.Columns.Add("Quantity", "SL");
            gridItems.Columns["Quantity"].DataPropertyName = "Quantity";
            gridItems.Columns["Quantity"].Width = 60;
            gridItems.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            gridItems.Columns.Add("UnitPrice", "Đơn giá");
            gridItems.Columns["UnitPrice"].DataPropertyName = "UnitPrice";
            gridItems.Columns["UnitPrice"].Width = 120;
            gridItems.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";
            gridItems.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            gridItems.Columns.Add("LineTotal", "Thành tiền");
            gridItems.Columns["LineTotal"].DataPropertyName = "LineTotal";
            gridItems.Columns["LineTotal"].Width = 120;
            gridItems.Columns["LineTotal"].DefaultCellStyle.Format = "N0";
            gridItems.Columns["LineTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
    }
}
