using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class OrderManagementForm : Form
    {
        private readonly IOrderService _orderService;
        private readonly IErrorHandler _errorHandler;
        private readonly UserDTO? _currentUser;

        public OrderManagementForm(IOrderService orderService, IErrorHandler errorHandler, UserDTO? currentUser = null)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            _currentUser = currentUser;
            InitializeComponent();
            InitializeControls();
            LoadOrders();

            // Enable keyboard shortcuts
            this.KeyPreview = true;
            this.KeyDown += OrderManagementForm_KeyDown;
        }

        private void OrderManagementForm_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    // F5: Refresh data
                    txtSearch?.Clear();
                    dtpFrom!.Value = DateTime.Now.AddDays(-30);
                    dtpTo!.Value = DateTime.Now;
                    LoadOrders();
                    e.Handled = true;
                    break;
                case Keys.Enter:
                    // Enter: View detail if grid focused
                    if (gridOrders!.Focused || !txtSearch!.Focused)
                    {
                        ViewOrderDetail();
                        e.Handled = true;
                    }
                    break;
                case Keys.Escape:
                    // Esc: Close form
                    this.Close();
                    e.Handled = true;
                    break;
                case Keys.F:
                    // Ctrl+F: Focus search
                    if (e.Control)
                    {
                        txtSearch!.Focus();
                        txtSearch.SelectAll();
                        e.Handled = true;
                    }
                    break;
                case Keys.P:
                    // Ctrl+P: Print
                    if (e.Control)
                    {
                        PrintOrder();
                        e.Handled = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// Initializes event handlers and sets initial values.
        /// Single responsibility: only wires up event handlers and sets initial data.
        /// </summary>
        private void InitializeControls()
        {
            // Load logo if available
            var logo = LogoHelper.LoadLogo(UIThemeConstants.Spacing.LogoSizeMedium);
            if (logo != null && picLogo != null)
            {
                picLogo.Image = logo;
            }
            else if (picLogo != null)
            {
                picLogo.Visible = false;
            }

            // Set default date values
            dtpTo!.Value = DateTime.Now;
            dtpFrom!.Value = DateTime.Now.AddDays(-30);

            // Wire up event handlers
            txtSearch!.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) LoadOrders(); };
            btnSearch!.Click += (s, e) => LoadOrders();
            btnRefresh!.Click += (s, e) => { txtSearch.Clear(); dtpFrom.Value = DateTime.Now.AddDays(-30); dtpTo.Value = DateTime.Now; LoadOrders(); };
            btnViewDetail!.Click += (s, e) => ViewOrderDetail();
            btnPrint!.Click += (s, e) => PrintOrder();
            btnCancelOrder!.Click += (s, e) => CancelSelectedOrder();

            // Kiểm tra quyền: Chỉ Admin mới được hủy đơn hàng
            if (_currentUser == null || _currentUser.Role != UserRole.Admin)
            {
                btnCancelOrder.Visible = false;
            }

            // Double-click to view detail
            gridOrders!.CellDoubleClick += (s, e) => ViewOrderDetail();

            // Setup grid columns and styling
            SetupGridColumns();
            
            // Setup grid styling
            if (gridOrders != null)
            {
                // Grid header styling
                gridOrders.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
                gridOrders.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
                gridOrders.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
                gridOrders.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(70, 130, 180);
                
                // Grid row styling
                gridOrders.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(200, 230, 255);
                gridOrders.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
                gridOrders.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            }
        }

        /// <summary>
        /// Sets up grid columns for orders.
        /// Single responsibility: only configures grid columns.
        /// </summary>
        private void SetupGridColumns()
        {
            if (gridOrders == null) return;
            
            // Clear existing columns
            gridOrders.Columns.Clear();
            gridOrders.AutoGenerateColumns = false; // Disable auto-generation to have full control
            
            // Add columns manually
            gridOrders.Columns.Add("Id", "Id");
            gridOrders.Columns["Id"].Visible = false;
            gridOrders.Columns["Id"].DataPropertyName = "Id";
            
            gridOrders.Columns.Add("OrderCode", "Mã đơn");
            gridOrders.Columns["OrderCode"].DataPropertyName = "OrderCode";
            
            gridOrders.Columns.Add("OrderDate", "Ngày đơn");
            gridOrders.Columns["OrderDate"].DataPropertyName = "OrderDate";
            gridOrders.Columns["OrderDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            
            gridOrders.Columns.Add("CustomerName", "Khách hàng");
            gridOrders.Columns["CustomerName"].DataPropertyName = "CustomerName";
            
            gridOrders.Columns.Add("UserName", "Nhân viên");
            gridOrders.Columns["UserName"].DataPropertyName = "UserName";
            
            gridOrders.Columns.Add("TotalAmount", "Tổng tiền");
            gridOrders.Columns["TotalAmount"].DataPropertyName = "TotalAmount";
            gridOrders.Columns["TotalAmount"].DefaultCellStyle.Format = "N0";
            gridOrders.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            gridOrders.Columns.Add("PaymentMethod", "PTTT");
            gridOrders.Columns["PaymentMethod"].DataPropertyName = "PaymentMethod";
            
            gridOrders.Columns.Add("Status", "Trạng thái");
            gridOrders.Columns["Status"].DataPropertyName = "Status";
            
            gridOrders.Columns.Add("ItemsCount", "Số SP");
            gridOrders.Columns["ItemsCount"].DataPropertyName = "ItemsCount";
        }

        /// <summary>
        /// Loads and displays orders in the grid.
        /// Single responsibility: only loads and displays data.
        /// </summary>
        private void LoadOrders()
        {
            try
            {
                if (dtpFrom == null || dtpTo == null || gridOrders == null)
                {
                    _errorHandler.ShowError("Các control chưa được khởi tạo!");
                    return;
                }

                var allOrders = _orderService.GetOrdersByDateRange(dtpFrom.Value.Date, dtpTo.Value.Date.AddDays(1));
                if (allOrders == null)
                {
                    _errorHandler.ShowWarning("Không thể tải danh sách đơn hàng. Dịch vụ trả về null.");
                    gridOrders.DataSource = null;
                    return;
                }

                var orders = allOrders.ToList();

                // Filter by search text (OrderCode)
                if (txtSearch != null && !string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var searchText = txtSearch.Text.ToLower();
                    orders = orders.Where(o => 
                        o != null && 
                        (o.OrderCode?.ToLower().Contains(searchText) ?? false)
                    ).ToList();
                }

                gridOrders.DataSource = orders.Where(o => o != null).Select(o => new
                {
                    o.Id,
                    o.OrderCode,
                    o.OrderDate,
                    CustomerName = o.CustomerName ?? "Khách lẻ",
                    UserName = o.UserName ?? "",
                    o.TotalAmount,
                    PaymentMethod = o.PaymentMethod ?? "",
                    Status = o.Status == OrderStatus.Paid ? "Đã thanh toán" : o.Status == OrderStatus.Cancelled ? "Đã hủy" : o.Status.ToString(),
                    ItemsCount = o.Items?.Count ?? 0
                }).ToList();

                // Color rows based on status
                foreach (DataGridViewRow row in gridOrders.Rows)
                {
                    var status = row.Cells["Status"]?.Value?.ToString();
                    if (status == "Đã hủy")
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                    else if (status == "Đã thanh toán")
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                }
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Lỗi khi tải danh sách đơn hàng: {ex.Message}");
                if (gridOrders != null)
                {
                    gridOrders.DataSource = null;
                }
            }
        }

        /// <summary>
        /// Gets the selected order from grid.
        /// Single responsibility: only retrieves selected order.
        /// </summary>
        private OrderDTO? GetSelectedOrder()
        {
            if (gridOrders == null || gridOrders.SelectedRows.Count == 0)
                return null;

            try
            {
                var selectedRow = gridOrders.SelectedRows[0];
                var idCell = selectedRow.Cells["Id"];
                
                if (idCell == null || idCell.Value == null || idCell.Value == DBNull.Value)
                {
                    _errorHandler.ShowWarning("Không thể lấy thông tin đơn hàng. Vui lòng thử lại!");
                    return null;
                }

                // Safely convert to int
                int orderId;
                if (idCell.Value is int intValue)
                {
                    orderId = intValue;
                }
                else if (int.TryParse(idCell.Value.ToString(), out orderId))
                {
                    // Successfully parsed
                }
                else
                {
                    _errorHandler.ShowWarning("Mã đơn hàng không hợp lệ!");
                    return null;
                }

                if (orderId <= 0)
                {
                    _errorHandler.ShowWarning("Mã đơn hàng không hợp lệ!");
                    return null;
                }

                return _orderService.GetOrderById(orderId);
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Lỗi khi lấy thông tin đơn hàng: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Opens dialog to view order details.
        /// Single responsibility: only displays order details.
        /// </summary>
        private void ViewOrderDetail()
        {
            var order = GetSelectedOrder();
            if (order == null)
            {
                _errorHandler.ShowWarning("Vui lòng chọn đơn hàng để xem chi tiết!");
                return;
            }

            using var dialog = new OrderDetailDialog(order);
            dialog.ShowDialog(this);
        }

        /// <summary>
        /// Prints the selected order with preview option.
        /// Single responsibility: only handles print action.
        /// </summary>
        private void PrintOrder()
        {
            var order = GetSelectedOrder();
            if (order == null)
            {
                _errorHandler.ShowWarning("Vui lòng chọn đơn hàng để in!");
                return;
            }

            try
            {
                // Show preview dialog first
                using var previewDialog = new InvoicePreviewDialog(order);
                previewDialog.ShowDialog(this);
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Cancels the selected order and restores inventory.
        /// Single responsibility: only handles cancel action.
        /// </summary>
        private void CancelSelectedOrder()
        {
            // Kiểm tra quyền: Chỉ Admin mới được hủy đơn hàng
            if (_currentUser == null || _currentUser.Role != UserRole.Admin)
            {
                _errorHandler.ShowWarning("Chỉ quản trị viên mới có quyền hủy đơn hàng!");
                return;
            }

            var order = GetSelectedOrder();
            if (order == null)
            {
                _errorHandler.ShowWarning("Vui lòng chọn đơn hàng để hủy!");
                return;
            }

            if (order.Status == OrderStatus.Cancelled)
            {
                _errorHandler.ShowWarning("Đơn hàng này đã bị hủy!");
                return;
            }

            if (!ErrorHandler.ShowConfirmation($"Bạn có chắc chắn muốn hủy đơn hàng {order.OrderCode}?\nHệ thống sẽ hoàn lại tồn kho cho các sản phẩm trong đơn."))
                return;

            try
            {
                _orderService.CancelOrder(order.Id);
                LoadOrders();
                    _errorHandler.ShowSuccess("Đã hủy đơn hàng và hoàn lại tồn kho thành công!");
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError(ex);
            }
        }

    }
}

