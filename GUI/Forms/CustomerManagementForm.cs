using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;
using UIThemeConstants = WinFormsFashionShop.Presentation.Helpers.UIThemeConstants;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class CustomerManagementForm : Form
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IErrorHandler _errorHandler;
        private readonly UserDTO? _currentUser;

        public CustomerManagementForm(ICustomerService customerService, IOrderService orderService, IErrorHandler errorHandler, UserDTO? currentUser = null)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            _currentUser = currentUser;
            InitializeComponent();
            InitializeControls();
            LoadCustomers();
        }

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

            // Wire up event handlers
            btnSearch.Click += (s, e) => LoadCustomers();
            gridCustomers.SelectionChanged += (s, e) => LoadCustomerOrders();
            gridCustomers.CellDoubleClick += (s, e) => EditSelectedCustomer();
            btnAdd.Click += (s, e) => AddCustomer();
            btnEdit.Click += (s, e) => EditSelectedCustomer();
            btnDelete.Click += (s, e) => DeleteSelectedCustomer();
            btnViewHistory.Click += (s, e) => LoadCustomerOrders();
            btnRefresh.Click += (s, e) => LoadCustomers();

            // Kiểm tra quyền: Chỉ Admin mới được xóa khách hàng
            if (_currentUser == null || _currentUser.Role != UserRole.Admin)
            {
                btnDelete.Visible = false;
            }
        }

        private void LoadCustomers()
        {
            try
            {
                if (gridCustomers == null)
                {
                    _errorHandler.ShowError("Grid khách hàng chưa được khởi tạo!");
                    return;
                }

                var allCustomers = _customerService.GetAllCustomers();
                if (allCustomers == null)
                {
                    _errorHandler.ShowWarning("Không thể tải danh sách khách hàng. Dịch vụ trả về null.");
                    gridCustomers.DataSource = null;
                    return;
                }

                var customers = allCustomers.ToList();
                
                if (txtSearch != null && !string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var searchText = txtSearch.Text?.ToLower() ?? string.Empty;
                    customers = customers.Where(c => 
                        c != null &&
                        ((c.CustomerName?.ToLower().Contains(searchText) ?? false) ||
                        (c.Phone?.ToLower().Contains(searchText) ?? false) ||
                        (c.Email?.ToLower().Contains(searchText) ?? false))
                    ).ToList();
                }

                if (customers == null)
                {
                    gridCustomers.DataSource = new List<object>();
                    return;
                }

                var customerList = customers.Where(c => c != null).Select(c => new
                {
                    c.Id,
                    c.CustomerName,
                    c.Phone,
                    c.Email,
                    c.IsActive,
                    TrạngThái = c.IsActive ? "Hoạt động" : "Ngừng"
                }).ToList();
                gridCustomers.DataSource = customerList;
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Lỗi khi tải danh sách khách hàng: {ex.Message}");
                if (gridCustomers != null)
                {
                    gridCustomers.DataSource = null;
                }
            }
        }

        private void LoadCustomerOrders()
        {
            if (gridCustomers == null || gridOrders == null)
            {
                return;
            }

            if (gridCustomers.SelectedRows.Count == 0)
            {
                gridOrders.DataSource = null;
                return;
            }

            try
            {
                var selectedRow = gridCustomers.SelectedRows[0];
                var idCell = selectedRow.Cells["Id"];
                
                if (idCell == null || idCell.Value == null)
                {
                    gridOrders.DataSource = null;
                    return;
                }

                int customerId;
                if (idCell.Value is int intValue)
                {
                    customerId = intValue;
                }
                else if (!int.TryParse(idCell.Value.ToString(), out customerId))
                {
                    _errorHandler.ShowWarning("Không thể lấy ID khách hàng.");
                    gridOrders.DataSource = null;
                    return;
                }
                
                var allOrders = _orderService.GetOrdersByCustomerId(customerId);
                if (allOrders == null)
                {
                    gridOrders.DataSource = null;
                    return;
                }

                var orders = allOrders.ToList();

                gridOrders.DataSource = orders?.Where(o => o != null).Select(o => new
                {
                    o.Id,
                    o.OrderCode,
                    o.OrderDate,
                    o.TotalAmount,
                    o.Status,
                    SốLượngSP = o.Items?.Count ?? 0
                }).ToList();
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Lỗi khi tải đơn hàng của khách hàng: {ex.Message}");
                if (gridOrders != null)
                    gridOrders.DataSource = null;
            }
        }

        private void AddCustomer()
        {
            using var dialog = new CustomerEditDialog(null);
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.CreateCustomerDTO != null)
            {
                try
                {
                    _customerService.CreateCustomer(dialog.CreateCustomerDTO);
                    LoadCustomers();
                    _errorHandler.ShowSuccess("Thêm khách hàng thành công!");
                }
                catch (Exception ex)
                {
                    _errorHandler.ShowError(ex);
                }
            }
        }

        private void EditSelectedCustomer()
        {
            if (gridCustomers.SelectedRows.Count == 0)
            {
                _errorHandler.ShowWarning("Vui lòng chọn khách hàng cần sửa!");
                return;
            }

            var id = (int)gridCustomers.SelectedRows[0].Cells["Id"].Value;
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                _errorHandler.ShowError("Không tìm thấy khách hàng!");
                return;
            }

            using var dialog = new CustomerEditDialog(customer);
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.UpdateCustomerDTO != null)
            {
                try
                {
                    _customerService.UpdateCustomer(dialog.UpdateCustomerDTO);
                    LoadCustomers();
                    _errorHandler.ShowSuccess("Cập nhật khách hàng thành công!");
                }
                catch (Exception ex)
                {
                    _errorHandler.ShowError(ex);
                }
            }
        }

        private void DeleteSelectedCustomer()
        {
            // Kiểm tra quyền: Chỉ Admin mới được xóa khách hàng
            if (_currentUser == null || _currentUser.Role != UserRole.Admin)
            {
                _errorHandler.ShowWarning("Chỉ quản trị viên mới có quyền xóa khách hàng!");
                return;
            }

            if (gridCustomers.SelectedRows.Count == 0)
            {
                _errorHandler.ShowWarning("Vui lòng chọn khách hàng cần xóa!");
                return;
            }

            var id = (int)gridCustomers.SelectedRows[0].Cells["Id"].Value;
            var customerName = gridCustomers.SelectedRows[0].Cells["CustomerName"].Value?.ToString() ?? "";

            if (_errorHandler.ShowConfirmation($"Bạn có chắc muốn xóa khách hàng '{customerName}'?"))
            {
                try
                {
                    _customerService.DeleteCustomer(id);
                    LoadCustomers();
                    _errorHandler.ShowSuccess("Xóa khách hàng thành công!");
                }
                catch (Exception ex)
                {
                    _errorHandler.ShowError(ex);
                }
            }
        }
    }

}
