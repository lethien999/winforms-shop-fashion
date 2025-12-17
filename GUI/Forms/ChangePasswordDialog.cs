using System;
using System.Windows.Forms;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    /// <summary>
    /// Dialog for changing user password.
    /// </summary>
    public partial class ChangePasswordDialog : Form
    {
        public string? NewPassword { get; private set; }
        private string _username;

        public ChangePasswordDialog(string username)
        {
            _username = username;
            InitializeComponent();
            InitializeControls();
        }

        /// <summary>
        /// Initializes all controls in the dialog.
        /// Single responsibility: only sets up UI controls.
        /// </summary>
        private void InitializeControls()
        {
            Text = $"Đổi mật khẩu cho: {_username}";

            // Wire up event handlers
            btnOK.Click += BtnOK_Click;
        }

        private void BtnOK_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                ErrorHandler.ShowWarning("Vui lòng nhập mật khẩu mới!");
                DialogResult = DialogResult.None;
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                ErrorHandler.ShowWarning("Mật khẩu xác nhận không khớp!");
                DialogResult = DialogResult.None;
                return;
            }

            if (txtNewPassword.Text.Length < 6)
            {
                ErrorHandler.ShowWarning("Mật khẩu phải có ít nhất 6 ký tự!");
                DialogResult = DialogResult.None;
                return;
            }

            NewPassword = txtNewPassword.Text;
        }
    }
}
