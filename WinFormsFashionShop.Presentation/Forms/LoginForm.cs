using System;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Services;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IAuthService _authService;
        private TextBox _txtUsername = new TextBox();
        private TextBox _txtPassword = new TextBox();
        private Button _btnLogin = new Button();

        public LoginForm(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            Text = "Login";
            Width = 400;
            Height = 250;
            InitializeControls();
        }

        private void InitializeControls()
        {
            _txtUsername.PlaceholderText = "Username";
            _txtUsername.Top = 30;
            _txtUsername.Left = 30;
            _txtUsername.Width = 300;

            _txtPassword.PlaceholderText = "Password";
            _txtPassword.Top = 70;
            _txtPassword.Left = 30;
            _txtPassword.Width = 300;
            _txtPassword.UseSystemPasswordChar = true;

            _btnLogin.Text = "Login";
            _btnLogin.Top = 110;
            _btnLogin.Left = 30;
            _btnLogin.Click += OnLoginClicked;

            Controls.Add(_txtUsername);
            Controls.Add(_txtPassword);
            Controls.Add(_btnLogin);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // LoginForm
            // 
            ClientSize = new Size(282, 253);
            Name = "LoginForm";
            Load += LoginForm_Load;
            ResumeLayout(false);

        }

        private void OnLoginClicked(object? sender, EventArgs e)
        {
            var username = _txtUsername.Text.Trim();
            var password = _txtPassword.Text; // in real app, use secure string
            try
            {
                if (_authService.ValidateCredentials(username, password))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Invalid credentials", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}