using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.Business.Mappers;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;
using UIThemeConstants = WinFormsFashionShop.Presentation.Helpers.UIThemeConstants;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class UserManagementForm : Form
    {
        private readonly IUserService _userService;
        private readonly IErrorHandler _errorHandler;

        public UserManagementForm(IUserService userService, IErrorHandler errorHandler)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
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

            // Apply grid styling
            UIThemeConstants.ThemeHelper.ApplyGridStyle(gridUsers);

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
                if (gridUsers == null)
                {
                    _errorHandler.ShowError("Grid người dùng chưa được khởi tạo!");
                    return;
                }

                var allUsers = _userService.GetAllUsers();
                if (allUsers == null)
                {
                    _errorHandler.ShowWarning("Không thể tải danh sách người dùng. Dịch vụ trả về null.");
                    gridUsers.DataSource = null;
                    return;
                }

                var users = allUsers.ToList();
                
                users = ApplySearchFilter(users);
                users = ApplyRoleFilter(users);
                users = ApplyStatusFilter(users);

                gridUsers.DataSource = users?.Where(u => u != null).Select(u => new
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
                _errorHandler.ShowError($"Lỗi khi tải danh sách người dùng: {ex.Message}");
                if (gridUsers != null)
                {
                    gridUsers.DataSource = null;
                }
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
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.CreateUserDTO != null)
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
                    _errorHandler.ShowSuccess("Thêm người dùng thành công!");
                }
                catch (Exception ex)
                {
                    _errorHandler.ShowError(ex);
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
                _errorHandler.ShowWarning("Vui lòng chọn người dùng cần sửa!");
                return;
            }

            using var dialog = new UserEditDialog(user);
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.UpdateUserDTO != null)
            {
                try
                {
                    var existingUser = _userService.GetUserById(user.Id);
                    if (existingUser == null)
                    {
                        _errorHandler.ShowError("Không tìm thấy người dùng!");
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
                    _errorHandler.ShowSuccess("Cập nhật người dùng thành công!");
                }
                catch (Exception ex)
                {
                    _errorHandler.ShowError(ex);
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
                _errorHandler.ShowWarning("Vui lòng chọn người dùng cần xóa!");
                return;
            }

            if (_errorHandler.ShowConfirmation($"Bạn có chắc muốn xóa người dùng '{user.Username}'?"))
            {
                try
                {
                    _userService.DeleteUser(user.Id);
                    LoadUsers();
                    _errorHandler.ShowSuccess("Xóa người dùng thành công!");
                }
                catch (Exception ex)
                {
                    _errorHandler.ShowError(ex);
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
                _errorHandler.ShowWarning("Vui lòng chọn người dùng cần đổi mật khẩu!");
                return;
            }

            using var dialog = new ChangePasswordDialog(user.Username);
            if (dialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.NewPassword))
            {
                try
                {
                    _userService.UpdateUserPassword(user.Id, dialog.NewPassword);
                    _errorHandler.ShowSuccess("Đổi mật khẩu thành công!");
                }
                catch (Exception ex)
                {
                    _errorHandler.ShowError(ex);
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
                _errorHandler.ShowWarning("Vui lòng chọn người dùng!");
                return;
            }

            try
            {
                if (user.IsActive)
                {
                    _userService.DeactivateUser(user.Id);
                    _errorHandler.ShowSuccess("Đã ngừng kích hoạt người dùng!");
                }
                else
                {
                    _userService.ActivateUser(user.Id);
                    _errorHandler.ShowSuccess("Đã kích hoạt người dùng!");
                }
                LoadUsers();
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError(ex);
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

}

