namespace WinFormsFashionShop.Presentation.Forms
{
    partial class QRCodePaymentDialog
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
            lblTitle = new Label();
            pnlContent = new Panel();
            _lblStatus = new Label();
            pnlQRCode = new Panel();
            _picQRCode = new PictureBox();
            lblQRCode = new Label();
            pnlBankInfo = new Panel();
            _lblBankName = new Label();
            _lblAccountNumber = new Label();
            _lblAccountName = new Label();
            _lblTransferContent = new Label();
            pnlOrderInfo = new Panel();
            lblDescription = new Label();
            _lblAmount = new Label();
            _lblOrderCode = new Label();
            pnlFooter = new Panel();
            _btnCancel = new Button();
            _btnCheckPayment = new Button();
            pnlHeader.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlQRCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_picQRCode).BeginInit();
            pnlBankInfo.SuspendLayout();
            pnlOrderInfo.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(70, 130, 180);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(550, 70);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Arial", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(550, 70);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "THANH TOÁN QUA VIETQR";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlContent
            // 
            pnlContent.AutoScroll = true;
            pnlContent.Controls.Add(pnlQRCode);
            pnlContent.Controls.Add(pnlBankInfo);
            pnlContent.Controls.Add(pnlOrderInfo);
            pnlContent.Controls.Add(_lblStatus);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 70);
            pnlContent.Margin = new Padding(0);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(20, 20, 20, 20);
            pnlContent.Size = new Size(550, 720);
            pnlContent.TabIndex = 1;
            // 
            // _lblStatus
            // 
            _lblStatus.Dock = DockStyle.Bottom;
            _lblStatus.Font = new Font("Arial", 9F, FontStyle.Italic);
            _lblStatus.ForeColor = Color.Blue;
            _lblStatus.Location = new Point(20, 645);
            _lblStatus.Name = "_lblStatus";
            _lblStatus.Size = new Size(510, 35);
            _lblStatus.TabIndex = 2;
            _lblStatus.Text = "⏳ Đang tạo mã QR...";
            _lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlQRCode
            // 
            pnlQRCode.Controls.Add(_picQRCode);
            pnlQRCode.Controls.Add(lblQRCode);
            pnlQRCode.Dock = DockStyle.Fill;
            pnlQRCode.Location = new Point(20, 323);
            pnlQRCode.Margin = new Padding(0);
            pnlQRCode.Name = "pnlQRCode";
            pnlQRCode.Padding = new Padding(10, 10, 10, 10);
            pnlQRCode.Size = new Size(510, 322);
            pnlQRCode.TabIndex = 1;
            // 
            // _picQRCode
            // 
            _picQRCode.BackColor = Color.White;
            _picQRCode.BorderStyle = BorderStyle.FixedSingle;
            _picQRCode.Dock = DockStyle.Fill;
            _picQRCode.Location = new Point(10, 50);
            _picQRCode.Margin = new Padding(0);
            _picQRCode.Name = "_picQRCode";
            _picQRCode.Padding = new Padding(10);
            _picQRCode.Size = new Size(490, 262);
            _picQRCode.SizeMode = PictureBoxSizeMode.Zoom;
            _picQRCode.TabIndex = 1;
            _picQRCode.TabStop = false;
            // 
            // lblQRCode
            // 
            lblQRCode.Dock = DockStyle.Top;
            lblQRCode.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblQRCode.Location = new Point(10, 10);
            lblQRCode.Name = "lblQRCode";
            lblQRCode.Size = new Size(490, 40);
            lblQRCode.TabIndex = 0;
            lblQRCode.Text = "📱 Quét mã QR để thanh toán";
            lblQRCode.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlBankInfo
            // 
            pnlBankInfo.BackColor = Color.FromArgb(240, 248, 255);
            pnlBankInfo.BorderStyle = BorderStyle.FixedSingle;
            pnlBankInfo.Controls.Add(_lblTransferContent);
            pnlBankInfo.Controls.Add(_lblAccountName);
            pnlBankInfo.Controls.Add(_lblAccountNumber);
            pnlBankInfo.Controls.Add(_lblBankName);
            pnlBankInfo.Dock = DockStyle.Top;
            pnlBankInfo.Location = new Point(20, 173);
            pnlBankInfo.Margin = new Padding(0, 10, 0, 10);
            pnlBankInfo.Name = "pnlBankInfo";
            pnlBankInfo.Padding = new Padding(15, 10, 15, 10);
            pnlBankInfo.Size = new Size(510, 150);
            pnlBankInfo.TabIndex = 3;
            // 
            // _lblBankName
            // 
            _lblBankName.Dock = DockStyle.Top;
            _lblBankName.Font = new Font("Arial", 11F, FontStyle.Bold);
            _lblBankName.ForeColor = Color.FromArgb(70, 130, 180);
            _lblBankName.Location = new Point(15, 10);
            _lblBankName.Name = "_lblBankName";
            _lblBankName.Size = new Size(478, 30);
            _lblBankName.TabIndex = 0;
            _lblBankName.Text = "🏦 Ngân hàng: Đang tải...";
            // 
            // _lblAccountNumber
            // 
            _lblAccountNumber.Dock = DockStyle.Top;
            _lblAccountNumber.Font = new Font("Consolas", 13F, FontStyle.Bold);
            _lblAccountNumber.ForeColor = Color.FromArgb(0, 100, 0);
            _lblAccountNumber.Location = new Point(15, 40);
            _lblAccountNumber.Name = "_lblAccountNumber";
            _lblAccountNumber.Size = new Size(478, 35);
            _lblAccountNumber.TabIndex = 1;
            _lblAccountNumber.Text = "💳 STK: ---";
            // 
            // _lblAccountName
            // 
            _lblAccountName.Dock = DockStyle.Top;
            _lblAccountName.Font = new Font("Arial", 10F, FontStyle.Bold);
            _lblAccountName.ForeColor = Color.FromArgb(25, 25, 112);
            _lblAccountName.Location = new Point(15, 75);
            _lblAccountName.Name = "_lblAccountName";
            _lblAccountName.Size = new Size(478, 28);
            _lblAccountName.TabIndex = 2;
            _lblAccountName.Text = "👤 Chủ TK: ---";
            // 
            // _lblTransferContent
            // 
            _lblTransferContent.Dock = DockStyle.Top;
            _lblTransferContent.Font = new Font("Consolas", 10F, FontStyle.Bold);
            _lblTransferContent.ForeColor = Color.FromArgb(139, 69, 19);
            _lblTransferContent.Location = new Point(15, 103);
            _lblTransferContent.Name = "_lblTransferContent";
            _lblTransferContent.Size = new Size(478, 35);
            _lblTransferContent.TabIndex = 3;
            _lblTransferContent.Text = "📝 Nội dung CK: ---";
            // 
            // pnlOrderInfo
            // 
            pnlOrderInfo.BackColor = Color.White;
            pnlOrderInfo.BorderStyle = BorderStyle.FixedSingle;
            pnlOrderInfo.Controls.Add(lblDescription);
            pnlOrderInfo.Controls.Add(_lblAmount);
            pnlOrderInfo.Controls.Add(_lblOrderCode);
            pnlOrderInfo.Dock = DockStyle.Top;
            pnlOrderInfo.Location = new Point(20, 20);
            pnlOrderInfo.Margin = new Padding(0);
            pnlOrderInfo.Name = "pnlOrderInfo";
            pnlOrderInfo.Padding = new Padding(15, 15, 15, 15);
            pnlOrderInfo.Size = new Size(510, 153);
            pnlOrderInfo.TabIndex = 0;
            // 
            // lblDescription
            // 
            lblDescription.Dock = DockStyle.Top;
            lblDescription.Font = new Font("Arial", 9F);
            lblDescription.ForeColor = Color.Gray;
            lblDescription.Location = new Point(17, 93);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(514, 27);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "📝 Mô tả đơn hàng";
            // 
            // _lblAmount
            // 
            _lblAmount.Dock = DockStyle.Top;
            _lblAmount.Font = new Font("Arial", 13F, FontStyle.Bold);
            _lblAmount.ForeColor = Color.FromArgb(220, 20, 60);
            _lblAmount.Location = new Point(17, 53);
            _lblAmount.Name = "_lblAmount";
            _lblAmount.Size = new Size(514, 40);
            _lblAmount.TabIndex = 1;
            _lblAmount.Text = "💰 Số tiền: 0 VNĐ";
            // 
            // _lblOrderCode
            // 
            _lblOrderCode.Dock = DockStyle.Top;
            _lblOrderCode.Font = new Font("Arial", 11F, FontStyle.Bold);
            _lblOrderCode.Location = new Point(17, 20);
            _lblOrderCode.Name = "_lblOrderCode";
            _lblOrderCode.Size = new Size(514, 33);
            _lblOrderCode.TabIndex = 0;
            _lblOrderCode.Text = "📋 Mã đơn: 0";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(240, 240, 240);
            pnlFooter.Controls.Add(_btnCancel);
            pnlFooter.Controls.Add(_btnCheckPayment);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 690);
            pnlFooter.Margin = new Padding(0);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(15, 12, 15, 12);
            pnlFooter.Size = new Size(550, 60);
            pnlFooter.TabIndex = 2;
            // 
            // _btnCancel
            // 
            _btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            _btnCancel.DialogResult = DialogResult.Cancel;
            _btnCancel.FlatAppearance.BorderSize = 0;
            _btnCancel.FlatStyle = FlatStyle.Flat;
            _btnCancel.Font = new Font("Arial", 10F, FontStyle.Bold);
            _btnCancel.ForeColor = Color.White;
            _btnCancel.Location = new Point(285, 12);
            _btnCancel.Margin = new Padding(0);
            _btnCancel.Name = "_btnCancel";
            _btnCancel.Size = new Size(235, 35);
            _btnCancel.TabIndex = 1;
            _btnCancel.Text = "Hủy";
            _btnCancel.UseVisualStyleBackColor = false;
            // 
            // _btnCheckPayment
            // 
            _btnCheckPayment.BackColor = Color.FromArgb(34, 139, 34);
            _btnCheckPayment.Enabled = false;
            _btnCheckPayment.FlatAppearance.BorderSize = 0;
            _btnCheckPayment.FlatStyle = FlatStyle.Flat;
            _btnCheckPayment.Font = new Font("Arial", 10F, FontStyle.Bold);
            _btnCheckPayment.ForeColor = Color.White;
            _btnCheckPayment.Location = new Point(15, 12);
            _btnCheckPayment.Margin = new Padding(0);
            _btnCheckPayment.Name = "_btnCheckPayment";
            _btnCheckPayment.Size = new Size(260, 35);
            _btnCheckPayment.TabIndex = 0;
            _btnCheckPayment.Text = "🔄 Kiểm tra thanh toán";
            _btnCheckPayment.UseVisualStyleBackColor = false;
            // 
            // QRCodePaymentDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 250);
            ClientSize = new Size(550, 850);
            Controls.Add(pnlContent);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(0);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "QRCodePaymentDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thanh toán qua VietQR - PayOS";
            pnlHeader.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlQRCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_picQRCode).EndInit();
            pnlBankInfo.ResumeLayout(false);
            pnlOrderInfo.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlOrderInfo;
        private System.Windows.Forms.Label _lblOrderCode;
        private System.Windows.Forms.Label _lblAmount;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Panel pnlBankInfo;
        private System.Windows.Forms.Label _lblBankName;
        private System.Windows.Forms.Label _lblAccountNumber;
        private System.Windows.Forms.Label _lblAccountName;
        private System.Windows.Forms.Label _lblTransferContent;
        private System.Windows.Forms.Panel pnlQRCode;
        private System.Windows.Forms.Label lblQRCode;
        private System.Windows.Forms.PictureBox _picQRCode;
        private System.Windows.Forms.Label _lblStatus;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button _btnCheckPayment;
        private System.Windows.Forms.Button _btnCancel;
    }
}
