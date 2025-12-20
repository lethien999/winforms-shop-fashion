using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;
using UIThemeConstants = WinFormsFashionShop.Presentation.Helpers.UIThemeConstants;

namespace WinFormsFashionShop.Presentation.Forms
{
    /// <summary>
    /// Dialog for previewing invoice before printing.
    /// Single responsibility: only displays invoice preview.
    /// </summary>
    public partial class InvoicePreviewDialog : Form
    {
        private readonly OrderDTO _order;

        public InvoicePreviewDialog(OrderDTO order)
        {
            _order = order ?? throw new ArgumentNullException(nameof(order));
            InitializeComponent();
            InitializeControls();
            LoadInvoiceData();
        }

        /// <summary>
        /// Initializes event handlers and sets initial values.
        /// Single responsibility: only wires up event handlers and sets initial data.
        /// </summary>
        private void InitializeControls()
        {
            // Load logo if available
            var logo = LogoHelper.LoadLogo(UIThemeConstants.Spacing.LogoSizeSmall);
            if (logo != null && picLogo != null)
            {
                picLogo.Image = logo;
            }
            else if (picLogo != null)
            {
                picLogo.Visible = false;
            }

            // Set form title
            Text = $"Xem trước hóa đơn - {_order.OrderCode}";

            // Set header info (only show order code in header, title is in invoice content)
            lblHeaderTitle!.Text = $"HÓA ĐƠN - {_order.OrderCode}";
            lblOrderDate!.Text = $"Ngày: {_order.OrderDate:dd/MM/yyyy HH:mm}";

            // Wire up event handlers
            _btnPrint!.Click += (s, e) => PrintInvoice();
            _btnClose!.Click += (s, e) => Close();

            // Handle resize to center preview panel
            mainPanel!.Resize += (s, e) =>
            {
                if (_pnlPreview != null)
                {
                    _pnlPreview.Left = Math.Max(0, (mainPanel.Width - _pnlPreview.Width) / 2);
                }
            };
        }

        /// <summary>
        /// Loads invoice data into controls.
        /// Single responsibility: only populates controls with order data.
        /// </summary>
        private void LoadInvoiceData()
        {
            // Order info
            lblOrderCode!.Text = $"Mã đơn: {_order.OrderCode}";
            lblCustomer!.Text = $"Khách hàng: {(_order.CustomerName ?? "Khách lẻ")}";
            lblStaff!.Text = $"Nhân viên: {(_order.UserName ?? "")}";
            
            var statusText = _order.Status == OrderStatus.Paid ? "Đã thanh toán" :
                            _order.Status == OrderStatus.Cancelled ? "Đã hủy" : _order.Status.ToString();
            lblStatus!.Text = $"Trạng thái: {statusText}";
            lblPaymentMethod!.Text = $"Phương thức TT: {(_order.PaymentMethod ?? "")}";

            // Total amount
            lblTotalAmount!.Text = $"{_order.TotalAmount:N0} VNĐ";

            // Setup and populate DataGridView
            SetupDataGridView();
            PopulateDataGridView();
        }

