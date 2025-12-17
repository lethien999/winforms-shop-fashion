namespace WinFormsFashionShop.Presentation.Forms
{
    partial class InvoicePreviewDialog
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
            lblOrderDate = new Label();
            lblHeaderTitle = new Label();
            mainPanel = new Panel();
            _pnlPreview = new Panel();
            pnlInvoiceContent = new TableLayoutPanel();
            pnlHeaderBorder = new Panel();
            pnlShopInfo = new Panel();
            lblShopEmail = new Label();
            lblShopPhone = new Label();
            lblShopAddress = new Label();
            lblShopName = new Label();
            lblInvoiceTitle = new Label();
            lineOrder = new Panel();
            pnlOrderInfo = new Panel();
            lblPaymentMethod = new Label();
            lblStatus = new Label();
            lblCustomer = new Label();
            lblStaff = new Label();
            lblOrderCode = new Label();
            gridItems = new DataGridView();
            lineTotal = new Panel();
            pnlTotal = new Panel();
            lblTotalAmount = new Label();
            lblTotalLabel = new Label();
            lblThankYou1 = new Label();
            lblThankYou2 = new Label();
            pnlFooter = new Panel();
            lblInfo = new Label();
            _btnClose = new Button();
            _btnPrint = new Button();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            mainPanel.SuspendLayout();
            _pnlPreview.SuspendLayout();
            pnlInvoiceContent.SuspendLayout();
            pnlHeaderBorder.SuspendLayout();
            pnlShopInfo.SuspendLayout();
            pnlOrderInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridItems).BeginInit();
            pnlTotal.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(70, 130, 180);
            pnlHeader.Controls.Add(picLogo);
            pnlHeader.Controls.Add(lblOrderDate);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(15, 12, 15, 12);
            pnlHeader.Size = new Size(900, 60);
            pnlHeader.TabIndex = 0;
            // 
            // picLogo
            // 
            picLogo.Image = GUI.Properties.Resources.Logo_3T;
            picLogo.Location = new Point(15, 12);
            picLogo.Margin = new Padding(0);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(36, 36);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 2;
            picLogo.TabStop = false;
            // 
            // lblOrderDate
            // 
            lblOrderDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblOrderDate.Font = new Font("Arial", 10F);
            lblOrderDate.ForeColor = Color.White;
            lblOrderDate.Location = new Point(650, 20);
            lblOrderDate.Name = "lblOrderDate";
            lblOrderDate.Size = new Size(235, 20);
            lblOrderDate.TabIndex = 1;
            lblOrderDate.Text = "Ngày: 01/01/2024 00:00";
            lblOrderDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Arial", 16F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(56, 18);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(299, 32);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "HÓA ĐƠN BÁN HÀNG";
            lblHeaderTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // mainPanel
            // 
            mainPanel.AutoScroll = true;
            mainPanel.BackColor = Color.FromArgb(230, 230, 235);
            mainPanel.Controls.Add(_pnlPreview);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 60);
            mainPanel.Margin = new Padding(0);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(20);
            mainPanel.Size = new Size(900, 850);
            mainPanel.TabIndex = 1;
            // 
            // _pnlPreview
            // 
            _pnlPreview.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _pnlPreview.BackColor = Color.White;
            _pnlPreview.BorderStyle = BorderStyle.FixedSingle;
            _pnlPreview.Controls.Add(pnlInvoiceContent);
            _pnlPreview.Location = new Point(20, 20);
            _pnlPreview.Margin = new Padding(0);
            _pnlPreview.Name = "_pnlPreview";
            _pnlPreview.Padding = new Padding(30, 40, 30, 40);
            _pnlPreview.Size = new Size(860, 800);
            _pnlPreview.TabIndex = 0;
            // 
            // pnlInvoiceContent
            // 
            pnlInvoiceContent.AutoSize = true;
            pnlInvoiceContent.BackColor = Color.White;
            pnlInvoiceContent.ColumnCount = 1;
            pnlInvoiceContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlInvoiceContent.Controls.Add(pnlHeaderBorder, 0, 0);
            pnlInvoiceContent.Controls.Add(lineOrder, 0, 1);
            pnlInvoiceContent.Controls.Add(pnlOrderInfo, 0, 2);
            pnlInvoiceContent.Controls.Add(gridItems, 0, 3);
            pnlInvoiceContent.Controls.Add(lineTotal, 0, 4);
            pnlInvoiceContent.Controls.Add(pnlTotal, 0, 5);
            pnlInvoiceContent.Controls.Add(lblThankYou1, 0, 6);
            pnlInvoiceContent.Controls.Add(lblThankYou2, 0, 7);
            pnlInvoiceContent.Dock = DockStyle.Fill;
            pnlInvoiceContent.Location = new Point(30, 40);
            pnlInvoiceContent.Margin = new Padding(0);
            pnlInvoiceContent.Name = "pnlInvoiceContent";
            pnlInvoiceContent.Padding = new Padding(0, 0, 0, 20);
            pnlInvoiceContent.RowCount = 8;
            pnlInvoiceContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            pnlInvoiceContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            pnlInvoiceContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            pnlInvoiceContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 200F));
            pnlInvoiceContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            pnlInvoiceContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            pnlInvoiceContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            pnlInvoiceContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            pnlInvoiceContent.Size = new Size(798, 718);
            pnlInvoiceContent.TabIndex = 0;
            // 
            // pnlHeaderBorder
            // 
            pnlHeaderBorder.BackColor = Color.FromArgb(250, 250, 250);
            pnlHeaderBorder.BorderStyle = BorderStyle.FixedSingle;
            pnlHeaderBorder.Controls.Add(pnlShopInfo);
            pnlHeaderBorder.Controls.Add(lblInvoiceTitle);
            pnlHeaderBorder.Dock = DockStyle.Fill;
            pnlHeaderBorder.Location = new Point(0, 0);
            pnlHeaderBorder.Margin = new Padding(0);
            pnlHeaderBorder.Name = "pnlHeaderBorder";
            pnlHeaderBorder.Size = new Size(798, 90);
            pnlHeaderBorder.TabIndex = 0;
            // 
            // pnlShopInfo
            // 
            pnlShopInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlShopInfo.BackColor = Color.Transparent;
            pnlShopInfo.Controls.Add(lblShopEmail);
            pnlShopInfo.Controls.Add(lblShopPhone);
            pnlShopInfo.Controls.Add(lblShopAddress);
            pnlShopInfo.Controls.Add(lblShopName);
            pnlShopInfo.Location = new Point(5, 45);
            pnlShopInfo.Margin = new Padding(5, 45, 5, 0);
            pnlShopInfo.Name = "pnlShopInfo";
            pnlShopInfo.Size = new Size(788, 45);
            pnlShopInfo.TabIndex = 2;
            // 
            // lblShopEmail
            // 
            lblShopEmail.Font = new Font("Arial", 9F);
            lblShopEmail.ForeColor = Color.Black;
            lblShopEmail.Location = new Point(395, 40);
            lblShopEmail.Name = "lblShopEmail";
            lblShopEmail.Size = new Size(395, 18);
            lblShopEmail.TabIndex = 3;
            lblShopEmail.Text = "Email: shop@fashion.com";
            lblShopEmail.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblShopPhone
            // 
            lblShopPhone.Font = new Font("Arial", 9F);
            lblShopPhone.ForeColor = Color.Black;
            lblShopPhone.Location = new Point(0, 40);
            lblShopPhone.Name = "lblShopPhone";
            lblShopPhone.Size = new Size(395, 18);
            lblShopPhone.TabIndex = 2;
            lblShopPhone.Text = "ĐT: 0123 456 789";
            lblShopPhone.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblShopAddress
            // 
            lblShopAddress.Font = new Font("Arial", 9F);
            lblShopAddress.ForeColor = Color.Black;
            lblShopAddress.Location = new Point(0, 22);
            lblShopAddress.Name = "lblShopAddress";
            lblShopAddress.Size = new Size(788, 18);
            lblShopAddress.TabIndex = 1;
            lblShopAddress.Text = "Địa chỉ: 123 Đường ABC, Quận XYZ, TP.HCM";
            lblShopAddress.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblShopName
            // 
            lblShopName.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblShopName.ForeColor = Color.Black;
            lblShopName.Location = new Point(0, 0);
            lblShopName.Name = "lblShopName";
            lblShopName.Size = new Size(788, 22);
            lblShopName.TabIndex = 0;
            lblShopName.Text = "WinForms Fashion Shop";
            lblShopName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblInvoiceTitle
            // 
            lblInvoiceTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblInvoiceTitle.Font = new Font("Arial", 16F, FontStyle.Bold);
            lblInvoiceTitle.ForeColor = Color.Black;
            lblInvoiceTitle.Location = new Point(5, 12);
            lblInvoiceTitle.Margin = new Padding(5, 12, 5, 0);
            lblInvoiceTitle.Name = "lblInvoiceTitle";
            lblInvoiceTitle.Size = new Size(788, 32);
            lblInvoiceTitle.TabIndex = 1;
            lblInvoiceTitle.Text = "HÓA ĐƠN BÁN HÀNG";
            lblInvoiceTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lineOrder
            // 
            lineOrder.BackColor = Color.Black;
            lineOrder.Dock = DockStyle.Fill;
            lineOrder.Location = new Point(0, 90);
            lineOrder.Margin = new Padding(0);
            lineOrder.Name = "lineOrder";
            lineOrder.Size = new Size(798, 1);
            lineOrder.TabIndex = 3;
            // 
            // pnlOrderInfo
            // 
            pnlOrderInfo.BackColor = Color.White;
            pnlOrderInfo.Controls.Add(lblPaymentMethod);
            pnlOrderInfo.Controls.Add(lblStatus);
            pnlOrderInfo.Controls.Add(lblCustomer);
            pnlOrderInfo.Controls.Add(lblStaff);
            pnlOrderInfo.Controls.Add(lblOrderCode);
            pnlOrderInfo.Dock = DockStyle.Fill;
            pnlOrderInfo.Location = new Point(0, 91);
            pnlOrderInfo.Margin = new Padding(0);
            pnlOrderInfo.Name = "pnlOrderInfo";
            pnlOrderInfo.Padding = new Padding(5, 0, 5, 0);
            pnlOrderInfo.Size = new Size(798, 75);
            pnlOrderInfo.TabIndex = 4;
            // 
            // lblPaymentMethod
            // 
            lblPaymentMethod.Font = new Font("Arial", 10F);
            lblPaymentMethod.ForeColor = Color.Black;
            lblPaymentMethod.Location = new Point(5, 44);
            lblPaymentMethod.Name = "lblPaymentMethod";
            lblPaymentMethod.Size = new Size(390, 22);
            lblPaymentMethod.TabIndex = 4;
            lblPaymentMethod.Text = "Phương thức TT: Tiền mặt";
            lblPaymentMethod.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Arial", 10F);
            lblStatus.ForeColor = Color.Black;
            lblStatus.Location = new Point(400, 22);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(390, 22);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Trạng thái: Đã thanh toán";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCustomer
            // 
            lblCustomer.Font = new Font("Arial", 10F);
            lblCustomer.ForeColor = Color.Black;
            lblCustomer.Location = new Point(5, 22);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(390, 22);
            lblCustomer.TabIndex = 2;
            lblCustomer.Text = "Khách hàng: Khách lẻ";
            lblCustomer.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStaff
            // 
            lblStaff.Font = new Font("Arial", 10F);
            lblStaff.ForeColor = Color.Black;
            lblStaff.Location = new Point(400, 0);
            lblStaff.Name = "lblStaff";
            lblStaff.Size = new Size(390, 22);
            lblStaff.TabIndex = 1;
            lblStaff.Text = "Nhân viên: Administrator";
            lblStaff.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblOrderCode
            // 
            lblOrderCode.Font = new Font("Arial", 10F);
            lblOrderCode.ForeColor = Color.Black;
            lblOrderCode.Location = new Point(5, 0);
            lblOrderCode.Name = "lblOrderCode";
            lblOrderCode.Size = new Size(390, 22);
            lblOrderCode.TabIndex = 0;
            lblOrderCode.Text = "Mã đơn: ORD202511300001";
            lblOrderCode.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // gridItems
            // 
            gridItems.AllowUserToAddRows = false;
            gridItems.AllowUserToDeleteRows = false;
            gridItems.AllowUserToResizeColumns = false;
            gridItems.AllowUserToResizeRows = false;
            gridItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridItems.BackgroundColor = Color.White;
            gridItems.BorderStyle = BorderStyle.None;
            gridItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridItems.Dock = DockStyle.Fill;
            gridItems.Location = new Point(0, 166);
            gridItems.Margin = new Padding(0);
            gridItems.MultiSelect = false;
            gridItems.Name = "gridItems";
            gridItems.ReadOnly = true;
            gridItems.RowHeadersVisible = false;
            gridItems.RowHeadersWidth = 51;
            gridItems.RowTemplate.Height = 30;
            gridItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridItems.Size = new Size(798, 200);
            gridItems.TabIndex = 5;
            // 
            // lineTotal
            // 
            lineTotal.BackColor = Color.Black;
            lineTotal.Dock = DockStyle.Fill;
            lineTotal.Location = new Point(0, 366);
            lineTotal.Margin = new Padding(0);
            lineTotal.Name = "lineTotal";
            lineTotal.Size = new Size(798, 1);
            lineTotal.TabIndex = 6;
            // 
            // pnlTotal
            // 
            pnlTotal.BackColor = Color.FromArgb(245, 245, 250);
            pnlTotal.BorderStyle = BorderStyle.FixedSingle;
            pnlTotal.Controls.Add(lblTotalAmount);
            pnlTotal.Controls.Add(lblTotalLabel);
            pnlTotal.Dock = DockStyle.Fill;
            pnlTotal.Location = new Point(0, 367);
            pnlTotal.Margin = new Padding(0);
            pnlTotal.Name = "pnlTotal";
            pnlTotal.Padding = new Padding(5, 0, 5, 0);
            pnlTotal.Size = new Size(798, 45);
            pnlTotal.TabIndex = 7;
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTotalAmount.Font = new Font("Arial", 15F, FontStyle.Bold);
            lblTotalAmount.ForeColor = Color.FromArgb(220, 53, 69);
            lblTotalAmount.Location = new Point(5, 0);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(788, 45);
            lblTotalAmount.TabIndex = 1;
            lblTotalAmount.Text = "0 VNĐ";
            lblTotalAmount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTotalLabel
            // 
            lblTotalLabel.Font = new Font("Arial", 13F, FontStyle.Bold);
            lblTotalLabel.ForeColor = Color.Black;
            lblTotalLabel.Location = new Point(5, 0);
            lblTotalLabel.Name = "lblTotalLabel";
            lblTotalLabel.Size = new Size(200, 45);
            lblTotalLabel.TabIndex = 0;
            lblTotalLabel.Text = "TỔNG TIỀN:";
            lblTotalLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblThankYou1
            // 
            lblThankYou1.Dock = DockStyle.Fill;
            lblThankYou1.Font = new Font("Arial", 11F, FontStyle.Bold);
            lblThankYou1.ForeColor = Color.Black;
            lblThankYou1.Location = new Point(0, 0);
            lblThankYou1.Margin = new Padding(0);
            lblThankYou1.Name = "lblThankYou1";
            lblThankYou1.Size = new Size(798, 25);
            lblThankYou1.TabIndex = 8;
            lblThankYou1.Text = "Cảm ơn quý khách đã mua hàng!";
            lblThankYou1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblThankYou2
            // 
            lblThankYou2.Dock = DockStyle.Fill;
            lblThankYou2.Font = new Font("Arial", 10F, FontStyle.Italic);
            lblThankYou2.ForeColor = Color.Gray;
            lblThankYou2.Location = new Point(0, 0);
            lblThankYou2.Margin = new Padding(0);
            lblThankYou2.Name = "lblThankYou2";
            lblThankYou2.Size = new Size(798, 20);
            lblThankYou2.TabIndex = 9;
            lblThankYou2.Text = "Hẹn gặp lại quý khách!";
            lblThankYou2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(240, 240, 240);
            pnlFooter.Controls.Add(lblInfo);
            pnlFooter.Controls.Add(_btnClose);
            pnlFooter.Controls.Add(_btnPrint);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 910);
            pnlFooter.Margin = new Padding(0);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(15);
            pnlFooter.Size = new Size(900, 60);
            pnlFooter.TabIndex = 2;
            // 
            // lblInfo
            // 
            lblInfo.Dock = DockStyle.Left;
            lblInfo.Font = new Font("Arial", 9F, FontStyle.Italic);
            lblInfo.ForeColor = Color.Gray;
            lblInfo.Location = new Point(15, 15);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(300, 30);
            lblInfo.TabIndex = 2;
            lblInfo.Text = "💡 Xem trước hóa đơn trước khi in";
            lblInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _btnClose
            // 
            _btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _btnClose.BackColor = Color.FromArgb(108, 117, 125);
            _btnClose.Cursor = Cursors.Hand;
            _btnClose.FlatAppearance.BorderSize = 0;
            _btnClose.FlatStyle = FlatStyle.Flat;
            _btnClose.Font = new Font("Arial", 10F, FontStyle.Bold);
            _btnClose.ForeColor = Color.White;
            _btnClose.Location = new Point(780, 12);
            _btnClose.Margin = new Padding(0);
            _btnClose.Name = "_btnClose";
            _btnClose.Size = new Size(105, 35);
            _btnClose.TabIndex = 1;
            _btnClose.Text = "Đóng";
            _btnClose.UseVisualStyleBackColor = false;
            // 
            // _btnPrint
            // 
            _btnPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _btnPrint.BackColor = Color.FromArgb(34, 139, 34);
            _btnPrint.Cursor = Cursors.Hand;
            _btnPrint.FlatAppearance.BorderSize = 0;
            _btnPrint.FlatAppearance.MouseOverBackColor = Color.FromArgb(28, 120, 28);
            _btnPrint.FlatStyle = FlatStyle.Flat;
            _btnPrint.Font = new Font("Arial", 10F, FontStyle.Bold);
            _btnPrint.ForeColor = Color.White;
            _btnPrint.Location = new Point(665, 12);
            _btnPrint.Margin = new Padding(0);
            _btnPrint.Name = "_btnPrint";
            _btnPrint.Size = new Size(105, 35);
            _btnPrint.TabIndex = 0;
            _btnPrint.Text = "🖨️ In";
            _btnPrint.UseVisualStyleBackColor = false;
            // 
            // InvoicePreviewDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 250);
            ClientSize = new Size(900, 970);
            Controls.Add(mainPanel);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
            Margin = new Padding(0);
            MinimumSize = new Size(800, 700);
            Name = "InvoicePreviewDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Xem trước hóa đơn";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            mainPanel.ResumeLayout(false);
            _pnlPreview.ResumeLayout(false);
            _pnlPreview.PerformLayout();
            pnlInvoiceContent.ResumeLayout(false);
            pnlHeaderBorder.ResumeLayout(false);
            pnlShopInfo.ResumeLayout(false);
            pnlOrderInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridItems).EndInit();
            pnlTotal.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel _pnlPreview;
        private System.Windows.Forms.TableLayoutPanel pnlInvoiceContent;
        private System.Windows.Forms.Panel pnlHeaderBorder;
        private System.Windows.Forms.Label lblInvoiceTitle;
        private System.Windows.Forms.Panel pnlShopInfo;
        private System.Windows.Forms.Label lblShopName;
        private System.Windows.Forms.Label lblShopAddress;
        private System.Windows.Forms.Label lblShopPhone;
        private System.Windows.Forms.Label lblShopEmail;
        private System.Windows.Forms.Panel lineOrder;
        private System.Windows.Forms.Panel pnlOrderInfo;
        private System.Windows.Forms.Label lblOrderCode;
        private System.Windows.Forms.Label lblStaff;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.DataGridView gridItems;
        private System.Windows.Forms.Panel lineTotal;
        private System.Windows.Forms.Panel pnlTotal;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblThankYou1;
        private System.Windows.Forms.Label lblThankYou2;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button _btnPrint;
        private System.Windows.Forms.Button _btnClose;
        private System.Windows.Forms.Label lblInfo;
    }
}
