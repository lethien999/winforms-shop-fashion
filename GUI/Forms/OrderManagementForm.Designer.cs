namespace WinFormsFashionShop.Presentation.Forms
{
    partial class OrderManagementForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            pnlHeader = new Panel();
            picLogo = new PictureBox();
            lblTitle = new Label();
            pnlSearch = new Panel();
            btnRefresh = new Button();
            btnSearch = new Button();
            dtpTo = new DateTimePicker();
            lblTo = new Label();
            dtpFrom = new DateTimePicker();
            lblFrom = new Label();
            txtSearch = new TextBox();
            lblSearch = new Label();
            gridOrders = new DataGridView();
            pnlButtons = new Panel();
            lblInfo = new Label();
            btnCancelOrder = new Button();
            btnPrint = new Button();
            btnViewDetail = new Button();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridOrders).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(70, 130, 180);
            pnlHeader.Controls.Add(picLogo);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(15, 12, 15, 12);
            pnlHeader.Size = new Size(1000, 60);
            pnlHeader.TabIndex = 0;
            // 
            // picLogo
            // 
            picLogo.Image = GUI.Properties.Resources.Logo_3T;
            picLogo.Location = new Point(15, 12);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(36, 36);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 1;
            picLogo.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(56, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(291, 29);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📋 QUẢN LÝ ĐƠN HÀNG";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = Color.White;
            pnlSearch.BorderStyle = BorderStyle.FixedSingle;
            pnlSearch.Controls.Add(btnRefresh);
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Controls.Add(dtpTo);
            pnlSearch.Controls.Add(lblTo);
            pnlSearch.Controls.Add(dtpFrom);
            pnlSearch.Controls.Add(lblFrom);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 60);
            pnlSearch.Margin = new Padding(0);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(12, 10, 12, 10);
            pnlSearch.Size = new Size(1000, 70);
            pnlSearch.TabIndex = 1;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(108, 117, 125);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(871, 9);
            btnRefresh.Margin = new Padding(0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(98, 28);
            btnRefresh.TabIndex = 7;
            btnRefresh.Text = "🔄 Làm mới";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(0, 123, 255);
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(771, 10);
            btnSearch.Margin = new Padding(0);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(88, 28);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "🔍 Tìm";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // dtpTo
            // 
            dtpTo.Font = new Font("Arial", 9F);
            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.Location = new Point(597, 14);
            dtpTo.Margin = new Padding(0);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(160, 25);
            dtpTo.TabIndex = 5;
            // 
            // lblTo
            // 
            lblTo.Font = new Font("Arial", 9F);
            lblTo.Location = new Point(539, 17);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(70, 20);
            lblTo.TabIndex = 4;
            lblTo.Text = "📅 Đến:";
            // 
            // dtpFrom
            // 
            dtpFrom.Font = new Font("Arial", 9F);
            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.Location = new Point(376, 17);
            dtpFrom.Margin = new Padding(0);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(160, 25);
            dtpFrom.TabIndex = 3;
            // 
            // lblFrom
            // 
            lblFrom.Font = new Font("Arial", 9F);
            lblFrom.Location = new Point(321, 20);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(70, 20);
            lblFrom.TabIndex = 2;
            lblFrom.Text = "📅 Từ:";
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Arial", 9F);
            txtSearch.Location = new Point(94, 18);
            txtSearch.Margin = new Padding(0);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Nhập mã đơn hàng...";
            txtSearch.Size = new Size(210, 25);
            txtSearch.TabIndex = 1;
            // 
            // lblSearch
            // 
            lblSearch.Font = new Font("Arial", 9F);
            lblSearch.Location = new Point(12, 21);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(105, 20);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "🔍 Mã đơn:";
            // 
            // gridOrders
            // 
            gridOrders.AllowUserToAddRows = false;
            gridOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridOrders.BackgroundColor = Color.White;
            gridOrders.BorderStyle = BorderStyle.None;
            gridOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridOrders.ColumnHeadersHeight = 32;
            gridOrders.Dock = DockStyle.Fill;
            gridOrders.EnableHeadersVisualStyles = false;
            gridOrders.GridColor = Color.FromArgb(230, 230, 230);
            gridOrders.Location = new Point(0, 130);
            gridOrders.Margin = new Padding(0);
            gridOrders.MultiSelect = false;
            gridOrders.Name = "gridOrders";
            gridOrders.ReadOnly = true;
            gridOrders.RowHeadersWidth = 40;
            gridOrders.RowTemplate.Height = 32;
            gridOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridOrders.Size = new Size(1000, 530);
            gridOrders.TabIndex = 2;
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.FromArgb(240, 240, 240);
            pnlButtons.BorderStyle = BorderStyle.FixedSingle;
            pnlButtons.Controls.Add(lblInfo);
            pnlButtons.Controls.Add(btnCancelOrder);
            pnlButtons.Controls.Add(btnPrint);
            pnlButtons.Controls.Add(btnViewDetail);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Location = new Point(0, 660);
            pnlButtons.Margin = new Padding(0);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Padding = new Padding(12, 8, 12, 8);
            pnlButtons.Size = new Size(1000, 60);
            pnlButtons.TabIndex = 3;
            // 
            // lblInfo
            // 
            lblInfo.Dock = DockStyle.Right;
            lblInfo.Font = new Font("Arial", 8F, FontStyle.Italic);
            lblInfo.ForeColor = Color.Gray;
            lblInfo.Location = new Point(558, 8);
            lblInfo.Name = "lblInfo";
            lblInfo.Padding = new Padding(0, 0, 12, 0);
            lblInfo.Size = new Size(428, 42);
            lblInfo.TabIndex = 3;
            lblInfo.Text = "💡 Double-click vào đơn hàng để xem chi tiết";
            lblInfo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCancelOrder
            // 
            btnCancelOrder.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCancelOrder.BackColor = Color.FromArgb(220, 53, 69);
            btnCancelOrder.FlatAppearance.BorderSize = 0;
            btnCancelOrder.FlatStyle = FlatStyle.Flat;
            btnCancelOrder.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnCancelOrder.ForeColor = Color.White;
            btnCancelOrder.Location = new Point(350, 12);
            btnCancelOrder.Margin = new Padding(0);
            btnCancelOrder.Name = "btnCancelOrder";
            btnCancelOrder.Size = new Size(140, 35);
            btnCancelOrder.TabIndex = 2;
            btnCancelOrder.Text = "❌ Hủy đơn";
            btnCancelOrder.UseVisualStyleBackColor = false;
            // 
            // btnPrint
            // 
            btnPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPrint.BackColor = Color.FromArgb(34, 139, 34);
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnPrint.ForeColor = Color.White;
            btnPrint.Location = new Point(200, 12);
            btnPrint.Margin = new Padding(0);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(140, 35);
            btnPrint.TabIndex = 1;
            btnPrint.Text = "🖨️ In hóa đơn";
            btnPrint.UseVisualStyleBackColor = false;
            // 
            // btnViewDetail
            // 
            btnViewDetail.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnViewDetail.BackColor = Color.FromArgb(0, 123, 255);
            btnViewDetail.FlatAppearance.BorderSize = 0;
            btnViewDetail.FlatStyle = FlatStyle.Flat;
            btnViewDetail.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnViewDetail.ForeColor = Color.White;
            btnViewDetail.Location = new Point(50, 12);
            btnViewDetail.Margin = new Padding(0);
            btnViewDetail.Name = "btnViewDetail";
            btnViewDetail.Size = new Size(140, 35);
            btnViewDetail.TabIndex = 0;
            btnViewDetail.Text = "👁️ Xem chi tiết";
            btnViewDetail.UseVisualStyleBackColor = false;
            // 
            // OrderManagementForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 250);
            ClientSize = new Size(1000, 720);
            Controls.Add(gridOrders);
            Controls.Add(pnlButtons);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
            Margin = new Padding(0);
            MinimumSize = new Size(900, 600);
            Name = "OrderManagementForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản lý Đơn hàng";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridOrders).EndInit();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView gridOrders;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnViewDetail;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCancelOrder;
        private System.Windows.Forms.Label lblInfo;
    }
}
