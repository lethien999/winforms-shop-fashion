namespace WinFormsFashionShop.Presentation.Forms
{
    partial class OrderDetailDialog
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
            pnlInfo = new Panel();
            lblBankTransferInfo = new Label();
            lblStatus = new Label();
            lblPayment = new Label();
            lblStaff = new Label();
            lblCustomer = new Label();
            lblDate = new Label();
            lblOrderCode = new Label();
            pnlGrid = new Panel();
            gridItems = new DataGridView();
            pnlFooter = new Panel();
            pnlActions = new Panel();
            btnPayVietQR = new Button();
            btnCheckPayment = new Button();
            btnCancelOrder = new Button();
            btnPrint = new Button();
            btnClose = new Button();
            lblTotal = new Label();
            pnlHeader.SuspendLayout();
            pnlInfo.SuspendLayout();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridItems).BeginInit();
            pnlFooter.SuspendLayout();
            pnlActions.SuspendLayout();
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
            pnlHeader.Size = new Size(914, 65);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderIcon
            // 
            lblHeaderIcon.Font = new Font("Segoe UI", 26F);
            lblHeaderIcon.ForeColor = Color.White;
            lblHeaderIcon.Location = new Point(15, 0);
            lblHeaderIcon.Name = "lblHeaderIcon";
            lblHeaderIcon.Size = new Size(55, 61);
            lblHeaderIcon.TabIndex = 0;
            lblHeaderIcon.Text = "📋";
            lblHeaderIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(70, 18);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(230, 37);
            lblHeaderTitle.TabIndex = 1;
            lblHeaderTitle.Text = "Chi tiết đơn hàng";
            // 
            // pnlInfo
            // 
            pnlInfo.BackColor = Color.FromArgb(248, 249, 250);
            pnlInfo.Controls.Add(lblBankTransferInfo);
            pnlInfo.Controls.Add(lblStatus);
            pnlInfo.Controls.Add(lblPayment);
            pnlInfo.Controls.Add(lblStaff);
            pnlInfo.Controls.Add(lblCustomer);
            pnlInfo.Controls.Add(lblDate);
            pnlInfo.Controls.Add(lblOrderCode);
            pnlInfo.Dock = DockStyle.Top;
            pnlInfo.Location = new Point(0, 65);
            pnlInfo.Margin = new Padding(3, 4, 3, 4);
            pnlInfo.Name = "pnlInfo";
            pnlInfo.Padding = new Padding(20, 15, 20, 15);
            pnlInfo.Size = new Size(914, 190);
            pnlInfo.TabIndex = 1;
            // 
            // lblBankTransferInfo
            // 
            lblBankTransferInfo.Font = new Font("Segoe UI", 9F);
            lblBankTransferInfo.ForeColor = Color.FromArgb(70, 130, 180);
            lblBankTransferInfo.Location = new Point(20, 180);
            lblBankTransferInfo.Name = "lblBankTransferInfo";
            lblBankTransferInfo.Size = new Size(400, 100);
            lblBankTransferInfo.TabIndex = 6;
            lblBankTransferInfo.Visible = false;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(64, 64, 64);
            lblStatus.Location = new Point(20, 150);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(125, 23);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "📊  Trạng thái:";
            // 
            // lblPayment
            // 
            lblPayment.AutoSize = true;
            lblPayment.Font = new Font("Segoe UI", 10F);
            lblPayment.ForeColor = Color.FromArgb(64, 64, 64);
            lblPayment.Location = new Point(20, 122);
            lblPayment.Name = "lblPayment";
            lblPayment.Size = new Size(169, 23);
            lblPayment.TabIndex = 4;
            lblPayment.Text = "💳  Phương thức TT:";
            // 
            // lblStaff
            // 
            lblStaff.AutoSize = true;
            lblStaff.Font = new Font("Segoe UI", 10F);
            lblStaff.ForeColor = Color.FromArgb(64, 64, 64);
            lblStaff.Location = new Point(20, 94);
            lblStaff.Name = "lblStaff";
            lblStaff.Size = new Size(125, 23);
            lblStaff.TabIndex = 3;
            lblStaff.Text = "👨‍💼  Nhân viên:";
            // 
            // lblCustomer
            // 
            lblCustomer.AutoSize = true;
            lblCustomer.Font = new Font("Segoe UI", 10F);
            lblCustomer.ForeColor = Color.FromArgb(64, 64, 64);
            lblCustomer.Location = new Point(20, 66);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(138, 23);
            lblCustomer.TabIndex = 2;
            lblCustomer.Text = "👤  Khách hàng:";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 10F);
            lblDate.ForeColor = Color.FromArgb(64, 64, 64);
            lblDate.Location = new Point(20, 38);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(117, 23);
            lblDate.TabIndex = 1;
            lblDate.Text = "📅  Ngày đặt:";
            // 
            // lblOrderCode
            // 
            lblOrderCode.AutoSize = true;
            lblOrderCode.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblOrderCode.ForeColor = Color.FromArgb(70, 130, 180);
            lblOrderCode.Location = new Point(20, 10);
            lblOrderCode.Name = "lblOrderCode";
            lblOrderCode.Size = new Size(117, 25);
            lblOrderCode.TabIndex = 0;
            lblOrderCode.Text = "\U0001f6d2  Mã đơn:";
            // 
            // pnlGrid
            // 
            pnlGrid.BackColor = Color.White;
            pnlGrid.Controls.Add(gridItems);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(0, 255);
            pnlGrid.Margin = new Padding(3, 4, 3, 4);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(15, 10, 15, 10);
            pnlGrid.Size = new Size(914, 526);
            pnlGrid.TabIndex = 2;
            // 
            // gridItems
            // 
            gridItems.AllowUserToAddRows = false;
            gridItems.BackgroundColor = Color.White;
            gridItems.BorderStyle = BorderStyle.None;
            gridItems.ColumnHeadersHeight = 35;
            gridItems.Dock = DockStyle.Fill;
            gridItems.Location = new Point(15, 10);
            gridItems.Margin = new Padding(3, 4, 3, 4);
            gridItems.Name = "gridItems";
            gridItems.ReadOnly = true;
            gridItems.RowHeadersWidth = 51;
            gridItems.RowTemplate.Height = 35;
            gridItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridItems.Size = new Size(884, 506);
            gridItems.TabIndex = 0;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(248, 249, 250);
            pnlFooter.Controls.Add(pnlActions);
            pnlFooter.Controls.Add(btnPrint);
            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Controls.Add(lblTotal);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 781);
            pnlFooter.Margin = new Padding(3, 4, 3, 4);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(17, 15, 17, 15);
            pnlFooter.Size = new Size(914, 100);
            pnlFooter.TabIndex = 3;
            // 
            // pnlActions
            // 
            pnlActions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pnlActions.Controls.Add(btnPayVietQR);
            pnlActions.Controls.Add(btnCheckPayment);
            pnlActions.Controls.Add(btnCancelOrder);
            pnlActions.Location = new Point(17, 35);
            pnlActions.Margin = new Padding(3, 4, 3, 4);
            pnlActions.Name = "pnlActions";
            pnlActions.Size = new Size(530, 42);
            pnlActions.TabIndex = 2;
            pnlActions.Visible = false;
            // 
            // btnPayVietQR
            // 
            btnPayVietQR.BackColor = Color.FromArgb(0, 123, 255);
            btnPayVietQR.Cursor = Cursors.Hand;
            btnPayVietQR.FlatAppearance.BorderSize = 0;
            btnPayVietQR.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 105, 217);
            btnPayVietQR.FlatStyle = FlatStyle.Flat;
            btnPayVietQR.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnPayVietQR.ForeColor = Color.White;
            btnPayVietQR.Location = new Point(0, 0);
            btnPayVietQR.Margin = new Padding(3, 4, 3, 4);
            btnPayVietQR.Name = "btnPayVietQR";
            btnPayVietQR.Size = new Size(165, 42);
            btnPayVietQR.TabIndex = 0;
            btnPayVietQR.Text = "💳 Thanh toán VietQR";
            btnPayVietQR.UseVisualStyleBackColor = false;
            btnPayVietQR.Visible = false;
            // 
            // btnCheckPayment
            // 
            btnCheckPayment.BackColor = Color.FromArgb(23, 162, 184);
            btnCheckPayment.Cursor = Cursors.Hand;
            btnCheckPayment.FlatAppearance.BorderSize = 0;
            btnCheckPayment.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 140, 160);
            btnCheckPayment.FlatStyle = FlatStyle.Flat;
            btnCheckPayment.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnCheckPayment.ForeColor = Color.White;
            btnCheckPayment.Location = new Point(175, 0);
            btnCheckPayment.Margin = new Padding(3, 4, 3, 4);
            btnCheckPayment.Name = "btnCheckPayment";
            btnCheckPayment.Size = new Size(216, 42);
            btnCheckPayment.TabIndex = 1;
            btnCheckPayment.Text = "🔄 Kiểm tra thanh toán";
            btnCheckPayment.UseVisualStyleBackColor = false;
            btnCheckPayment.Visible = false;
            // 
            // btnCancelOrder
            // 
            btnCancelOrder.BackColor = Color.FromArgb(220, 53, 69);
            btnCancelOrder.Cursor = Cursors.Hand;
            btnCancelOrder.FlatAppearance.BorderSize = 0;
            btnCancelOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 35, 51);
            btnCancelOrder.FlatStyle = FlatStyle.Flat;
            btnCancelOrder.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnCancelOrder.ForeColor = Color.White;
            btnCancelOrder.Location = new Point(397, 0);
            btnCancelOrder.Margin = new Padding(3, 4, 3, 4);
            btnCancelOrder.Name = "btnCancelOrder";
            btnCancelOrder.Size = new Size(130, 42);
            btnCancelOrder.TabIndex = 2;
            btnCancelOrder.Text = "❌ Hủy đơn";
            btnCancelOrder.UseVisualStyleBackColor = false;
            btnCancelOrder.Visible = false;
            // 
            // btnPrint
            // 
            btnPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPrint.BackColor = Color.FromArgb(70, 130, 180);
            btnPrint.Cursor = Cursors.Hand;
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 115, 160);
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnPrint.ForeColor = Color.White;
            btnPrint.Location = new Point(617, 35);
            btnPrint.Margin = new Padding(3, 4, 3, 4);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(140, 42);
            btnPrint.TabIndex = 3;
            btnPrint.Text = "🖨️ In hóa đơn";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Visible = false;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.BackColor = Color.FromArgb(108, 117, 125);
            btnClose.Cursor = Cursors.Hand;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 98, 104);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(767, 35);
            btnClose.Margin = new Padding(3, 4, 3, 4);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(130, 42);
            btnClose.TabIndex = 1;
            btnClose.Text = "✕  Đóng";
            btnClose.UseVisualStyleBackColor = false;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(220, 53, 69);
            lblTotal.Location = new Point(12, -1);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(165, 32);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "💰 Tổng tiền:";
            // 
            // OrderDetailDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(914, 881);
            Controls.Add(pnlGrid);
            Controls.Add(pnlFooter);
            Controls.Add(pnlInfo);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimumSize = new Size(797, 784);
            Name = "OrderDetailDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chi tiết đơn hàng";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlInfo.ResumeLayout(false);
            pnlInfo.PerformLayout();
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridItems).EndInit();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            pnlActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderIcon;
        private Label lblHeaderTitle;
        private Panel pnlInfo;
        private Label lblOrderCode;
        private Label lblDate;
        private Label lblCustomer;
        private Label lblStaff;
        private Label lblPayment;
        private Label lblStatus;
        private Label lblBankTransferInfo;
        private Panel pnlGrid;
        private DataGridView gridItems;
        private Panel pnlFooter;
        private Label lblTotal;
        private Button btnClose;
        private Button btnPrint;
        private Panel pnlActions;
        private Button btnPayVietQR;
        private Button btnCheckPayment;
        private Button btnCancelOrder;
    }
}
