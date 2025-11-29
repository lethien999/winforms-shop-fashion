using System;
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
        private DataGridView? gridOrders;
        private TextBox? txtSearch;
        private DateTimePicker? dtpFrom, dtpTo;
        private Button? btnSearch, btnRefresh, btnViewDetail, btnCancelOrder, btnPrint;
        private Label? lblSearch, lblFrom, lblTo;

        public OrderManagementForm(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            InitializeComponent();
            InitializeControls();
            LoadOrders();
        }

        private void InitializeComponent()
        {
            Text = "Quản lý Đơn hàng";
            Width = 1200;
            Height = 700;
            StartPosition = FormStartPosition.CenterParent;
        }

        /// <summary>
        /// Initializes all UI controls.
        /// Single responsibility: only sets up UI controls.
        /// </summary>
        private void InitializeControls()
        {
            // Search section
            lblSearch = new Label { Text = "Tìm kiếm (Mã đơn):", Left = 10, Top = 10, Width = 120 };
            txtSearch = new TextBox { Left = 140, Top = 10, Width = 200 };
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) LoadOrders(); };

            lblFrom = new Label { Text = "Từ ngày:", Left = 360, Top = 10, Width = 70 };
            dtpFrom = new DateTimePicker { Left = 440, Top = 10, Width = 120, Format = DateTimePickerFormat.Short };

            lblTo = new Label { Text = "Đến ngày:", Left = 580, Top = 10, Width = 70 };
            dtpTo = new DateTimePicker { Left = 660, Top = 10, Width = 120, Format = DateTimePickerFormat.Short };
            dtpTo.Value = DateTime.Now;
            dtpFrom.Value = DateTime.Now.AddDays(-30);

            btnSearch = new Button { Text = "Tìm kiếm", Left = 800, Top = 10, Width = 100 };
            btnSearch.Click += (s, e) => LoadOrders();

            btnRefresh = new Button { Text = "Làm mới", Left = 910, Top = 10, Width = 100 };
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); dtpFrom.Value = DateTime.Now.AddDays(-30); dtpTo.Value = DateTime.Now; LoadOrders(); };

            // Grid
            gridOrders = new DataGridView
            {
                Left = 10,
                Top = 50,
                Width = 1160,
                Height = 550,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false
            };
            SetupGridColumns();

            // Buttons
            btnViewDetail = new Button { Text = "Xem chi tiết", Left = 10, Top = 610, Width = 120 };
            btnViewDetail.Click += (s, e) => ViewOrderDetail();

            btnPrint = new Button { Text = "In hóa đơn", Left = 140, Top = 610, Width = 120 };
            btnPrint.Click += (s, e) => PrintOrder();

            btnCancelOrder = new Button { Text = "Hủy đơn hàng", Left = 270, Top = 610, Width = 120 };
            btnCancelOrder.Click += (s, e) => CancelSelectedOrder();

            // Double-click to view detail
            gridOrders.CellDoubleClick += (s, e) => ViewOrderDetail();

            Controls.AddRange(new Control[] {
                lblSearch, txtSearch, lblFrom, dtpFrom, lblTo, dtpTo,
                btnSearch, btnRefresh, gridOrders,
                btnViewDetail, btnPrint, btnCancelOrder
            });
        }

        /// <summary>
        /// Sets up grid columns for orders.
        /// Single responsibility: only configures grid columns.
        /// </summary>
        private void SetupGridColumns()
        {
            gridOrders.Columns.Clear();
            gridOrders.Columns.Add("Id", "Id");
            gridOrders.Columns["Id"].Visible = false;
            gridOrders.Columns.Add("OrderCode", "Mã đơn");
            gridOrders.Columns.Add("OrderDate", "Ngày đơn");
            gridOrders.Columns["OrderDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            gridOrders.Columns.Add("CustomerName", "Khách hàng");
            gridOrders.Columns.Add("UserName", "Nhân viên");
            gridOrders.Columns.Add("TotalAmount", "Tổng tiền");
            gridOrders.Columns["TotalAmount"].DefaultCellStyle.Format = "N0";
            gridOrders.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridOrders.Columns.Add("PaymentMethod", "PTTT");
            gridOrders.Columns.Add("Status", "Trạng thái");
            gridOrders.Columns.Add("ItemsCount", "Số SP");
        }

        /// <summary>
        /// Loads and displays orders in the grid.
        /// Single responsibility: only loads and displays data.
        /// </summary>
        private void LoadOrders()
        {
            try
            {
                var orders = _orderService.GetOrdersByDateRange(dtpFrom.Value.Date, dtpTo.Value.Date.AddDays(1)).ToList();

                // Filter by search text (OrderCode)
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var searchText = txtSearch.Text.ToLower();
                    orders = orders.Where(o => o.OrderCode.ToLower().Contains(searchText)).ToList();
                }

                gridOrders.DataSource = orders.Select(o => new
                {
                    o.Id,
                    o.OrderCode,
                    o.OrderDate,
                    CustomerName = o.CustomerName ?? "Khách lẻ",
                    UserName = o.UserName ?? "",
                    o.TotalAmount,
                    PaymentMethod = o.PaymentMethod ?? "",
                    Status = o.Status == OrderStatus.Paid ? "Đã thanh toán" : o.Status == OrderStatus.Cancelled ? "Đã hủy" : o.Status,
                    ItemsCount = o.Items?.Count ?? 0
                }).ToList();

                // Color rows based on status
                foreach (DataGridViewRow row in gridOrders.Rows)
                {
                    var status = row.Cells["Status"].Value?.ToString();
                    if (status == "Đã hủy")
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                    else if (status == "Đã thanh toán")
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Gets the selected order from grid.
        /// Single responsibility: only retrieves selected order.
        /// </summary>
        private OrderDTO? GetSelectedOrder()
        {
            if (gridOrders.SelectedRows.Count == 0)
                return null;

            var orderId = (int)gridOrders.SelectedRows[0].Cells["Id"].Value;
            return _orderService.GetOrderById(orderId);
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
                ErrorHandler.ShowWarning("Vui lòng chọn đơn hàng để xem chi tiết!");
                return;
            }

            using var dialog = new OrderDetailDialog(order);
            dialog.ShowDialog(this);
        }

        /// <summary>
        /// Prints the selected order.
        /// Single responsibility: only handles print action.
        /// </summary>
        private void PrintOrder()
        {
            var order = GetSelectedOrder();
            if (order == null)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn đơn hàng để in!");
                return;
            }

            try
            {
                PrintHelper.PrintOrder(order);
                ErrorHandler.ShowSuccess("Đã gửi lệnh in hóa đơn!");
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Cancels the selected order and restores inventory.
        /// Single responsibility: only handles cancel action.
        /// </summary>
        private void CancelSelectedOrder()
        {
            var order = GetSelectedOrder();
            if (order == null)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn đơn hàng để hủy!");
                return;
            }

            if (order.Status == OrderStatus.Cancelled)
            {
                ErrorHandler.ShowWarning("Đơn hàng này đã bị hủy!");
                return;
            }

            if (!ErrorHandler.ShowConfirmation($"Bạn có chắc chắn muốn hủy đơn hàng {order.OrderCode}?\nHệ thống sẽ hoàn lại tồn kho cho các sản phẩm trong đơn."))
                return;

            try
            {
                _orderService.CancelOrder(order.Id);
                LoadOrders();
                ErrorHandler.ShowSuccess("Đã hủy đơn hàng và hoàn lại tồn kho thành công!");
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex);
            }
        }

        // Dialog for viewing order details
        public class OrderDetailDialog : Form
        {
            private readonly OrderDTO _order;

            public OrderDetailDialog(OrderDTO order)
            {
                _order = order ?? throw new ArgumentNullException(nameof(order));
                Text = $"Chi tiết đơn hàng - {order.OrderCode}";
                Width = 700;
                Height = 600;
                StartPosition = FormStartPosition.CenterParent;
                InitializeControls();
            }

            private void InitializeControls()
            {
                var lblOrderCode = new Label { Text = $"Mã đơn: {_order.OrderCode}", Left = 10, Top = 10, Width = 300 };
                var lblDate = new Label { Text = $"Ngày đơn: {_order.OrderDate:dd/MM/yyyy HH:mm}", Left = 10, Top = 35, Width = 300 };
                var lblCustomer = new Label { Text = $"Khách hàng: {(_order.CustomerName ?? "Khách lẻ")}", Left = 10, Top = 60, Width = 300 };
                var lblStaff = new Label { Text = $"Nhân viên: {(_order.UserName ?? "")}", Left = 10, Top = 85, Width = 300 };
                var lblPayment = new Label { Text = $"Phương thức TT: {(_order.PaymentMethod ?? "")}", Left = 10, Top = 110, Width = 300 };
                var lblStatus = new Label { Text = $"Trạng thái: {(_order.Status == OrderStatus.Paid ? "Đã thanh toán" : _order.Status == OrderStatus.Cancelled ? "Đã hủy" : _order.Status)}", Left = 10, Top = 135, Width = 300 };

                var gridItems = new DataGridView
                {
                    Left = 10,
                    Top = 170,
                    Width = 660,
                    Height = 350,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false
                };

                gridItems.Columns.Add("ProductCode", "Mã SP");
                gridItems.Columns.Add("ProductName", "Tên SP");
                gridItems.Columns.Add("Quantity", "SL");
                gridItems.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                gridItems.Columns.Add("UnitPrice", "Đơn giá");
                gridItems.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";
                gridItems.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                gridItems.Columns.Add("LineTotal", "Thành tiền");
                gridItems.Columns["LineTotal"].DefaultCellStyle.Format = "N0";
                gridItems.Columns["LineTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                gridItems.DataSource = _order.Items?.Select(i => new
                {
                    ProductCode = i.ProductCode ?? "",
                    ProductName = i.ProductName ?? "",
                    i.Quantity,
                    i.UnitPrice,
                    i.LineTotal
                }).ToList();

                var lblTotal = new Label { Text = $"Tổng tiền: {_order.TotalAmount:N0} VNĐ", Left = 10, Top = 530, Width = 300, Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold) };

                var btnClose = new Button { Text = "Đóng", Left = 580, Top = 530, Width = 90, DialogResult = DialogResult.Cancel };

                Controls.AddRange(new Control[] {
                    lblOrderCode, lblDate, lblCustomer, lblStaff, lblPayment, lblStatus,
                    gridItems, lblTotal, btnClose
                });
            }
        }
    }
}

