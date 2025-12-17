using System;
using System.Windows.Forms;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    /// <summary>
    /// Dialog for editing customer (add/edit).
    /// </summary>
    public partial class CustomerEditDialog : Form
    {
        public CreateCustomerDTO? CreateCustomerDTO { get; private set; }
        public UpdateCustomerDTO? UpdateCustomerDTO { get; private set; }
        public object? CustomerDTO => (object?)CreateCustomerDTO ?? UpdateCustomerDTO;
        private CustomerDTO? _existingCustomer;

        public CustomerEditDialog(CustomerDTO? customer)
        {
            _existingCustomer = customer;
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            Text = _existingCustomer == null ? "Thêm khách hàng mới" : "Sửa khách hàng";
            
            // Set initial values
            if (_existingCustomer != null)
            {
                txtName.Text = _existingCustomer.CustomerName ?? "";
                txtPhone.Text = _existingCustomer.Phone ?? "";
                txtEmail.Text = _existingCustomer.Email ?? "";
                chkIsActive.Checked = _existingCustomer.IsActive;
            }
            else
            {
                chkIsActive.Checked = true;
            }

            // Wire up event handlers
            btnOK.Click += BtnOK_Click;
        }

        private void BtnOK_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
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
                    CustomerName = txtName.Text.Trim(),
                    Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim(),
                    Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim()
                };
            }
            else
            {
                // Update existing customer
                UpdateCustomerDTO = new UpdateCustomerDTO
                {
                    Id = _existingCustomer.Id,
                    CustomerName = txtName.Text.Trim(),
                    Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim(),
                    Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                    IsActive = chkIsActive.Checked
                };
            }
        }
    }
}
