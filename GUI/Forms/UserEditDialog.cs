using System;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    /// <summary>
    /// Dialog for adding/editing user information.
    /// </summary>
    public partial class UserEditDialog : Form
    {
        public CreateUserDTO? CreateUserDTO { get; private set; }
        public UpdateUserDTO? UpdateUserDTO { get; private set; }
        private User? _existingUser;

        public UserEditDialog(User? user)
        {
            _existingUser = user;
            InitializeComponent();
            InitializeControls();
        }

        /// <summary>
        /// Initializes all controls in the dialog.
        /// Single responsibility: only sets up UI controls.
        /// </summary>
        private void InitializeControls()
        {
            Text = _existingUser == null ? "Thêm người dùng mới" : "Sửa thông tin người dùng";
            
            // Setup role ComboBox FIRST (before setting SelectedIndex)
            cmbRole.Items.Clear();
            cmbRole.Items.Add(UserRole.Admin);
            cmbRole.Items.Add(UserRole.Staff);
            
            // Set initial values
            if (_existingUser != null)
            {
                txtUsername.Text = _existingUser.Username ?? "";
                txtFullName.Text = _existingUser.FullName ?? "";
                txtUsername.ReadOnly = true; // Cannot change username
                
                // Set role - use SelectedItem after items are populated
                if (_existingUser.Role == UserRole.Admin)
                    cmbRole.SelectedItem = UserRole.Admin;
                else if (_existingUser.Role == UserRole.Staff)
                    cmbRole.SelectedItem = UserRole.Staff;
                else
                    cmbRole.SelectedIndex = 0; // Default to first item if role is unknown
                
                chkIsActive.Checked = _existingUser.IsActive;
                lblPassword.Text = "Mật khẩu mới (để trống nếu không đổi):";
            }
            else
            {
                // Default to Staff (index 1) for new users
                if (cmbRole.Items.Count > 1)
                {
                    cmbRole.SelectedIndex = 1; // Staff
                }
                else if (cmbRole.Items.Count > 0)
                {
                    cmbRole.SelectedIndex = 0; // Fallback to first item
                }
                
                chkIsActive.Visible = false; // Hide for new users (always active)
                lblPassword.Text = "Mật khẩu:";
            }

            // Wire up event handlers
            btnOK.Click += BtnOK_Click;
        }

        private void BtnOK_Click(object? sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                DialogResult = DialogResult.None;
                return;
            }

            if (_existingUser == null)
            {
                CreateUserDTO = new CreateUserDTO
                {
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text,
                    FullName = txtFullName.Text.Trim(),
                    Role = cmbRole.SelectedItem?.ToString() ?? UserRole.Staff
                };
            }
            else
            {
                UpdateUserDTO = new UpdateUserDTO
                {
                    Id = _existingUser.Id,
                    Username = txtUsername.Text.Trim(),
                    Password = string.IsNullOrWhiteSpace(txtPassword.Text) ? null : txtPassword.Text,
                    FullName = txtFullName.Text.Trim(),
                    Role = cmbRole.SelectedItem?.ToString() ?? UserRole.Staff,
                    IsActive = chkIsActive.Checked
                };
            }
        }

        /// <summary>
        /// Validates user input before saving.
        /// Single responsibility: only validates inputs.
        /// </summary>
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                ErrorHandler.ShowWarning("Vui lòng nhập tên đăng nhập!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                ErrorHandler.ShowWarning("Vui lòng nhập họ và tên!");
                return false;
            }

            if (_existingUser == null && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ErrorHandler.ShowWarning("Vui lòng nhập mật khẩu!");
                return false;
            }

            if (cmbRole.SelectedItem == null)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn vai trò!");
                return false;
            }

            return true;
        }
    }
}
