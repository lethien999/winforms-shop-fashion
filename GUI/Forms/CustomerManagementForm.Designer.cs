namespace WinFormsFashionShop.Presentation.Forms
{
    partial class CustomerManagementForm
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
            lblTitle = new Label();
            pnlSearch = new Panel();
            txtSearch = new TextBox();
            btnSearch = new Button();
            splitContainer = new SplitContainer();
            gridCustomers = new DataGridView();
            gridOrders = new DataGridView();
            lblHistory = new Label();
            pnlButtons = new Panel();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnViewHistory = new Button();
            btnRefresh = new Button();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridCustomers).BeginInit();
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
            picLogo.Margin = new Padding(0);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(36, 36);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(56, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(328, 27);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "👥 QUẢN LÝ KHÁCH HÀNG";
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = Color.White;
            pnlSearch.BorderStyle = BorderStyle.FixedSingle;
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 60);
            pnlSearch.Margin = new Padding(0);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(12, 10, 12, 10);
            pnlSearch.Size = new Size(1000, 70);
            pnlSearch.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.Font = new Font("Arial", 10F);
            txtSearch.Location = new Point(12, 18);
            txtSearch.Margin = new Padding(0);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Tìm theo tên/SĐT...";
            txtSearch.Size = new Size(820, 28);
            txtSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.BackColor = Color.FromArgb(0, 123, 255);
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 105, 217);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Arial", 10F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(840, 18);
            btnSearch.Margin = new Padding(0);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(148, 28);
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 130);
            splitContainer.Margin = new Padding(0);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(gridCustomers);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(gridOrders);
            splitContainer.Panel2.Controls.Add(lblHistory);
            splitContainer.Size = new Size(1000, 530);
            splitContainer.SplitterDistance = 497;
            splitContainer.SplitterWidth = 5;
            splitContainer.TabIndex = 1;
            // 
            // gridCustomers
            // 
            gridCustomers.AllowUserToAddRows = false;
            gridCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridCustomers.ColumnHeadersHeight = 32;
            gridCustomers.Dock = DockStyle.Fill;
            gridCustomers.Location = new Point(0, 0);
            gridCustomers.Margin = new Padding(0);
            gridCustomers.Name = "gridCustomers";
            gridCustomers.ReadOnly = true;
            gridCustomers.RowHeadersWidth = 40;
            gridCustomers.RowTemplate.Height = 32;
            gridCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridCustomers.Size = new Size(497, 530);
            gridCustomers.TabIndex = 0;
            // 
            // gridOrders
            // 
            gridOrders.AllowUserToAddRows = false;
            gridOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridOrders.ColumnHeadersHeight = 32;
            gridOrders.Dock = DockStyle.Fill;
            gridOrders.Location = new Point(0, 28);
            gridOrders.Margin = new Padding(0);
            gridOrders.Name = "gridOrders";
            gridOrders.ReadOnly = true;
            gridOrders.RowHeadersWidth = 40;
            gridOrders.RowTemplate.Height = 32;
            gridOrders.Size = new Size(498, 502);
            gridOrders.TabIndex = 0;
            // 
            // lblHistory
            // 
            lblHistory.Dock = DockStyle.Top;
            lblHistory.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblHistory.Location = new Point(0, 0);
            lblHistory.Name = "lblHistory";
            lblHistory.Size = new Size(498, 28);
            lblHistory.TabIndex = 1;
            lblHistory.Text = "Lịch sử mua hàng:";
            lblHistory.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnAdd);
            pnlButtons.Controls.Add(btnEdit);
            pnlButtons.Controls.Add(btnDelete);
            pnlButtons.Controls.Add(btnViewHistory);
            pnlButtons.Controls.Add(btnRefresh);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Location = new Point(0, 660);
            pnlButtons.Margin = new Padding(0);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Padding = new Padding(12, 8, 12, 8);
            pnlButtons.Size = new Size(1000, 60);
            pnlButtons.TabIndex = 2;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 12);
            btnAdd.Margin = new Padding(0);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 35);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(118, 12);
            btnEdit.Margin = new Padding(0);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(100, 35);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Sửa";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(224, 12);
            btnDelete.Margin = new Padding(0);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 35);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnViewHistory
            // 
            btnViewHistory.Location = new Point(330, 12);
            btnViewHistory.Margin = new Padding(0);
            btnViewHistory.Name = "btnViewHistory";
            btnViewHistory.Size = new Size(100, 35);
            btnViewHistory.TabIndex = 3;
            btnViewHistory.Text = "Xem lịch sử";
            btnViewHistory.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(436, 12);
            btnRefresh.Margin = new Padding(0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 35);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Làm mới";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // CustomerManagementForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 250);
            ClientSize = new Size(1000, 720);
            Controls.Add(splitContainer);
            Controls.Add(pnlButtons);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
            Margin = new Padding(0);
            MinimumSize = new Size(900, 600);
            Name = "CustomerManagementForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản lý Khách hàng";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridCustomers).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridOrders).EndInit();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView gridCustomers;
        private System.Windows.Forms.Label lblHistory;
        private System.Windows.Forms.DataGridView gridOrders;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnViewHistory;
        private System.Windows.Forms.Button btnRefresh;
    }
}
