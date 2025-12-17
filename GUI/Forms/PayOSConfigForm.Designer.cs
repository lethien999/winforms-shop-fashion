namespace WinFormsFashionShop.Presentation.Forms
{
    partial class PayOSConfigForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlConfigGroup = new System.Windows.Forms.Panel();
            this.lblRequired = new System.Windows.Forms.Label();
            this._txtChecksumKey = new System.Windows.Forms.TextBox();
            this.lblChecksumKey = new System.Windows.Forms.Label();
            this._txtApiKey = new System.Windows.Forms.TextBox();
            this.lblApiKey = new System.Windows.Forms.Label();
            this._txtClientId = new System.Windows.Forms.TextBox();
            this.lblClientId = new System.Windows.Forms.Label();
            this.lblGroupTitle = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this._btnCancel = new System.Windows.Forms.Button();
            this._btnSave = new System.Windows.Forms.Button();
            this._btnTest = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlConfigGroup.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
            this.pnlHeader.Controls.Add(this.lblInfo);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(650, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblInfo
            // 
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblInfo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblInfo.ForeColor = System.Drawing.Color.White;
            this.lblInfo.Location = new System.Drawing.Point(0, 40);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(650, 40);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Lấy thông tin từ PayOS Dashboard: https://payos.vn";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(650, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CẤU HÌNH PAYOS API";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.Controls.Add(this.pnlConfigGroup);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 80);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.pnlContent.Size = new System.Drawing.Size(650, 300);
            this.pnlContent.TabIndex = 1;
            // 
            // pnlConfigGroup
            // 
            this.pnlConfigGroup.BackColor = System.Drawing.Color.White;
            this.pnlConfigGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlConfigGroup.Controls.Add(this.lblRequired);
            this.pnlConfigGroup.Controls.Add(this._txtChecksumKey);
            this.pnlConfigGroup.Controls.Add(this.lblChecksumKey);
            this.pnlConfigGroup.Controls.Add(this._txtApiKey);
            this.pnlConfigGroup.Controls.Add(this.lblApiKey);
            this.pnlConfigGroup.Controls.Add(this._txtClientId);
            this.pnlConfigGroup.Controls.Add(this.lblClientId);
            this.pnlConfigGroup.Controls.Add(this.lblGroupTitle);
            this.pnlConfigGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlConfigGroup.Location = new System.Drawing.Point(30, 20);
            this.pnlConfigGroup.Name = "pnlConfigGroup";
            this.pnlConfigGroup.Padding = new System.Windows.Forms.Padding(20);
            this.pnlConfigGroup.Size = new System.Drawing.Size(590, 220);
            this.pnlConfigGroup.TabIndex = 0;
            // 
            // lblRequired
            // 
            this.lblRequired.AutoSize = true;
            this.lblRequired.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblRequired.ForeColor = System.Drawing.Color.Gray;
            this.lblRequired.Location = new System.Drawing.Point(160, 160);
            this.lblRequired.Name = "lblRequired";
            this.lblRequired.Size = new System.Drawing.Size(110, 14);
            this.lblRequired.TabIndex = 7;
            this.lblRequired.Text = "* Thông tin bắt buộc";
            // 
            // _txtChecksumKey
            // 
            this._txtChecksumKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtChecksumKey.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.GraphicsUnit.Point);
            this._txtChecksumKey.Location = new System.Drawing.Point(160, 123);
            this._txtChecksumKey.Name = "_txtChecksumKey";
            this._txtChecksumKey.Size = new System.Drawing.Size(420, 23);
            this._txtChecksumKey.TabIndex = 6;
            this._txtChecksumKey.UseSystemPasswordChar = true;
            // 
            // lblChecksumKey
            // 
            this.lblChecksumKey.AutoSize = false;
            this.lblChecksumKey.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.GraphicsUnit.Point);
            this.lblChecksumKey.Location = new System.Drawing.Point(10, 125);
            this.lblChecksumKey.Name = "lblChecksumKey";
            this.lblChecksumKey.Size = new System.Drawing.Size(140, 20);
            this.lblChecksumKey.TabIndex = 5;
            this.lblChecksumKey.Text = "Checksum Key: *";
            // 
            // _txtApiKey
            // 
            this._txtApiKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtApiKey.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.GraphicsUnit.Point);
            this._txtApiKey.Location = new System.Drawing.Point(160, 83);
            this._txtApiKey.Name = "_txtApiKey";
            this._txtApiKey.Size = new System.Drawing.Size(420, 23);
            this._txtApiKey.TabIndex = 4;
            this._txtApiKey.UseSystemPasswordChar = true;
            // 
            // lblApiKey
            // 
            this.lblApiKey.AutoSize = false;
            this.lblApiKey.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.GraphicsUnit.Point);
            this.lblApiKey.Location = new System.Drawing.Point(10, 85);
            this.lblApiKey.Name = "lblApiKey";
            this.lblApiKey.Size = new System.Drawing.Size(140, 20);
            this.lblApiKey.TabIndex = 3;
            this.lblApiKey.Text = "API Key: *";
            // 
            // _txtClientId
            // 
            this._txtClientId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtClientId.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.GraphicsUnit.Point);
            this._txtClientId.Location = new System.Drawing.Point(160, 43);
            this._txtClientId.Name = "_txtClientId";
            this._txtClientId.Size = new System.Drawing.Size(420, 23);
            this._txtClientId.TabIndex = 2;
            // 
            // lblClientId
            // 
            this.lblClientId.AutoSize = false;
            this.lblClientId.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.GraphicsUnit.Point);
            this.lblClientId.Location = new System.Drawing.Point(10, 45);
            this.lblClientId.Name = "lblClientId";
            this.lblClientId.Size = new System.Drawing.Size(140, 20);
            this.lblClientId.TabIndex = 1;
            this.lblClientId.Text = "Client ID: *";
            // 
            // lblGroupTitle
            // 
            this.lblGroupTitle.AutoSize = true;
            this.lblGroupTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblGroupTitle.ForeColor = System.Drawing.Color.FromArgb(70, 130, 180);
            this.lblGroupTitle.Location = new System.Drawing.Point(10, 10);
            this.lblGroupTitle.Name = "lblGroupTitle";
            this.lblGroupTitle.Size = new System.Drawing.Size(95, 19);
            this.lblGroupTitle.TabIndex = 0;
            this.lblGroupTitle.Text = "Thông tin API";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.pnlFooter.Controls.Add(this._btnCancel);
            this.pnlFooter.Controls.Add(this._btnSave);
            this.pnlFooter.Controls.Add(this._btnTest);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 380);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(20);
            this.pnlFooter.Size = new System.Drawing.Size(650, 70);
            this.pnlFooter.TabIndex = 2;
            // 
            // _btnCancel
            // 
            this._btnCancel.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this._btnCancel.FlatAppearance.BorderSize = 0;
            this._btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCancel.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this._btnCancel.ForeColor = System.Drawing.Color.White;
            this._btnCancel.Location = new System.Drawing.Point(520, 15);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(110, 40);
            this._btnCancel.TabIndex = 2;
            this._btnCancel.Text = "Hủy";
            this._btnCancel.UseVisualStyleBackColor = false;
            // 
            // _btnSave
            // 
            this._btnSave.BackColor = System.Drawing.Color.FromArgb(34, 139, 34);
            this._btnSave.FlatAppearance.BorderSize = 0;
            this._btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSave.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this._btnSave.ForeColor = System.Drawing.Color.White;
            this._btnSave.Location = new System.Drawing.Point(400, 15);
            this._btnSave.Name = "_btnSave";
            this._btnSave.Size = new System.Drawing.Size(110, 40);
            this._btnSave.TabIndex = 1;
            this._btnSave.Text = "💾 Lưu";
            this._btnSave.UseVisualStyleBackColor = false;
            // 
            // _btnTest
            // 
            this._btnTest.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this._btnTest.FlatAppearance.BorderSize = 0;
            this._btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnTest.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this._btnTest.ForeColor = System.Drawing.Color.White;
            this._btnTest.Location = new System.Drawing.Point(20, 15);
            this._btnTest.Name = "_btnTest";
            this._btnTest.Size = new System.Drawing.Size(160, 40);
            this._btnTest.TabIndex = 0;
            this._btnTest.Text = "🔍 Kiểm tra kết nối";
            this._btnTest.UseVisualStyleBackColor = false;
            // 
            // PayOSConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 250);
            this.ClientSize = new System.Drawing.Size(650, 450);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PayOSConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cấu hình PayOS";
            this.pnlHeader.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlConfigGroup.ResumeLayout(false);
            this.pnlConfigGroup.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlConfigGroup;
        private System.Windows.Forms.Label lblGroupTitle;
        private System.Windows.Forms.Label lblClientId;
        private System.Windows.Forms.TextBox _txtClientId;
        private System.Windows.Forms.Label lblApiKey;
        private System.Windows.Forms.TextBox _txtApiKey;
        private System.Windows.Forms.Label lblChecksumKey;
        private System.Windows.Forms.TextBox _txtChecksumKey;
        private System.Windows.Forms.Label lblRequired;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button _btnTest;
        private System.Windows.Forms.Button _btnSave;
        private System.Windows.Forms.Button _btnCancel;
    }
}
