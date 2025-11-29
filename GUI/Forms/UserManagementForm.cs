using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.Business.Mappers;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class UserManagementForm : Form
    {
        private readonly IUserService _userService;

        public UserManagementForm(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            InitializeComponent();
            InitializeControls();
            LoadUsers();
        }

        /// <summary>
        /// Sets up event handlers for all controls.
        /// Single responsibility: only wires up event handlers.
        /// </summary>
        private void InitializeControls()
        {
            SetupFilterComboBoxes();
            WireUpEventHandlers();
        }

        /// <summary>
        /// Sets up filter ComboBoxes with default values.
        /// Single responsibility: only configures filter ComboBoxes.
        /// </summary>
        private void SetupFilterComboBoxes()
        {
            cmbRoleFilter.Items.Add("Tất cả");
            cmbRoleFilter.Items.Add(UserRole.Admin);
            cmbRoleFilter.Items.Add(UserRole.Staff);
            cmbRoleFilter.SelectedIndex = 0;

            cmbStatusFilter.Items.Add("Tất cả");
            cmbStatusFilter.Items.Add("Hoạt động");
            cmbStatusFilter.Items.Add("Ngừng");
            cmbStatusFilter.SelectedIndex = 0;
        }

        /// <summary>
        /// Wires up all event handlers.
        /// Single responsibility: only sets up event handlers.
        /// </summary>
        private void WireUpEventHandlers()
        {
            btnSearch.Click += (s, e) => LoadUsers();
            btnRefresh.Click += (s, e) => LoadUsers();
            gridUsers.CellDoubleClick += (s, e) => EditSelectedUser();
            btnAdd.Click += (s, e) => AddUser();
            btnEdit.Click += (s, e) => EditSelectedUser();
            btnDelete.Click += (s, e) => DeleteSelectedUser();
            btnChangePassword.Click += (s, e) => ChangePassword();
            btnActivate.Click += (s, e) => ToggleUserStatus();
            cmbRoleFilter.SelectedIndexChanged += (s, e) => LoadUsers();
            cmbStatusFilter.SelectedIndexChanged += (s, e) => LoadUsers();
        }

        /// <summary>
        /// Loads and displays users in the grid.
        /// Single responsibility: only loads and displays data.
        /// </summary>
        private void LoadUsers()
        {
            try
            {
                var users = _userService.GetAllUsers().ToList();
                
                users = ApplySearchFilter(users);
                users = ApplyRoleFilter(users);
                users = ApplyStatusFilter(users);

                gridUsers.DataSource = users.Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.FullName,
                    u.Role,
                    u.IsActive,
                    TrạngThái = u.IsActive ? "Hoạt động" : "Ngừng",
                    u.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Applies search filter to users list.
        /// Single responsibility: only filters by search text.
        /// </summary>
        private System.Collections.Generic.List<User> ApplySearchFilter(System.Collections.Generic.List<User> users)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
                return users;

            var searchText = txtSearch.Text.ToLower();
            return users.Where(u =>
                u.Username.ToLower().Contains(searchText) ||
                u.FullName.ToLower().Contains(searchText)
            ).ToList();
        }

        /// <summary>
        /// Applies role filter to users list.
        /// Single responsibility: only filters by role.
        /// </summary>
        private System.Collections.Generic.List<User> ApplyRoleFilter(System.Collections.Generic.List<User> users)
        {
            if (cmbRoleFilter.SelectedIndex <= 0)
                return users;

            var selectedRole = cmbRoleFilter.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(selectedRole))
                return users;

            return users.Where(u => u.Role == selectedRole).ToList();
        }

        /// <summary>
        /// Applies status filter to users list.
        /// Single responsibility: only filters by status.
        /// </summary>
        private System.Collections.Generic.List<User> ApplyStatusFilter(System.Collections.Generic.List<User> users)
        {
            if (cmbStatusFilter.SelectedIndex <= 0)
                return users;

            var isActive = cmbStatusFilter.SelectedIndex == 1; // 1 = Hoạt động, 2 = Ngừng
            return users.Where(u => u.IsActive == isActive).ToList();
        }

        /// <summary>
        /// Opens dialog to add a new user.
        /// Single responsibility: only handles add user action.
        /// </summary>
        private void AddUser()
        {
            using var dialog = new UserEditDialog(null);
            if (dialog.ShowDialog() == DialogResult.OK && dialog.CreateUserDTO != null)
            {
                try
                {
                    var user = new User
                    {
                        Username = dialog.CreateUserDTO.Username,
                        FullName = dialog.CreateUserDTO.FullName,
                        Role = dialog.CreateUserDTO.Role
                    };
                    _userService.CreateUser(user, dialog.CreateUserDTO.Password);
                    LoadUsers();
                    ErrorHandler.ShowSuccess("Thêm người dùng thành công!");
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError(ex);
                }
            }
        }

        /// <summary>
        /// Opens dialog to edit selected user.
        /// Single responsibility: only handles edit user action.
        /// </summary>
        private void EditSelectedUser()
        {
            var user = GetSelectedUser();
            if (user == null)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn người dùng cần sửa!");
                return;
            }

            using var dialog = new UserEditDialog(user);
            if (dialog.ShowDialog() == DialogResult.OK && dialog.UpdateUserDTO != null)
            {
                try
                {
                    var existingUser = _userService.GetUserById(user.Id);
                    if (existingUser == null)
                    {
                        ErrorHandler.ShowError("Không tìm thấy người dùng!");
                        return;
                    }

                    existingUser.Username = dialog.UpdateUserDTO.Username;
                    existingUser.FullName = dialog.UpdateUserDTO.FullName;
                    existingUser.Role = dialog.UpdateUserDTO.Role;
                    existingUser.IsActive = dialog.UpdateUserDTO.IsActive;

                    _userService.UpdateUser(existingUser);
                    
                    // Update password if provided
                    if (!string.IsNullOrWhiteSpace(dialog.UpdateUserDTO.Password))
                    {
                        _userService.UpdateUserPassword(existingUser.Id, dialog.UpdateUserDTO.Password);
                    }

                    LoadUsers();
                    ErrorHandler.ShowSuccess("Cập nhật người dùng thành công!");
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError(ex);
                }
            }
        }

        /// <summary>
        /// Deletes the selected user.
        /// Single responsibility: only handles delete user action.
        /// </summary>
        private void DeleteSelectedUser()
        {
            var user = GetSelectedUser();
            if (user == null)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn người dùng cần xóa!");
                return;
            }

            if (ErrorHandler.ShowConfirmation($"Bạn có chắc muốn xóa người dùng '{user.Username}'?"))
            {
                try
                {
                    _userService.DeleteUser(user.Id);
                    LoadUsers();
                    ErrorHandler.ShowSuccess("Xóa người dùng thành công!");
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError(ex);
                }
            }
        }

        /// <summary>
        /// Opens dialog to change password for selected user.
        /// Single responsibility: only handles change password action.
        /// </summary>
        private void ChangePassword()
        {
            var user = GetSelectedUser();
            if (user == null)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn người dùng cần đổi mật khẩu!");
                return;
            }

            using var dialog = new ChangePasswordDialog(user.Username);
            if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.NewPassword))
            {
                try
                {
                    _userService.UpdateUserPassword(user.Id, dialog.NewPassword);
                    ErrorHandler.ShowSuccess("Đổi mật khẩu thành công!");
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError(ex);
                }
            }
        }

        /// <summary>
        /// Toggles active status of selected user.
        /// Single responsibility: only handles toggle status action.
        /// </summary>
        private void ToggleUserStatus()
        {
            var user = GetSelectedUser();
            if (user == null)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn người dùng!");
                return;
            }

            try
            {
                if (user.IsActive)
                {
                    _userService.DeactivateUser(user.Id);
                    ErrorHandler.ShowSuccess("Đã ngừng kích hoạt người dùng!");
                }
                else
                {
                    _userService.ActivateUser(user.Id);
                    ErrorHandler.ShowSuccess("Đã kích hoạt người dùng!");
                }
                LoadUsers();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Gets the selected user from grid.
        /// Single responsibility: only retrieves selected user.
        /// </summary>
        private User? GetSelectedUser()
        {
            if (gridUsers.SelectedRows.Count == 0)
                return null;

            var id = (int)gridUsers.SelectedRows[0].Cells["Id"].Value;
            return _userService.GetUserById(id);
        }
    }

    /// <summary>
    /// Dialog for adding/editing user information.
    /// </summary>
    public class UserEditDialog : Form
    {
        private TextBox? _txtUsername, _txtFullName, _txtPassword;
        private ComboBox? _cmbRole;
        private CheckBox? _chkIsActive;
        private Button? _btnOK, _btnCancel;
        public CreateUserDTO? CreateUserDTO { get; private set; }
        public UpdateUserDTO? UpdateUserDTO { get; private set; }
        private User? _existingUser;

        public UserEditDialog(User? user)
        {
            _existingUser = user;
            Text = user == null ? "Thêm người dùng mới" : "Sửa thông tin người dùng";
            Width = 450;
            Height = user == null ? 280 : 320;
            StartPosition = FormStartPosition.CenterParent;
            InitializeControls();
        }

        /// <summary>
        /// Initializes all controls in the dialog.
        /// Single responsibility: only sets up UI controls.
        /// </summary>
        private void InitializeControls()
        {
            var lblUsername = new Label { Text = "Tên đăng nhập:", Left = 10, Top = 20, Width = 120 };
            _txtUsername = new TextBox { Left = 140, Top = 20, Width = 250, Text = _existingUser?.Username ?? "" };
            if (_existingUser != null) _txtUsername.ReadOnly = true; // Cannot change username

            var lblFullName = new Label { Text = "Họ và tên:", Left = 10, Top = 60, Width = 120 };
            _txtFullName = new TextBox { Left = 140, Top = 60, Width = 250, Text = _existingUser?.FullName ?? "" };

            var lblPassword = new Label { Text = _existingUser == null ? "Mật khẩu:" : "Mật khẩu mới (để trống nếu không đổi):", Left = 10, Top = 100, Width = 120 };
            _txtPassword = new TextBox { Left = 140, Top = 100, Width = 250, UseSystemPasswordChar = true };

            var lblRole = new Label { Text = "Vai trò:", Left = 10, Top = 140, Width = 120 };
            _cmbRole = new ComboBox { Left = 140, Top = 140, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            _cmbRole.Items.Add(UserRole.Admin);
            _cmbRole.Items.Add(UserRole.Staff);
            if (_existingUser != null)
            {
                _cmbRole.SelectedItem = _existingUser.Role;
            }
            else
            {
                _cmbRole.SelectedIndex = 1; // Default to Staff
            }

            _chkIsActive = new CheckBox { Text = "Hoạt động", Left = 140, Top = 180, Checked = _existingUser?.IsActive ?? true };
            if (_existingUser == null) _chkIsActive.Visible = false; // Hide for new users (always active)

            _btnOK = new Button { Text = "OK", Left = 140, Top = _existingUser == null ? 220 : 240, Width = 100, DialogResult = DialogResult.OK };
            _btnCancel = new Button { Text = "Hủy", Left = 250, Top = _existingUser == null ? 220 : 240, Width = 100, DialogResult = DialogResult.Cancel };

            _btnOK.Click += (s, e) =>
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
                        Username = _txtUsername?.Text.Trim() ?? "",
                        Password = _txtPassword?.Text ?? "",
                        FullName = _txtFullName?.Text.Trim() ?? "",
                        Role = _cmbRole?.SelectedItem?.ToString() ?? UserRole.Staff
                    };
                }
                else
                {
                    UpdateUserDTO = new UpdateUserDTO
                    {
                        Id = _existingUser.Id,
                        Username = _txtUsername?.Text.Trim() ?? "",
                        Password = string.IsNullOrWhiteSpace(_txtPassword?.Text) ? null : _txtPassword.Text,
                        FullName = _txtFullName?.Text.Trim() ?? "",
                        Role = _cmbRole?.SelectedItem?.ToString() ?? UserRole.Staff,
                        IsActive = _chkIsActive?.Checked ?? true
                    };
                }
            };

            Controls.AddRange(new Control[] {
                lblUsername, _txtUsername, lblFullName, _txtFullName,
                lblPassword, _txtPassword, lblRole, _cmbRole,
                _chkIsActive, _btnOK, _btnCancel
            });
        }

        /// <summary>
        /// Validates user input before saving.
        /// Single responsibility: only validates inputs.
        /// </summary>
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(_txtUsername?.Text))
            {
                ErrorHandler.ShowWarning("Vui lòng nhập tên đăng nhập!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_txtFullName?.Text))
            {
                ErrorHandler.ShowWarning("Vui lòng nhập họ và tên!");
                return false;
            }

            if (_existingUser == null && string.IsNullOrWhiteSpace(_txtPassword?.Text))
            {
                ErrorHandler.ShowWarning("Vui lòng nhập mật khẩu!");
                return false;
            }

            if (_cmbRole?.SelectedItem == null)
            {
                ErrorHandler.ShowWarning("Vui lòng chọn vai trò!");
                return false;
            }

            return true;
        }
    }

    /// <summary>
    /// Dialog for changing user password.
    /// </summary>
    public class ChangePasswordDialog : Form
    {
        private TextBox? _txtNewPassword, _txtConfirmPassword;
        private Button? _btnOK, _btnCancel;
        public string? NewPassword { get; private set; }
        private string _username;

        public ChangePasswordDialog(string username)
        {
            _username = username;
            Text = $"Đổi mật khẩu cho: {username}";
            Width = 400;
            Height = 180;
            StartPosition = FormStartPosition.CenterParent;
            InitializeControls();
        }

        /// <summary>
        /// Initializes all controls in the dialog.
        /// Single responsibility: only sets up UI controls.
        /// </summary>
        private void InitializeControls()
        {
            var lblNewPassword = new Label { Text = "Mật khẩu mới:", Left = 10, Top = 20, Width = 120 };
            _txtNewPassword = new TextBox { Left = 140, Top = 20, Width = 220, UseSystemPasswordChar = true };

            var lblConfirmPassword = new Label { Text = "Xác nhận mật khẩu:", Left = 10, Top = 60, Width = 120 };
            _txtConfirmPassword = new TextBox { Left = 140, Top = 60, Width = 220, UseSystemPasswordChar = true };

            _btnOK = new Button { Text = "OK", Left = 140, Top = 100, Width = 100, DialogResult = DialogResult.OK };
            _btnCancel = new Button { Text = "Hủy", Left = 250, Top = 100, Width = 100, DialogResult = DialogResult.Cancel };

            _btnOK.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(_txtNewPassword?.Text))
                {
                    ErrorHandler.ShowWarning("Vui lòng nhập mật khẩu mới!");
                    DialogResult = DialogResult.None;
                    return;
                }

                if (_txtNewPassword.Text != _txtConfirmPassword?.Text)
                {
                    ErrorHandler.ShowWarning("Mật khẩu xác nhận không khớp!");
                    DialogResult = DialogResult.None;
                    return;
                }

                if (_txtNewPassword.Text.Length < 6)
                {
                    ErrorHandler.ShowWarning("Mật khẩu phải có ít nhất 6 ký tự!");
                    DialogResult = DialogResult.None;
                    return;
                }

                NewPassword = _txtNewPassword.Text;
            };

            Controls.AddRange(new Control[] {
                lblNewPassword, _txtNewPassword,
                lblConfirmPassword, _txtConfirmPassword,
                _btnOK, _btnCancel
            });
        }
    }
}

