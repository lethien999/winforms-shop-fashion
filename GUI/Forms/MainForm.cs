using System;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class MainForm : Form
    {
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IInventoryService _inventoryService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly IReportService _reportService;
        private readonly UserDTO _currentUser;

        public MainForm(
            IProductService productService, 
            ICustomerService customerService, 
            IOrderService orderService, 
            IInventoryService inventoryService,
            ICategoryService categoryService,
            IUserService userService,
            IReportService reportService,
            UserDTO currentUser)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

            InitializeComponent();
            Text = $"WinForms Fashion Shop - {_currentUser.FullName} ({_currentUser.Role})";
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            
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
            
            // Chỉ hiển thị menu Admin nếu user là Admin
            if (_currentUser.Role == UserRole.Admin)
            {
                adminMenu.DropDownItems.Add(usersMenu);
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
            var frm = new ProductManagementForm(_productService, _categoryService);
            frm.ShowDialog(this);
        }

        private void OpenCategoryManagement()
        {
            var frm = new CategoryForm(_categoryService);
            frm.ShowDialog(this);
        }

        private void OpenCustomerManagement()
        {
            var frm = new CustomerManagementForm(_customerService, _orderService);
            frm.ShowDialog(this);
        }

        private void OpenOrderManagement()
        {
            var frm = new OrderManagementForm(_orderService);
            frm.ShowDialog(this);
        }

        private void OpenCreateOrder()
        {
            var frm = new OrderForm(_orderService, _productService, _customerService, _inventoryService, _currentUser);
            frm.ShowDialog(this);
        }

        private void OpenInventoryAdjustment()
        {
            var frm = new InventoryAdjustmentForm(_inventoryService, _productService);
            frm.ShowDialog(this);
        }

        private void OpenReports()
        {
            var frm = new ReportForm(_reportService);
            frm.ShowDialog(this);
        }

        private void OpenUserManagement()
        {
            var frm = new UserManagementForm(_userService);
            frm.ShowDialog(this);
        }
    }
}
