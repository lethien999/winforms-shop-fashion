using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class CustomerManagementForm : Form
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;

        public CustomerManagementForm(ICustomerService customerService, IOrderService orderService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            InitializeComponent();
            InitializeControls();
            LoadCustomers();
        }

        private void InitializeControls()
        {
            // Wire up event handlers
            btnSearch.Click += (s, e) => LoadCustomers();
            gridCustomers.SelectionChanged += (s, e) => LoadCustomerOrders();
            gridCustomers.CellDoubleClick += (s, e) => EditSelectedCustomer();
            btnAdd.Click += (s, e) => AddCustomer();
            btnEdit.Click += (s, e) => EditSelectedCustomer();
            btnDelete.Click += (s, e) => DeleteSelectedCustomer();
            btnViewHistory.Click += (s, e) => LoadCustomerOrders();
            btnRefresh.Click += (s, e) => LoadCustomers();
        }

        private void LoadCustomers()
        {
            try
            {
                var customers = _customerService.GetAllCustomers().ToList();
                
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var searchText = txtSearch.Text.ToLower();
                    customers = customers.Where(c => 
                        c.CustomerName.ToLower().Contains(searchText) ||
                        (c.Phone != null && c.Phone.ToLower().Contains(searchText)) ||
                        (c.Email != null && c.Email.ToLower().Contains(searchText))
                    ).ToList();
                }

                gridCustomers.DataSource = customers.Select(c => new
                {
                    c.Id,
                    c.CustomerName,
                    c.Phone,
                    c.Email,
                    c.IsActive,
                    TrạngThái = c.IsActive ? "Hoạt động" : "Ngừng"
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex);
            }
        }

        private void LoadCustomerOrders()
        {
            if (gridCustomers.SelectedRows.Count == 0)
            {
                gridOrders.DataSource = null;
                return;
            }

            try
            {
                var customerId = (int)gridCustomers.SelectedRows[0].Cells["Id"].Value;
                var orders = _orderService.GetOrdersByCustomerId(customerId).ToList();

                gridOrders.DataSource = orders.Select(o => new
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
                ErrorHandler.ShowError(ex);
            }
        }

        private void AddCustomer()
        {
            using var dialog = new CustomerEditDialog(null);
            if (dialog.ShowDialog() == DialogResult.OK && dialog.CreateCustomerDTO != null)
            {
                try
                {
                    _customerService.CreateCustomer(dialog.CreateCustomerDTO);
                    LoadCustomers();
                    ErrorHandler.ShowSuccess("Thêm khách hàng thành công!");
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError(ex);
                }
            }
        }

        private void EditSelectedCustomer()
        {
            if (gridCustomers.SelectedRows.Count == 0)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn khách hàng cần sửa!");
                return;
            }

            var id = (int)gridCustomers.SelectedRows[0].Cells["Id"].Value;
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                ErrorHandler.ShowError("Không tìm thấy khách hàng!");
                return;
            }

            using var dialog = new CustomerEditDialog(customer);
            if (dialog.ShowDialog() == DialogResult.OK && dialog.UpdateCustomerDTO != null)
            {
                try
                {
                    _customerService.UpdateCustomer(dialog.UpdateCustomerDTO);
                    LoadCustomers();
                    ErrorHandler.ShowSuccess("Cập nhật khách hàng thành công!");
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError(ex);
                }
            }
        }

        private void DeleteSelectedCustomer()
        {
            if (gridCustomers.SelectedRows.Count == 0)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn khách hàng cần xóa!");
                return;
            }

            var id = (int)gridCustomers.SelectedRows[0].Cells["Id"].Value;
            var customerName = gridCustomers.SelectedRows[0].Cells["CustomerName"].Value?.ToString() ?? "";

            if (ErrorHandler.ShowConfirmation($"Bạn có chắc muốn xóa khách hàng '{customerName}'?"))
            {
                try
                {
                    _customerService.DeleteCustomer(id);
                    LoadCustomers();
                    ErrorHandler.ShowSuccess("Xóa khách hàng thành công!");
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError(ex);
                }
            }
        }
    }

    // Dialog for editing customer
    public class CustomerEditDialog : Form
    {
        private TextBox? _txtName, _txtPhone, _txtEmail;
        private CheckBox? _chkIsActive;
        private Button? _btnOK, _btnCancel;
        public CreateCustomerDTO? CreateCustomerDTO { get; private set; }
        public UpdateCustomerDTO? UpdateCustomerDTO { get; private set; }
        public object? CustomerDTO => (object?)CreateCustomerDTO ?? UpdateCustomerDTO;
        private CustomerDTO? _existingCustomer;

        public CustomerEditDialog(CustomerDTO? customer)
        {
            _existingCustomer = customer;
            Text = customer == null ? "Thêm khách hàng mới" : "Sửa khách hàng";
            Width = 450;
            Height = 250;
            StartPosition = FormStartPosition.CenterParent;
            InitializeControls();
        }

        private void InitializeControls()
        {
            var lblName = new Label { Text = "Tên khách hàng:", Left = 10, Top = 20, Width = 120 };
            _txtName = new TextBox { Left = 140, Top = 20, Width = 250, Text = _existingCustomer?.CustomerName ?? "" };

            var lblPhone = new Label { Text = "Số điện thoại:", Left = 10, Top = 60, Width = 120 };
            _txtPhone = new TextBox { Left = 140, Top = 60, Width = 250, Text = _existingCustomer?.Phone ?? "" };

            var lblEmail = new Label { Text = "Email:", Left = 10, Top = 100, Width = 120 };
            _txtEmail = new TextBox { Left = 140, Top = 100, Width = 250, Text = _existingCustomer?.Email ?? "" };

            _chkIsActive = new CheckBox { Text = "Hoạt động", Left = 140, Top = 140, Checked = _existingCustomer?.IsActive ?? true };

            _btnOK = new Button { Text = "OK", Left = 140, Top = 180, Width = 100, DialogResult = DialogResult.OK };
            _btnCancel = new Button { Text = "Hủy", Left = 250, Top = 180, Width = 100, DialogResult = DialogResult.Cancel };

            _btnOK.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(_txtName.Text))
                {
                    ErrorHandler.ShowWarning("Vui lòng nhập tên khách hàng!");
                    DialogResult = DialogResult.None;
                    return;
                }

                if (_existingCustomer == null)
                {
                    // Create new customer
                    CreateCustomerDTO = new CreateCustomerDTO
                    {
                        CustomerName = _txtName?.Text.Trim() ?? "",
                        Phone = string.IsNullOrWhiteSpace(_txtPhone?.Text) ? null : _txtPhone.Text.Trim(),
                        Email = string.IsNullOrWhiteSpace(_txtEmail?.Text) ? null : _txtEmail.Text.Trim()
                    };
                }
                else
                {
                    // Update existing customer
                    UpdateCustomerDTO = new UpdateCustomerDTO
                    {
                        Id = _existingCustomer.Id,
                        CustomerName = _txtName?.Text.Trim() ?? "",
                        Phone = string.IsNullOrWhiteSpace(_txtPhone?.Text) ? null : _txtPhone.Text.Trim(),
                        Email = string.IsNullOrWhiteSpace(_txtEmail?.Text) ? null : _txtEmail.Text.Trim(),
                        IsActive = _chkIsActive?.Checked ?? true
                    };
                }
            };

            Controls.AddRange(new Control[] { 
                lblName, _txtName, lblPhone, _txtPhone, lblEmail, _txtEmail,
                _chkIsActive, _btnOK, _btnCancel 
            });
        }
    }
}
