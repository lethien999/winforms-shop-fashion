using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;
using WinFormsFashionShop.Presentation.Controllers;
using UIThemeConstants = WinFormsFashionShop.Presentation.Helpers.UIThemeConstants;
using DashboardCard = WinFormsFashionShop.Presentation.Helpers.DashboardCard;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class MainForm : Form
    {
        private readonly DashboardController _dashboardController;
        private readonly IErrorHandler _errorHandler;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IInventoryService _inventoryService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly IReportService _reportService;
        private readonly UserDTO _currentUser;

        public MainForm(
            IDashboardService dashboardService,
            IDashboardCardFactory cardFactory,
            IErrorHandler errorHandler,
            IProductService productService,
            ICustomerService customerService,
            IOrderService orderService,
            IInventoryService inventoryService,
            ICategoryService categoryService,
            IUserService userService,
            IReportService reportService,
            UserDTO currentUser)
        {
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

            // Create dashboard controller
            _dashboardController = new DashboardController(
                dashboardService,
                cardFactory,
                errorHandler,
                currentUser);

            InitializeComponent();
            Text = $"WinForms Fashion Shop - {_currentUser.FullName} ({_currentUser.Role})";
            InitializeMenu();
            InitializeDashboard();
        }

        /// <summary>
        /// Initializes dashboard with statistics cards and quick actions.
        /// Single responsibility: only sets up dashboard UI.
        /// </summary>
        private void InitializeDashboard()
        {
            try
            {
                // Update user info
                lblUserInfo.Text = _dashboardController.GetUserInfoText();

                // Load statistics
                LoadDashboardStatistics();

                // Setup quick actions
                SetupQuickActions();

                // Load recent orders
                LoadRecentOrders();

                // Apply grid styling
                UIThemeConstants.ThemeHelper.ApplyGridStyle(gridRecentOrders);

                // Double-click to view order details
                gridRecentOrders.CellDoubleClick += (s, e) =>
                {
                    if (e.RowIndex < 0 || e.RowIndex >= gridRecentOrders.Rows.Count)
                        return;

                    try
                    {
                        var row = gridRecentOrders.Rows[e.RowIndex];
                        
                        // Try to get OrderDTO from row Tag first (most reliable)
                        if (row.Tag is OrderDTO order)
                        {
                            // Open order management form and focus on this order
                            OpenOrderManagementWithOrder(order);
                            return;
                        }

                        // Fallback: Try to get OrderCode from cell
                        string? orderCode = null;
                        
                        // Try by column name first
                        if (gridRecentOrders.Columns["OrderCode"] != null)
                        {
                            var cell = row.Cells["OrderCode"];
                            if (cell != null && cell.Value != null)
                            {
                                orderCode = cell.Value.ToString();
                            }
                        }
                        // If column name doesn't work, try by index (OrderCode should be first column)
                        else if (row.Cells.Count > 0)
                        {
                            var cell = row.Cells[0];
                            if (cell != null && cell.Value != null)
                            {
                                orderCode = cell.Value.ToString();
                            }
                        }

                        if (!string.IsNullOrEmpty(orderCode))
                        {
                            // Open order management form
                            OpenOrderManagement();
                        }
                    }
                    catch (Exception ex)
                    {
                        _errorHandler.ShowError($"Lỗi khi mở đơn hàng: {ex.Message}");
                    }
                };
            }
            catch
            {
                // Error handling is done by controller
            }
        }

        /// <summary>
        /// Loads and displays dashboard statistics cards.
        /// Single responsibility: only updates UI with statistics cards.
        /// </summary>
        private void LoadDashboardStatistics()
        {
            try
            {
                // Update existing cards in Designer instead of clearing
                var stats = _dashboardController.GetStatistics();

                // Update card 1: Doanh thu tháng này
                lblStat1Value.Text = $"{stats.TotalRevenue:N0} VNĐ";

                // Update card 2: Đơn hàng hôm nay
                lblStat2Value.Text = $"{stats.TodayOrdersCount} đơn";

                // Update card 3: Tổng sản phẩm
                lblStat3Value.Text = $"{stats.TotalProducts} sản phẩm";

                // Update card 4: Tổng khách hàng
                lblStat4Value.Text = $"{stats.TotalCustomers} khách";

                // Update card 5: Cảnh báo tồn kho
                lblStat5Value.Text = $"{stats.LowStockCount} sản phẩm";
            }
            catch
            {
                // Error handling is done by controller
            }
        }

        /// <summary>
        /// Sets up quick action cards.
        /// Single responsibility: only updates UI with action cards.
        /// </summary>
        private void SetupQuickActions()
        {
            // Wire up event handlers for existing cards in Designer
            EventHandler action1Handler = (s, e) => OpenCreateOrder();
            EventHandler action2Handler = (s, e) => OpenProductManagement();
            EventHandler action3Handler = (s, e) => OpenOrderManagement();
            EventHandler action4Handler = (s, e) => OpenCustomerManagement();
            EventHandler action5Handler = (s, e) => OpenInventoryAdjustment();
            EventHandler action6Handler = (s, e) => OpenReports();
            EventHandler action7Handler = (s, e) => OpenUserManagement();

            cardAction1.Click += action1Handler;
            cardAction2.Click += action2Handler;
            cardAction3.Click += action3Handler;
            cardAction4.Click += action4Handler;
            cardAction5.Click += action5Handler;
            cardAction6.Click += action6Handler;

            // Show/hide admin card based on role
            if (_currentUser.Role == UserRole.Admin)
            {
                cardAction7.Visible = true;
                cardAction7.Click += action7Handler;
            }
            else
            {
                cardAction7.Visible = false;
            }

            // Make all labels clickable and forward clicks to parent panel
            MakeCardClickable(cardAction1, action1Handler);
            MakeCardClickable(cardAction2, action2Handler);
            MakeCardClickable(cardAction3, action3Handler);
            MakeCardClickable(cardAction4, action4Handler);
            MakeCardClickable(cardAction5, action5Handler);
            MakeCardClickable(cardAction6, action6Handler);
            if (_currentUser.Role == UserRole.Admin)
            {
                MakeCardClickable(cardAction7, action7Handler);
            }
        }

        /// <summary>
        /// Makes all controls in a card clickable and forwards clicks to the card's handler.
        /// </summary>
        private void MakeCardClickable(Panel card, EventHandler handler)
        {
            card.Cursor = Cursors.Hand;
            foreach (Control ctrl in card.Controls)
            {
                ctrl.Cursor = Cursors.Hand;
                ctrl.Click += handler;
            }
        }

        /// <summary>
        /// Loads recent orders into grid.
        /// Single responsibility: only updates UI with recent orders.
        /// </summary>
        private void LoadRecentOrders()
        {
            // Configure grid columns
            _dashboardController.ConfigureRecentOrdersGrid(gridRecentOrders);

            // Load orders from controller
            var recentOrders = _dashboardController.LoadRecentOrders(10);

            // Populate grid
            _dashboardController.PopulateRecentOrdersGrid(gridRecentOrders, recentOrders);

            // Update label
            lblRecentOrders.Text = _dashboardController.GetRecentOrdersLabelText(recentOrders.Count);
        }

        private void InitializeMenu()
        {
            // Apply theme to menu strip with custom renderer
            menuStrip.BackColor = UIThemeConstants.Colors.PrimaryBlue;
            menuStrip.ForeColor = UIThemeConstants.Colors.TextLight;
            menuStrip.Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());

            // Quản lý menu
            var manageMenu = new ToolStripMenuItem("Quản lý");
            var productsMenu = new ToolStripMenuItem("Sản phẩm");
            productsMenu.Click += (s, e) => OpenProductManagement();
            var categoriesMenu = new ToolStripMenuItem("Danh mục");
            categoriesMenu.Click += (s, e) => OpenCategoryManagement();
            var customersMenu = new ToolStripMenuItem("Khách hàng");
            customersMenu.Click += (s, e) => OpenCustomerManagement();
            var ordersMenu = new ToolStripMenuItem("Đơn hàng");
            ordersMenu.Click += (s, e) => OpenOrderManagement();
            var inventoryMenu = new ToolStripMenuItem("Tồn kho");
            inventoryMenu.Click += (s, e) => OpenInventoryAdjustment();

            manageMenu.DropDownItems.Add(productsMenu);
            manageMenu.DropDownItems.Add(categoriesMenu);
            manageMenu.DropDownItems.Add(customersMenu);
            manageMenu.DropDownItems.Add(ordersMenu);
            manageMenu.DropDownItems.Add(inventoryMenu);

            // Bán hàng menu
            var salesMenu = new ToolStripMenuItem("Bán hàng");
            var createOrderMenu = new ToolStripMenuItem("Lập hóa đơn");
            createOrderMenu.Click += (s, e) => OpenCreateOrder();
            salesMenu.DropDownItems.Add(createOrderMenu);

            // Báo cáo menu
            var reportsMenu = new ToolStripMenuItem("Báo cáo");
            reportsMenu.Click += (s, e) => OpenReports();

            // Admin menu (chỉ hiển thị cho Admin)
            var adminMenu = new ToolStripMenuItem("Quản trị");
            var usersMenu = new ToolStripMenuItem("Người dùng");
            usersMenu.Click += (s, e) => OpenUserManagement();

            var payOSConfigMenu = new ToolStripMenuItem("Cấu hình PayOS");
            payOSConfigMenu.Click += (s, e) => OpenPayOSConfig();

            // Chỉ hiển thị menu Admin nếu user là Admin
            if (_currentUser.Role == UserRole.Admin)
            {
                adminMenu.DropDownItems.Add(usersMenu);
                adminMenu.DropDownItems.Add(new ToolStripSeparator());
                adminMenu.DropDownItems.Add(payOSConfigMenu);
                menuStrip.Items.Add(adminMenu);
            }

            menuStrip.Items.Add(salesMenu);
            menuStrip.Items.Add(manageMenu);
            menuStrip.Items.Add(reportsMenu);

            // Update status bar
            statusLabel.Text = $"Người dùng: {_currentUser.FullName} | Vai trò: {_currentUser.Role}";

            Controls.Add(menuStrip);
            Controls.Add(statusStrip);
            MainMenuStrip = menuStrip;
        }

        private void OpenProductManagement()
        {
            var frm = new ProductManagementForm(_productService, _categoryService, _errorHandler, _currentUser);
            frm.ShowDialog(this);
            // Refresh dashboard after closing
            LoadDashboardStatistics();
        }

        private void OpenCategoryManagement()
        {
            var frm = new CategoryForm(_categoryService, _errorHandler, _currentUser);
            frm.ShowDialog(this);
            LoadDashboardStatistics();
        }

        private void OpenCustomerManagement()
        {
            var frm = new CustomerManagementForm(_customerService, _orderService, _errorHandler, _currentUser);
            frm.ShowDialog(this);
            LoadDashboardStatistics();
        }

        private void OpenOrderManagement()
        {
            var frm = new OrderManagementForm(_orderService, _errorHandler, _currentUser);
            frm.ShowDialog(this);
            // Refresh dashboard after closing
            LoadDashboardStatistics();
            LoadRecentOrders();
        }

        /// <summary>
        /// Opens order management form and focuses on a specific order.
        /// Single responsibility: only opens form with order focus.
        /// </summary>
        private void OpenOrderManagementWithOrder(OrderDTO order)
        {
            var frm = new OrderManagementForm(_orderService, _errorHandler, _currentUser);
            // Note: OrderManagementForm may need to be updated to support focusing on a specific order
            // For now, just open the form normally
            frm.ShowDialog(this);
            LoadDashboardStatistics();
            LoadRecentOrders();
        }

        private void OpenCreateOrder()
        {
            var frm = new OrderForm(_orderService, _productService, _customerService, _inventoryService, _categoryService, _errorHandler, _currentUser);
            frm.ShowDialog(this);
            // Refresh dashboard after creating order
            LoadDashboardStatistics();
            LoadRecentOrders();
        }

        private void OpenInventoryAdjustment()
        {
            var frm = new InventoryAdjustmentForm(_inventoryService, _productService, _errorHandler);
            frm.ShowDialog(this);
            LoadDashboardStatistics();
        }

        private void OpenReports()
        {
            var frm = new ReportForm(_reportService, _errorHandler);
            frm.ShowDialog(this);
        }

        private void OpenUserManagement()
        {
            var frm = new UserManagementForm(_userService, _errorHandler);
            frm.ShowDialog(this);
        }

        /// <summary>
        /// Opens PayOS configuration form.
        /// Single responsibility: only opens PayOS config form.
        /// </summary>
        private void OpenPayOSConfig()
        {
            try
            {
                using var form = new PayOSConfigForm();
                form.ShowDialog(this);
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void lblStat1Title_Click(object sender, EventArgs e)
        {

        }
    }
}
