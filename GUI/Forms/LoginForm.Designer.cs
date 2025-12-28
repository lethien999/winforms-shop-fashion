namespace WinFormsFashionShop.Presentation.Forms
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            picLogo = new PictureBox();
            lblTitle = new Label();
            lblSubTitle = new Label();
            pnlContent = new Panel();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            pnlPassword = new Panel();
            txtPassword = new TextBox();
            btnShowPassword = new Button();
            chkRememberMe = new CheckBox();
            btnLogin = new Button();
            lblStatus = new Label();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlContent.SuspendLayout();
            pnlPassword.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(70, 130, 180);
            pnlHeader.Controls.Add(picLogo);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 15, 20, 15);
            pnlHeader.Size = new Size(450, 120);
            pnlHeader.TabIndex = 0;
            // 
            // picLogo
            // 
            picLogo.Image = GUI.Properties.Resources.Logo_3T;
            picLogo.Location = new Point(185, 15);
            picLogo.Margin = new Padding(0);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(80, 80);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 95);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(450, 25);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "WinForms Fashion Shop";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubTitle
            // 
            lblSubTitle.Font = new Font("Segoe UI", 9F);
            lblSubTitle.ForeColor = Color.FromArgb(220, 220, 220);
            lblSubTitle.Location = new Point(0, 115);
            lblSubTitle.Name = "lblSubTitle";
            lblSubTitle.Size = new Size(450, 20);
            lblSubTitle.TabIndex = 2;
            lblSubTitle.Text = "Hệ thống quản lý bán hàng thời trang";
            lblSubTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(250, 250, 252);
            pnlContent.Controls.Add(lblUsername);
            pnlContent.Controls.Add(txtUsername);
            pnlContent.Controls.Add(lblPassword);
            pnlContent.Controls.Add(pnlPassword);
            pnlContent.Controls.Add(chkRememberMe);
            pnlContent.Controls.Add(btnLogin);
            pnlContent.Controls.Add(lblStatus);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 120);
            pnlContent.Margin = new Padding(0);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(40, 25, 40, 25);
            pnlContent.Size = new Size(450, 330);
            pnlContent.TabIndex = 1;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(70, 70, 80);
            lblUsername.Location = new Point(40, 25);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(145, 19);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "👤 Tên đăng nhập";
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 11F);
            txtUsername.Location = new Point(40, 50);
            txtUsername.Margin = new Padding(0);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Nhập tên đăng nhập...";
            txtUsername.Size = new Size(370, 32);
            txtUsername.TabIndex = 1;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(70, 70, 80);
            lblPassword.Location = new Point(40, 100);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 19);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "🔒 Mật khẩu";
            // 
            // pnlPassword
            // 
            pnlPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlPassword.BackColor = Color.White;
            pnlPassword.BorderStyle = BorderStyle.FixedSingle;
            pnlPassword.Controls.Add(txtPassword);
            pnlPassword.Controls.Add(btnShowPassword);
            pnlPassword.Location = new Point(40, 125);
            pnlPassword.Margin = new Padding(0);
            pnlPassword.Name = "pnlPassword";
            pnlPassword.Size = new Size(370, 32);
            pnlPassword.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(5, 4);
            txtPassword.Margin = new Padding(0);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Nhập mật khẩu...";
            txtPassword.Size = new Size(325, 24);
            txtPassword.TabIndex = 0;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnShowPassword
            // 
            btnShowPassword.BackColor = Color.White;
            btnShowPassword.Cursor = Cursors.Hand;
            btnShowPassword.FlatAppearance.BorderSize = 0;
            btnShowPassword.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
            btnShowPassword.FlatStyle = FlatStyle.Flat;
            btnShowPassword.Font = new Font("Segoe UI Emoji", 10F);
            btnShowPassword.ForeColor = Color.FromArgb(100, 100, 100);
            btnShowPassword.Location = new Point(332, 0);
            btnShowPassword.Margin = new Padding(0);
            btnShowPassword.Name = "btnShowPassword";
            btnShowPassword.Size = new Size(36, 30);
            btnShowPassword.TabIndex = 1;
            btnShowPassword.Text = "👁️";
            btnShowPassword.UseVisualStyleBackColor = false;
            // 
            // chkRememberMe
            // 
            chkRememberMe.AutoSize = true;
            chkRememberMe.Cursor = Cursors.Hand;
            chkRememberMe.Font = new Font("Segoe UI", 9F);
            chkRememberMe.ForeColor = Color.FromArgb(80, 80, 90);
            chkRememberMe.Location = new Point(40, 170);
            chkRememberMe.Name = "chkRememberMe";
            chkRememberMe.Size = new Size(141, 24);
            chkRememberMe.TabIndex = 4;
            chkRememberMe.Text = "Nhớ đăng nhập";
            chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnLogin.BackColor = Color.FromArgb(70, 130, 180);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 110, 160);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(40, 210);
            btnLogin.Margin = new Padding(0);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(370, 45);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.TabIndex = 5;
            btnLogin.Text = "🔓  Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.Font = new Font("Segoe UI", 9F);
            lblStatus.ForeColor = Color.FromArgb(220, 53, 69);
            lblStatus.Location = new Point(40, 265);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(370, 40);
            lblStatus.TabIndex = 6;
            lblStatus.TextAlign = ContentAlignment.TopCenter;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 250, 252);
            ClientSize = new Size(450, 450);
            Controls.Add(pnlContent);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(0);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập - WinForms Fashion Shop";
            pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            pnlPassword.ResumeLayout(false);
            pnlPassword.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Panel pnlPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnShowPassword;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblStatus;
    }
}

