using System;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IAuthService _authService;

        // Property to store logged-in user
        public UserDTO? LoggedInUser { get; private set; }

        public LoginForm(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            txtPassword.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) OnLoginClicked(s, e); };
            btnLogin.Click += OnLoginClicked;
        }

        private void OnLoginClicked(object? sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;
            
            if (string.IsNullOrWhiteSpace(username))
            {
                lblStatus.Text = "Vui lòng nhập tên đăng nhập!";
                return;
            }
            
            if (string.IsNullOrWhiteSpace(password))
            {
                lblStatus.Text = "Vui lòng nhập mật khẩu!";
                return;
            }

            try
            {
                // Get user first to check if active
                var user = _authService.GetUserByUsername(username);
                
                if (user == null)
                {
                    lblStatus.Text = "Tên đăng nhập không tồn tại!";
                    return;
                }

                if (!user.IsActive)
                {
                    lblStatus.Text = "Tài khoản đã bị vô hiệu hóa. Vui lòng liên hệ quản trị viên.";
                    ErrorHandler.ShowWarning("Tài khoản của bạn đã bị vô hiệu hóa. Vui lòng liên hệ quản trị viên.");
                    return;
                }

                if (_authService.ValidateCredentials(username, password))
                {
                    LoggedInUser = user;
                    lblStatus.Text = $"Đăng nhập thành công! Chào mừng {user.FullName} ({user.Role})";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    
                    // Show success message with role
                    ErrorHandler.ShowInfo($"Đăng nhập thành công!\n\nTên: {user.FullName}\nVai trò: {user.Role}");
                    
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    lblStatus.Text = "Mật khẩu không đúng!";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Lỗi: {ex.Message}";
                ErrorHandler.ShowError(ex);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}