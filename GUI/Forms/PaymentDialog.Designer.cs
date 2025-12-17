namespace WinFormsFashionShop.Presentation.Forms
{
    partial class PaymentDialog
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
            pnlTotal = new Panel();
            _lblTotalAmount = new Label();
            lblTotalLabel = new Label();
            pnlPayment = new Panel();
            lblCustomerPayment = new Label();
            _txtCustomerPayment = new TextBox();
            pnlChange = new Panel();
            _lblChange = new Label();
            lblChangeLabel = new Label();
            pnlFooter = new Panel();
            _btnConfirm = new Button();
            btnCancel = new Button();
            pnlHeader.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlTotal.SuspendLayout();
            pnlPayment.SuspendLayout();
            pnlChange.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(70, 130, 180);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(500, 70);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Arial", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(500, 70);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "💳 THANH TOÁN";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlContent
            // 
            pnlContent.AutoScroll = true;
            pnlContent.Controls.Add(pnlChange);
            pnlContent.Controls.Add(pnlPayment);
            pnlContent.Controls.Add(pnlTotal);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 70);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(25, 20, 25, 20);
            pnlContent.Size = new Size(500, 280);
            pnlContent.TabIndex = 1;
            // 
            // pnlTotal
            // 
            pnlTotal.BackColor = Color.White;
            pnlTotal.BorderStyle = BorderStyle.FixedSingle;
            pnlTotal.Controls.Add(_lblTotalAmount);
            pnlTotal.Controls.Add(lblTotalLabel);
            pnlTotal.Dock = DockStyle.Top;
            pnlTotal.Location = new Point(25, 20);
            pnlTotal.Name = "pnlTotal";
            pnlTotal.Padding = new Padding(15);
            pnlTotal.Size = new Size(450, 70);
            pnlTotal.TabIndex = 0;
            // 
            // _lblTotalAmount
            // 
            _lblTotalAmount.Dock = DockStyle.Top;
            _lblTotalAmount.Font = new Font("Arial", 16F, FontStyle.Bold);
            _lblTotalAmount.ForeColor = Color.FromArgb(220, 20, 60);
            _lblTotalAmount.Location = new Point(15, 25);
            _lblTotalAmount.Name = "_lblTotalAmount";
            _lblTotalAmount.Size = new Size(420, 30);
            _lblTotalAmount.TabIndex = 1;
            _lblTotalAmount.Text = "0 VNĐ";
            _lblTotalAmount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalLabel
            // 
            lblTotalLabel.Dock = DockStyle.Top;
            lblTotalLabel.Font = new Font("Arial", 11F);
            lblTotalLabel.Location = new Point(15, 15);
            lblTotalLabel.Name = "lblTotalLabel";
            lblTotalLabel.Size = new Size(420, 25);
            lblTotalLabel.TabIndex = 0;
            lblTotalLabel.Text = "💰 Tổng tiền cần thanh toán:";
            // 
            // pnlPayment
            // 
            pnlPayment.BackColor = Color.White;
            pnlPayment.BorderStyle = BorderStyle.FixedSingle;
            pnlPayment.Controls.Add(_txtCustomerPayment);
            pnlPayment.Controls.Add(lblCustomerPayment);
            pnlPayment.Dock = DockStyle.Top;
            pnlPayment.Location = new Point(25, 90);
            pnlPayment.Name = "pnlPayment";
            pnlPayment.Padding = new Padding(15);
            pnlPayment.Size = new Size(450, 80);
            pnlPayment.TabIndex = 1;
            // 
            // lblCustomerPayment
            // 
            lblCustomerPayment.Dock = DockStyle.Top;
            lblCustomerPayment.Font = new Font("Arial", 11F);
            lblCustomerPayment.Location = new Point(15, 15);
            lblCustomerPayment.Name = "lblCustomerPayment";
            lblCustomerPayment.Size = new Size(420, 25);
            lblCustomerPayment.TabIndex = 0;
            lblCustomerPayment.Text = "💵 Số tiền khách đưa:";
            // 
            // _txtCustomerPayment
            // 
            _txtCustomerPayment.Dock = DockStyle.Top;
            _txtCustomerPayment.Font = new Font("Arial", 14F, FontStyle.Bold);
            _txtCustomerPayment.Location = new Point(15, 40);
            _txtCustomerPayment.Name = "_txtCustomerPayment";
            _txtCustomerPayment.Size = new Size(420, 29);
            _txtCustomerPayment.TabIndex = 1;
            _txtCustomerPayment.TextAlign = HorizontalAlignment.Right;
            // 
            // pnlChange
            // 
            pnlChange.BackColor = Color.White;
            pnlChange.BorderStyle = BorderStyle.FixedSingle;
            pnlChange.Controls.Add(_lblChange);
            pnlChange.Controls.Add(lblChangeLabel);
            pnlChange.Dock = DockStyle.Top;
            pnlChange.Location = new Point(25, 170);
            pnlChange.Name = "pnlChange";
            pnlChange.Padding = new Padding(15);
            pnlChange.Size = new Size(450, 70);
            pnlChange.TabIndex = 2;
            // 
            // lblChangeLabel
            // 
            lblChangeLabel.Dock = DockStyle.Top;
            lblChangeLabel.Font = new Font("Arial", 11F);
            lblChangeLabel.Location = new Point(15, 15);
            lblChangeLabel.Name = "lblChangeLabel";
            lblChangeLabel.Size = new Size(420, 25);
            lblChangeLabel.TabIndex = 0;
            lblChangeLabel.Text = "💰 Tiền thừa:";
            // 
            // _lblChange
            // 
            _lblChange.Dock = DockStyle.Top;
            _lblChange.Font = new Font("Arial", 16F, FontStyle.Bold);
            _lblChange.ForeColor = Color.Green;
            _lblChange.Location = new Point(15, 40);
            _lblChange.Name = "_lblChange";
            _lblChange.Size = new Size(420, 30);
            _lblChange.TabIndex = 1;
            _lblChange.Text = "0 VNĐ";
            _lblChange.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(240, 240, 240);
            pnlFooter.Controls.Add(btnCancel);
            pnlFooter.Controls.Add(_btnConfirm);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 350);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(20);
            pnlFooter.Size = new Size(500, 80);
            pnlFooter.TabIndex = 2;
            // 
            // _btnConfirm
            // 
            _btnConfirm.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnConfirm.BackColor = Color.FromArgb(34, 139, 34);
            _btnConfirm.FlatAppearance.BorderSize = 0;
            _btnConfirm.FlatStyle = FlatStyle.Flat;
            _btnConfirm.Font = new Font("Arial", 11F, FontStyle.Bold);
            _btnConfirm.ForeColor = Color.White;
            _btnConfirm.Location = new Point(200, 15);
            _btnConfirm.Name = "_btnConfirm";
            _btnConfirm.Size = new Size(130, 40);
            _btnConfirm.TabIndex = 0;
            _btnConfirm.Text = "✅ Xác nhận";
            _btnConfirm.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Arial", 11F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(350, 15);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(130, 40);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // PaymentDialog
            // 
            AcceptButton = _btnConfirm;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 250);
            CancelButton = btnCancel;
            ClientSize = new Size(500, 430);
            Controls.Add(pnlContent);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PaymentDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thanh toán";
            pnlHeader.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlTotal.ResumeLayout(false);
            pnlPayment.ResumeLayout(false);
            pnlPayment.PerformLayout();
            pnlChange.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlContent;
        private Panel pnlTotal;
        private Label lblTotalLabel;
        internal Label _lblTotalAmount;
        private Panel pnlPayment;
        private Label lblCustomerPayment;
        internal TextBox _txtCustomerPayment;
        private Panel pnlChange;
        private Label lblChangeLabel;
        internal Label _lblChange;
        private Panel pnlFooter;
        internal Button _btnConfirm;
        private Button btnCancel;
    }
}
