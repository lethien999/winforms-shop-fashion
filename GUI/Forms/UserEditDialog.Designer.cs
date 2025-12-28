namespace WinFormsFashionShop.Presentation.Forms
{
    partial class UserEditDialog
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
            lblHeaderIcon = new Label();
            lblHeaderTitle = new Label();
            pnlContent = new Panel();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblRole = new Label();
            cmbRole = new ComboBox();
            chkIsActive = new CheckBox();
            pnlButtons = new Panel();
            btnOK = new Button();
            btnCancel = new Button();
            pnlHeader.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(70, 130, 180);
            pnlHeader.Controls.Add(lblHeaderIcon);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(480, 60);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderIcon
            // 
            lblHeaderIcon.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            lblHeaderIcon.ForeColor = Color.White;
            lblHeaderIcon.Location = new Point(15, 8);
            lblHeaderIcon.Name = "lblHeaderIcon";
            lblHeaderIcon.Size = new Size(50, 45);
            lblHeaderIcon.TabIndex = 0;
            lblHeaderIcon.Text = "👨‍💼";
            lblHeaderIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(65, 18);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(190, 25);
            lblHeaderTitle.TabIndex = 1;
            lblHeaderTitle.Text = "Thông tin người dùng";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(lblUsername);
            pnlContent.Controls.Add(txtUsername);
            pnlContent.Controls.Add(lblFullName);
            pnlContent.Controls.Add(txtFullName);
            pnlContent.Controls.Add(lblPassword);
            pnlContent.Controls.Add(txtPassword);
            pnlContent.Controls.Add(lblRole);
            pnlContent.Controls.Add(cmbRole);
            pnlContent.Controls.Add(chkIsActive);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 60);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(25, 20, 25, 10);
            pnlContent.Size = new Size(480, 240);
            pnlContent.TabIndex = 1;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblUsername.ForeColor = Color.FromArgb(64, 64, 64);
            lblUsername.Location = new Point(25, 20);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(130, 19);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "👤  Tên đăng nhập:";
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtUsername.Location = new Point(170, 17);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(280, 25);
            txtUsername.TabIndex = 1;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblFullName.ForeColor = Color.FromArgb(64, 64, 64);
            lblFullName.Location = new Point(25, 58);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(100, 19);
            lblFullName.TabIndex = 2;
            lblFullName.Text = "📋  Họ và tên:";
            // 
            // txtFullName
            // 
            txtFullName.BorderStyle = BorderStyle.FixedSingle;
            txtFullName.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtFullName.Location = new Point(170, 55);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(280, 25);
            txtFullName.TabIndex = 3;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblPassword.ForeColor = Color.FromArgb(64, 64, 64);
            lblPassword.Location = new Point(25, 96);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 19);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "🔒  Mật khẩu:";
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtPassword.Location = new Point(170, 93);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(280, 25);
            txtPassword.TabIndex = 5;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblRole.ForeColor = Color.FromArgb(64, 64, 64);
            lblRole.Location = new Point(25, 134);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(80, 19);
            lblRole.TabIndex = 6;
            lblRole.Text = "🎭  Vai trò:";
            // 
            // cmbRole
            // 
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FlatStyle = FlatStyle.Flat;
            cmbRole.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(170, 131);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(280, 25);
            cmbRole.TabIndex = 7;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Checked = true;
            chkIsActive.CheckState = CheckState.Checked;
            chkIsActive.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            chkIsActive.ForeColor = Color.FromArgb(64, 64, 64);
            chkIsActive.Location = new Point(170, 175);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(119, 23);
            chkIsActive.TabIndex = 8;
            chkIsActive.Text = "✅  Hoạt động";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.FromArgb(248, 249, 250);
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnOK);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Location = new Point(0, 300);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(480, 60);
            pnlButtons.TabIndex = 2;
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.FromArgb(34, 139, 34);
            btnOK.Cursor = Cursors.Hand;
            btnOK.DialogResult = DialogResult.OK;
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 167, 69);
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnOK.ForeColor = Color.White;
            btnOK.Location = new Point(240, 12);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(110, 38);
            btnOK.TabIndex = 9;
            btnOK.Text = "✓  Lưu";
            btnOK.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 98, 104);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(360, 12);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(110, 38);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "✕  Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // UserEditDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnCancel;
            ClientSize = new Size(480, 360);
            Controls.Add(pnlContent);
            Controls.Add(pnlButtons);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserEditDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thông tin người dùng";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderIcon;
        private Label lblHeaderTitle;
        private Panel pnlContent;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblRole;
        private ComboBox cmbRole;
        private CheckBox chkIsActive;
        private Panel pnlButtons;
        private Button btnOK;
        private Button btnCancel;
    }
}
