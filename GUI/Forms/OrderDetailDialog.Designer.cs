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
            pnlInfo.SuspendLayout();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridItems).BeginInit();
            pnlFooter.SuspendLayout();
            pnlActions.SuspendLayout();
            SuspendLayout();
            // 
            // pnlInfo
            // 
            pnlInfo.Controls.Add(lblBankTransferInfo);
            pnlInfo.Controls.Add(lblStatus);
            pnlInfo.Controls.Add(lblPayment);
            pnlInfo.Controls.Add(lblStaff);
            pnlInfo.Controls.Add(lblCustomer);
            pnlInfo.Controls.Add(lblDate);
            pnlInfo.Controls.Add(lblOrderCode);
            pnlInfo.Dock = DockStyle.Top;
            pnlInfo.Location = new Point(0, 0);
            pnlInfo.Margin = new Padding(3, 4, 3, 4);
            pnlInfo.Name = "pnlInfo";
            pnlInfo.Padding = new Padding(17, 20, 17, 20);
            pnlInfo.Size = new Size(914, 213);
            pnlInfo.TabIndex = 0;
            // 
            // lblBankTransferInfo
            // 
            lblBankTransferInfo.Font = new Font("Arial", 9F);
            lblBankTransferInfo.ForeColor = Color.FromArgb(70, 130, 180);
            lblBankTransferInfo.Location = new Point(17, 193);
            lblBankTransferInfo.Name = "lblBankTransferInfo";
            lblBankTransferInfo.Size = new Size(400, 107);
            lblBankTransferInfo.TabIndex = 6;
            lblBankTransferInfo.Visible = false;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblStatus.Location = new Point(17, 160);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(83, 18);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "Trạng thái:";
            // 
            // lblPayment
            // 
            lblPayment.AutoSize = true;
            lblPayment.Location = new Point(17, 133);
            lblPayment.Name = "lblPayment";
            lblPayment.Size = new Size(116, 20);
            lblPayment.TabIndex = 4;
            lblPayment.Text = "Phương thức TT:";
            // 
            // lblStaff
            // 
            lblStaff.AutoSize = true;
            lblStaff.Location = new Point(17, 107);
            lblStaff.Name = "lblStaff";
            lblStaff.Size = new Size(78, 20);
            lblStaff.TabIndex = 3;
            lblStaff.Text = "Nhân viên:";
            // 
            // lblCustomer
            // 
            lblCustomer.AutoSize = true;
            lblCustomer.Location = new Point(17, 80);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(89, 20);
            lblCustomer.TabIndex = 2;
            lblCustomer.Text = "Khách hàng:";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(17, 53);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(77, 20);
            lblDate.TabIndex = 1;
            lblDate.Text = "Ngày đơn:";
            // 
            // lblOrderCode
            // 
            lblOrderCode.AutoSize = true;
            lblOrderCode.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblOrderCode.Location = new Point(17, 20);
            lblOrderCode.Name = "lblOrderCode";
            lblOrderCode.Size = new Size(74, 19);
            lblOrderCode.TabIndex = 0;
            lblOrderCode.Text = "Mã đơn:";
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(gridItems);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(0, 213);
            pnlGrid.Margin = new Padding(3, 4, 3, 4);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(11, 13, 11, 13);
            pnlGrid.Size = new Size(914, 561);
            pnlGrid.TabIndex = 1;
            // 
            // gridItems
            // 
            gridItems.AllowUserToAddRows = false;
            gridItems.ColumnHeadersHeight = 29;
            gridItems.Dock = DockStyle.Fill;
            gridItems.Location = new Point(11, 13);
            gridItems.Margin = new Padding(3, 4, 3, 4);
            gridItems.Name = "gridItems";
            gridItems.ReadOnly = true;
            gridItems.RowHeadersWidth = 51;
            gridItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridItems.Size = new Size(892, 535);
            gridItems.TabIndex = 0;
            // 
            // pnlFooter
            // 
            pnlFooter.Controls.Add(pnlActions);
            pnlFooter.Controls.Add(btnPrint);
            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Controls.Add(lblTotal);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 774);
            pnlFooter.Margin = new Padding(3, 4, 3, 4);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(17, 20, 17, 20);
            pnlFooter.Size = new Size(914, 93);
            pnlFooter.TabIndex = 2;
            // 
            // pnlActions
            // 
            pnlActions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pnlActions.Controls.Add(btnPayVietQR);
            pnlActions.Controls.Add(btnCheckPayment);
            pnlActions.Controls.Add(btnCancelOrder);
            pnlActions.Location = new Point(17, 20);
            pnlActions.Margin = new Padding(3, 4, 3, 4);
            pnlActions.Name = "pnlActions";
            pnlActions.Size = new Size(571, 47);
            pnlActions.TabIndex = 2;
            pnlActions.Visible = false;
            // 
            // btnPayVietQR
            // 
            btnPayVietQR.Location = new Point(0, 0);
            btnPayVietQR.Margin = new Padding(3, 4, 3, 4);
            btnPayVietQR.Name = "btnPayVietQR";
            btnPayVietQR.Size = new Size(183, 47);
            btnPayVietQR.TabIndex = 0;
            btnPayVietQR.Text = "💳 Thanh toán VietQR";
            btnPayVietQR.UseVisualStyleBackColor = true;
            btnPayVietQR.Visible = false;
            // 
            // btnCheckPayment
            // 
            btnCheckPayment.Location = new Point(189, 0);
            btnCheckPayment.Margin = new Padding(3, 4, 3, 4);
            btnCheckPayment.Name = "btnCheckPayment";
            btnCheckPayment.Size = new Size(183, 47);
            btnCheckPayment.TabIndex = 1;
            btnCheckPayment.Text = "🔄 Kiểm tra thanh toán";
            btnCheckPayment.UseVisualStyleBackColor = true;
            btnCheckPayment.Visible = false;
            // 
            // btnCancelOrder
            // 
            btnCancelOrder.BackColor = Color.FromArgb(220, 53, 69);
            btnCancelOrder.ForeColor = Color.White;
            btnCancelOrder.Location = new Point(377, 0);
            btnCancelOrder.Margin = new Padding(3, 4, 3, 4);
            btnCancelOrder.Name = "btnCancelOrder";
            btnCancelOrder.Size = new Size(137, 47);
            btnCancelOrder.TabIndex = 2;
            btnCancelOrder.Text = "❌ Hủy đơn";
            btnCancelOrder.UseVisualStyleBackColor = false;
            btnCancelOrder.Visible = false;
            // 
            // btnPrint
            // 
            btnPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPrint.BackColor = Color.FromArgb(70, 130, 180);
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.Font = new Font("Arial", 10F, FontStyle.Bold);
            btnPrint.ForeColor = Color.White;
            btnPrint.Location = new Point(617, 20);
            btnPrint.Margin = new Padding(3, 4, 3, 4);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(143, 47);
            btnPrint.TabIndex = 3;
            btnPrint.Text = "🖨️ In hóa đơn";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Visible = false;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(780, 20);
            btnClose.Margin = new Padding(3, 4, 3, 4);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(114, 47);
            btnClose.TabIndex = 1;
            btnClose.Text = "Đóng";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblTotal.ForeColor = Color.Red;
            lblTotal.Location = new Point(17, 27);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(107, 24);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "Tổng tiền:";
            // 
            // OrderDetailDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 867);
            Controls.Add(pnlGrid);
            Controls.Add(pnlFooter);
            Controls.Add(pnlInfo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimumSize = new Size(797, 784);
            Name = "OrderDetailDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chi tiết đơn hàng";
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
