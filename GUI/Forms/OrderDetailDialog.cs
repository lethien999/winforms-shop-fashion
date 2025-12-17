using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.DTO;
using System.Drawing;

namespace WinFormsFashionShop.Presentation.Forms
{
    /// <summary>
    /// Dialog for viewing order details.
    /// </summary>
    public partial class OrderDetailDialog : Form
    {
        private readonly OrderDTO _order;

        public OrderDetailDialog(OrderDTO order)
        {
            _order = order ?? throw new ArgumentNullException(nameof(order));
            InitializeComponent();
            InitializeControls();
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

            // Wire up event handlers
            btnClose.Click += (s, e) => Close();
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
