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
            lblOrderCode = new Label();
            lblDate = new Label();
            lblCustomer = new Label();
            lblStaff = new Label();
            lblPayment = new Label();
            lblStatus = new Label();
            lblBankTransferInfo = new Label();
            pnlGrid = new Panel();
            gridItems = new DataGridView();
            pnlFooter = new Panel();
            lblTotal = new Label();
            btnClose = new Button();
            pnlActions = new Panel();
            btnPayVietQR = new Button();
            btnCheckPayment = new Button();
            btnCancelOrder = new Button();
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
            pnlInfo.Name = "pnlInfo";
            pnlInfo.Padding = new Padding(15);
            pnlInfo.Size = new Size(800, 160);
            pnlInfo.TabIndex = 0;
            // 
            // lblOrderCode
            // 
            lblOrderCode.AutoSize = true;
            lblOrderCode.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblOrderCode.Location = new Point(15, 15);
            lblOrderCode.Name = "lblOrderCode";
            lblOrderCode.Size = new Size(100, 16);
            lblOrderCode.TabIndex = 0;
            lblOrderCode.Text = "Mã đơn:";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(15, 40);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(100, 15);
            lblDate.TabIndex = 1;
            lblDate.Text = "Ngày đơn:";
            // 
            // lblCustomer
            // 
            lblCustomer.AutoSize = true;
            lblCustomer.Location = new Point(15, 60);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(100, 15);
            lblCustomer.TabIndex = 2;
            lblCustomer.Text = "Khách hàng:";
            // 
            // lblStaff
            // 
            lblStaff.AutoSize = true;
            lblStaff.Location = new Point(15, 80);
            lblStaff.Name = "lblStaff";
            lblStaff.Size = new Size(100, 15);
            lblStaff.TabIndex = 3;
            lblStaff.Text = "Nhân viên:";
            // 
            // lblPayment
            // 
            lblPayment.AutoSize = true;
            lblPayment.Location = new Point(15, 100);
            lblPayment.Name = "lblPayment";
            lblPayment.Size = new Size(100, 15);
            lblPayment.TabIndex = 4;
            lblPayment.Text = "Phương thức TT:";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblStatus.Location = new Point(15, 120);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(100, 15);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "Trạng thái:";
            // 
            // lblBankTransferInfo
            // 
            lblBankTransferInfo.AutoSize = false;
            lblBankTransferInfo.Font = new Font("Arial", 9F);
            lblBankTransferInfo.ForeColor = Color.FromArgb(70, 130, 180);
            lblBankTransferInfo.Location = new Point(15, 145);
            lblBankTransferInfo.Name = "lblBankTransferInfo";
            lblBankTransferInfo.Size = new Size(350, 80);
            lblBankTransferInfo.TabIndex = 6;
            lblBankTransferInfo.Visible = false;
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(gridItems);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(0, 160);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(10);
            pnlGrid.Size = new Size(800, 420);
            pnlGrid.TabIndex = 1;
            // 
            // gridItems
            // 
            gridItems.AllowUserToAddRows = false;
            gridItems.AutoGenerateColumns = false;
            gridItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            gridItems.Dock = DockStyle.Fill;
            gridItems.Location = new Point(10, 10);
            gridItems.Name = "gridItems";
            gridItems.ReadOnly = true;
            gridItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridItems.Size = new Size(780, 400);
            gridItems.TabIndex = 0;
            // 
            // pnlFooter
            // 
            pnlFooter.Controls.Add(pnlActions);
            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Controls.Add(lblTotal);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 580);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(15);
            pnlFooter.Size = new Size(800, 70);
            pnlFooter.TabIndex = 2;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblTotal.ForeColor = Color.Red;
            lblTotal.Location = new Point(15, 20);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(100, 19);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "Tổng tiền:";
            // 
            // btnClose
            // 
            // 
            // pnlActions
            // 
            pnlActions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pnlActions.Controls.Add(btnPayVietQR);
            pnlActions.Controls.Add(btnCheckPayment);
            pnlActions.Controls.Add(btnCancelOrder);
            pnlActions.Location = new Point(15, 15);
            pnlActions.Name = "pnlActions";
            pnlActions.Size = new Size(500, 35);
            pnlActions.TabIndex = 2;
            pnlActions.Visible = false;
            // 
            // btnPayVietQR
            // 
            btnPayVietQR.Location = new Point(0, 0);
            btnPayVietQR.Name = "btnPayVietQR";
            btnPayVietQR.Size = new Size(160, 35);
            btnPayVietQR.TabIndex = 0;
            btnPayVietQR.Text = "💳 Thanh toán VietQR";
            btnPayVietQR.UseVisualStyleBackColor = true;
            btnPayVietQR.Visible = false;
            // 
            // btnCheckPayment
            // 
            btnCheckPayment.Location = new Point(165, 0);
            btnCheckPayment.Name = "btnCheckPayment";
            btnCheckPayment.Size = new Size(160, 35);
            btnCheckPayment.TabIndex = 1;
            btnCheckPayment.Text = "🔄 Kiểm tra thanh toán";
            btnCheckPayment.UseVisualStyleBackColor = true;
            btnCheckPayment.Visible = false;
            // 
            // btnCancelOrder
            // 
            btnCancelOrder.BackColor = Color.FromArgb(220, 53, 69);
            btnCancelOrder.ForeColor = Color.White;
            btnCancelOrder.Location = new Point(330, 0);
            btnCancelOrder.Name = "btnCancelOrder";
            btnCancelOrder.Size = new Size(120, 35);
            btnCancelOrder.TabIndex = 2;
            btnCancelOrder.Text = "❌ Hủy đơn";
            btnCancelOrder.UseVisualStyleBackColor = false;
            btnCancelOrder.Visible = false;
            //  
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(650, 15);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 35);
            btnClose.TabIndex = 1;
            btnClose.Text = "Đóng";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // OrderDetailDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 650);
            Controls.Add(pnlGrid);
            Controls.Add(pnlFooter);
            Controls.Add(pnlInfo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimumSize = new Size(700, 600);
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
        private Panel pnlActions;
        private Button btnPayVietQR;
        private Button btnCheckPayment;
        private Button btnCancelOrder;
    }
}
