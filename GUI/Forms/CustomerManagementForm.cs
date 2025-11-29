using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;

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
                    c.Address,
                    c.IsActive,
                    TrạngThái = c.IsActive ? "Hoạt động" : "Ngừng"
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Lỗi tải lịch sử mua hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Thêm khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi thêm khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void EditSelectedCustomer()
        {
            if (gridCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var id = (int)gridCustomers.SelectedRows[0].Cells["Id"].Value;
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                MessageBox.Show("Không tìm thấy khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var dialog = new CustomerEditDialog(customer);
            if (dialog.ShowDialog() == DialogResult.OK && dialog.UpdateCustomerDTO != null)
            {
                try
                {
                    _customerService.UpdateCustomer(dialog.UpdateCustomerDTO);
                    LoadCustomers();
                    MessageBox.Show("Cập nhật khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi cập nhật khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DeleteSelectedCustomer()
        {
            if (gridCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var id = (int)gridCustomers.SelectedRows[0].Cells["Id"].Value;
            var customerName = gridCustomers.SelectedRows[0].Cells["CustomerName"].Value?.ToString() ?? "";

            if (MessageBox.Show($"Bạn có chắc muốn xóa khách hàng '{customerName}'?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _customerService.DeleteCustomer(id);
                    LoadCustomers();
                    MessageBox.Show("Xóa khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    // Dialog for editing customer
    public class CustomerEditDialog : Form
    {
        private TextBox? _txtName, _txtPhone, _txtEmail, _txtAddress;
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
            Height = 300;
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

            var lblAddress = new Label { Text = "Địa chỉ:", Left = 10, Top = 140, Width = 120 };
            _txtAddress = new TextBox { Left = 140, Top = 140, Width = 250, Text = _existingCustomer?.Address ?? "" };

            _chkIsActive = new CheckBox { Text = "Hoạt động", Left = 140, Top = 180, Checked = _existingCustomer?.IsActive ?? true };

            _btnOK = new Button { Text = "OK", Left = 140, Top = 220, Width = 100, DialogResult = DialogResult.OK };
            _btnCancel = new Button { Text = "Hủy", Left = 250, Top = 220, Width = 100, DialogResult = DialogResult.Cancel };

            _btnOK.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(_txtName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        Email = string.IsNullOrWhiteSpace(_txtEmail?.Text) ? null : _txtEmail.Text.Trim(),
                        Address = string.IsNullOrWhiteSpace(_txtAddress?.Text) ? null : _txtAddress.Text.Trim()
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
                        Address = string.IsNullOrWhiteSpace(_txtAddress?.Text) ? null : _txtAddress.Text.Trim(),
                        IsActive = _chkIsActive?.Checked ?? true
                    };
                }
            };

            Controls.AddRange(new Control[] { 
                lblName, _txtName, lblPhone, _txtPhone, lblEmail, _txtEmail,
                lblAddress, _txtAddress, _chkIsActive, _btnOK, _btnCancel 
            });
        }
    }
}
