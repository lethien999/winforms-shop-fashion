namespace WinFormsFashionShop.Presentation.Forms
{
    partial class ChangePasswordDialog
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
            lblNewPassword = new Label();
            txtNewPassword = new TextBox();
            lblConfirmPassword = new Label();
            txtConfirmPassword = new TextBox();
            lblPasswordHint = new Label();
            pnlButtons = new Panel();
            btnCancel = new Button();
            btnOK = new Button();
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
            pnlHeader.Margin = new Padding(3, 4, 3, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(480, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderIcon
            // 
            lblHeaderIcon.Font = new Font("Segoe UI", 24F);
            lblHeaderIcon.ForeColor = Color.White;
            lblHeaderIcon.Location = new Point(17, 11);
            lblHeaderIcon.Name = "lblHeaderIcon";
            lblHeaderIcon.Size = new Size(57, 60);
            lblHeaderIcon.TabIndex = 0;
            lblHeaderIcon.Text = "🔐";
            lblHeaderIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(74, 24);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(162, 32);
            lblHeaderTitle.TabIndex = 1;
            lblHeaderTitle.Text = "Đổi mật khẩu";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(lblNewPassword);
            pnlContent.Controls.Add(txtNewPassword);
            pnlContent.Controls.Add(lblConfirmPassword);
            pnlContent.Controls.Add(txtConfirmPassword);
            pnlContent.Controls.Add(lblPasswordHint);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 80);
            pnlContent.Margin = new Padding(3, 4, 3, 4);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(29, 27, 29, 13);
            pnlContent.Size = new Size(480, 200);
            pnlContent.TabIndex = 1;
            // 
            // lblNewPassword
            // 
            lblNewPassword.AutoSize = true;
            lblNewPassword.Font = new Font("Segoe UI", 10F);
            lblNewPassword.ForeColor = Color.FromArgb(64, 64, 64);
            lblNewPassword.Location = new Point(0, 31);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(153, 23);
            lblNewPassword.TabIndex = 0;
            lblNewPassword.Text = "🔑  Mật khẩu mới:";
            // 
            // txtNewPassword
            // 
            txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
            txtNewPassword.Font = new Font("Segoe UI", 10F);
            txtNewPassword.Location = new Point(200, 29);
            txtNewPassword.Margin = new Padding(3, 4, 3, 4);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.Size = new Size(251, 30);
            txtNewPassword.TabIndex = 1;
            txtNewPassword.UseSystemPasswordChar = true;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Segoe UI", 10F);
            lblConfirmPassword.ForeColor = Color.FromArgb(64, 64, 64);
            lblConfirmPassword.Location = new Point(0, 85);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(195, 23);
            lblConfirmPassword.TabIndex = 2;
            lblConfirmPassword.Text = "🔒  Xác nhận mật khẩu:";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmPassword.Font = new Font("Segoe UI", 10F);
            txtConfirmPassword.Location = new Point(200, 83);
            txtConfirmPassword.Margin = new Padding(3, 4, 3, 4);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(251, 30);
            txtConfirmPassword.TabIndex = 3;
            txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // lblPasswordHint
            // 
            lblPasswordHint.AutoSize = true;
            lblPasswordHint.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblPasswordHint.ForeColor = Color.FromArgb(128, 128, 128);
            lblPasswordHint.Location = new Point(200, 133);
            lblPasswordHint.Name = "lblPasswordHint";
            lblPasswordHint.Size = new Size(199, 20);
            lblPasswordHint.TabIndex = 4;
            lblPasswordHint.Text = "💡 Mật khẩu tối thiểu 6 ký tự";
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.FromArgb(248, 249, 250);
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnOK);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Location = new Point(0, 280);
            pnlButtons.Margin = new Padding(3, 4, 3, 4);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(480, 80);
            pnlButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 98, 104);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(343, 16);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(126, 51);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "✕  Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.FromArgb(0, 123, 255);
            btnOK.Cursor = Cursors.Hand;
            btnOK.DialogResult = DialogResult.OK;
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 105, 217);
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnOK.ForeColor = Color.White;
            btnOK.Location = new Point(206, 16);
            btnOK.Margin = new Padding(3, 4, 3, 4);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(126, 51);
            btnOK.TabIndex = 4;
            btnOK.Text = "🔄  Đổi";
            btnOK.UseVisualStyleBackColor = false;
            // 
            // ChangePasswordDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnCancel;
            ClientSize = new Size(480, 360);
            Controls.Add(pnlContent);
            Controls.Add(pnlButtons);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChangePasswordDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Đổi mật khẩu";
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
        private Label lblNewPassword;
        private TextBox txtNewPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private Label lblPasswordHint;
        private Panel pnlButtons;
        private Button btnOK;
        private Button btnCancel;
    }
}