        /// <summary>
        /// Sets up DataGridView columns and styling.
        /// Single responsibility: only configures DataGridView.
        /// </summary>
        private void SetupDataGridView()
        {
            gridItems!.Columns.Clear();
            gridItems.AutoGenerateColumns = false;
            gridItems.AllowUserToAddRows = false;
            gridItems.AllowUserToDeleteRows = false;
            gridItems.AllowUserToResizeColumns = false;
            gridItems.AllowUserToResizeRows = false;
            gridItems.ReadOnly = true;
            gridItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridItems.MultiSelect = false;
            gridItems.RowHeadersVisible = false;
            gridItems.BackgroundColor = Color.White;
            gridItems.BorderStyle = BorderStyle.None;
            gridItems.GridColor = Color.LightGray;
            gridItems.DefaultCellStyle.Font = new Font("Arial", 10);
            gridItems.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0);
            gridItems.RowTemplate.Height = 30;
            gridItems.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            // Column: STT
            var colSTT = new DataGridViewTextBoxColumn
            {
                Name = "STT",
                HeaderText = "STT",
                Width = 50,
                MinimumWidth = 50,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 10)
                }
            };

            // Column: Tên sản phẩm
            var colName = new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                HeaderText = "Tên sản phẩm",
                MinimumWidth = 200,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Font = new Font("Arial", 10)
                }
            };

            // Column: SL (Quantity)
            var colQty = new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "SL",
                Width = 60,
                MinimumWidth = 60,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 10)
                }
            };

            // Column: Đơn giá
            var colPrice = new DataGridViewTextBoxColumn
            {
                Name = "UnitPrice",
                HeaderText = "Đơn giá",
                Width = 120,
                MinimumWidth = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Arial", 10)
                }
            };

            // Column: Thành tiền
            var colTotal = new DataGridViewTextBoxColumn
            {
                Name = "LineTotal",
                HeaderText = "Thành tiền",
                Width = 130,
                MinimumWidth = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Arial", 10)
                }
            };

            gridItems.Columns.AddRange(new DataGridViewColumn[] { colSTT, colName, colQty, colPrice, colTotal });

            // Style header
            gridItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 130, 180);
            gridItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridItems.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
            gridItems.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridItems.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            gridItems.ColumnHeadersHeight = 35;
            gridItems.EnableHeadersVisualStyles = false;
        }

        /// <summary>
        /// Populates DataGridView with order items.
        /// Single responsibility: only fills DataGridView with data.
        /// </summary>
        private void PopulateDataGridView()
        {
            if (gridItems == null) return;

            gridItems.Rows.Clear();

            if (_order.Items == null || !_order.Items.Any())
            {
                // Show empty message if no items
                gridItems.Rows.Add("", "Không có sản phẩm", "", "", "");
                gridItems.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                gridItems.Rows[0].DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Italic);
                
                // Set minimum height
                if (pnlInvoiceContent != null && pnlInvoiceContent.RowCount > 3)
                {
                    pnlInvoiceContent.RowStyles[3].SizeType = SizeType.Absolute;
                    pnlInvoiceContent.RowStyles[3].Height = 70; // Header + 1 row
                }
                return;
            }

            int stt = 1;
            foreach (var item in _order.Items)
            {
                gridItems.Rows.Add(
                    stt.ToString(),
                    item.ProductName ?? "",
                    item.Quantity.ToString(),
                    $"{item.UnitPrice:N0} VNĐ",
                    $"{item.LineTotal:N0} VNĐ"
                );
                stt++;
            }

            // Calculate grid height based on rows (header + rows)
            int headerHeight = gridItems.ColumnHeadersHeight;
            int rowHeight = gridItems.RowTemplate.Height;
            int totalRows = gridItems.Rows.Count;
            int calculatedHeight = headerHeight + (totalRows * rowHeight) + 2; // +2 for border
            
            // Set minimum and maximum height
            int gridHeight = Math.Max(calculatedHeight, 70);
            gridHeight = Math.Min(gridHeight, 500); // Max 500px to prevent too large
            
            // Update TableLayoutPanel row height for gridItems (row index 3)
            if (pnlInvoiceContent != null && pnlInvoiceContent.RowCount > 3)
            {
                pnlInvoiceContent.RowStyles[3].SizeType = SizeType.Absolute;
                pnlInvoiceContent.RowStyles[3].Height = gridHeight;
            }

            // Set column widths and auto-size modes AFTER adding rows
            if (gridItems.Columns.Count >= 5)
            {
                gridItems.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                gridItems.Columns[0].Width = 50; // STT
                
                gridItems.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Product name fills remaining space
                gridItems.Columns[1].MinimumWidth = 200;
                
                gridItems.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                gridItems.Columns[2].Width = 60; // SL
                
                gridItems.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                gridItems.Columns[3].Width = 120; // Đơn giá
                
                gridItems.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                gridItems.Columns[4].Width = 130; // Thành tiền
            }
        }

        /// <summary>
        /// Prints the invoice.
        /// Single responsibility: only handles print action.
        /// </summary>
        private void PrintInvoice()
        {
            PrintHelper.PrintOrder(_order);
            ErrorHandler.ShowSuccess("Đã gửi lệnh in hóa đơn!");
        }
    }
}

