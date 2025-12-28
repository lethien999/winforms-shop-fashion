namespace WinFormsFashionShop.Presentation.Forms
{
    partial class ReportForm
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
            this.lblHeaderIcon = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabRevenue = new System.Windows.Forms.TabPage();
            this.pnlRevenue = new System.Windows.Forms.Panel();
            this.gridRevenue = new System.Windows.Forms.DataGridView();
            this.pnlRevenueFilter = new System.Windows.Forms.Panel();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnLoadRevenue = new System.Windows.Forms.Button();
            this.lblRevenueTotal = new System.Windows.Forms.Label();
            this.tabInventory = new System.Windows.Forms.TabPage();
            this.pnlInventory = new System.Windows.Forms.Panel();
            this.gridInventory = new System.Windows.Forms.DataGridView();
            this.pnlInventoryFilter = new System.Windows.Forms.Panel();
            this.lblThreshold = new System.Windows.Forms.Label();
            this.numLowStockThreshold = new System.Windows.Forms.NumericUpDown();
            this.btnLoadInventory = new System.Windows.Forms.Button();
            this.tabCustomers = new System.Windows.Forms.TabPage();
            this.pnlCustomers = new System.Windows.Forms.Panel();
            this.gridCustomers = new System.Windows.Forms.DataGridView();
            this.pnlCustomersFilter = new System.Windows.Forms.Panel();
            this.lblTopN = new System.Windows.Forms.Label();
            this.numTopCustomers = new System.Windows.Forms.NumericUpDown();
            this.btnLoadCustomers = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabRevenue.SuspendLayout();
            this.pnlRevenue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRevenue)).BeginInit();
            this.pnlRevenueFilter.SuspendLayout();
            this.tabInventory.SuspendLayout();
            this.pnlInventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInventory)).BeginInit();
            this.pnlInventoryFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLowStockThreshold)).BeginInit();
            this.tabCustomers.SuspendLayout();
            this.pnlCustomers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).BeginInit();
            this.pnlCustomersFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTopCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
            this.pnlHeader.Controls.Add(this.lblHeaderIcon);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 65);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeaderIcon
            // 
            this.lblHeaderIcon.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderIcon.ForeColor = System.Drawing.Color.White;
            this.lblHeaderIcon.Location = new System.Drawing.Point(15, 8);
            this.lblHeaderIcon.Name = "lblHeaderIcon";
            this.lblHeaderIcon.Size = new System.Drawing.Size(55, 50);
            this.lblHeaderIcon.TabIndex = 0;
            this.lblHeaderIcon.Text = "📊";
            this.lblHeaderIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(70, 18);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(200, 30);
            this.lblHeaderTitle.TabIndex = 1;
            this.lblHeaderTitle.Text = "Báo cáo thống kê";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabRevenue);
            this.tabControl.Controls.Add(this.tabInventory);
            this.tabControl.Controls.Add(this.tabCustomers);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabControl.Location = new System.Drawing.Point(0, 65);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 585);
            this.tabControl.TabIndex = 1;
            // 
            // tabRevenue
            // 
            this.tabRevenue.Controls.Add(this.pnlRevenue);
            this.tabRevenue.Location = new System.Drawing.Point(4, 28);
            this.tabRevenue.Name = "tabRevenue";
            this.tabRevenue.Padding = new System.Windows.Forms.Padding(3);
            this.tabRevenue.Size = new System.Drawing.Size(992, 553);
            this.tabRevenue.TabIndex = 0;
            this.tabRevenue.Text = "📈 Doanh thu";
            this.tabRevenue.UseVisualStyleBackColor = true;
            // 
            // pnlRevenue
            // 
            this.pnlRevenue.Controls.Add(this.gridRevenue);
            this.pnlRevenue.Controls.Add(this.pnlRevenueFilter);
            this.pnlRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRevenue.Location = new System.Drawing.Point(3, 3);
            this.pnlRevenue.Name = "pnlRevenue";
            this.pnlRevenue.Size = new System.Drawing.Size(986, 547);
            this.pnlRevenue.TabIndex = 0;
            // 
            // gridRevenue
            // 
            this.gridRevenue.AllowUserToAddRows = false;
            this.gridRevenue.BackgroundColor = System.Drawing.Color.White;
            this.gridRevenue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridRevenue.ColumnHeadersHeight = 35;
            this.gridRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRevenue.Location = new System.Drawing.Point(0, 70);
            this.gridRevenue.Name = "gridRevenue";
            this.gridRevenue.ReadOnly = true;
            this.gridRevenue.RowTemplate.Height = 30;
            this.gridRevenue.Size = new System.Drawing.Size(986, 477);
            this.gridRevenue.TabIndex = 1;
            // 
            // pnlRevenueFilter
            // 
            this.pnlRevenueFilter.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlRevenueFilter.Controls.Add(this.lblFrom);
            this.pnlRevenueFilter.Controls.Add(this.dtpFrom);
            this.pnlRevenueFilter.Controls.Add(this.lblTo);
            this.pnlRevenueFilter.Controls.Add(this.dtpTo);
            this.pnlRevenueFilter.Controls.Add(this.btnLoadRevenue);
            this.pnlRevenueFilter.Controls.Add(this.lblRevenueTotal);
            this.pnlRevenueFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRevenueFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlRevenueFilter.Name = "pnlRevenueFilter";
            this.pnlRevenueFilter.Size = new System.Drawing.Size(986, 70);
            this.pnlRevenueFilter.TabIndex = 0;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblFrom.Location = new System.Drawing.Point(15, 25);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(90, 19);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "📅 Từ ngày:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpFrom.Location = new System.Drawing.Point(110, 22);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(160, 25);
            this.dtpFrom.TabIndex = 1;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblTo.Location = new System.Drawing.Point(290, 25);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(96, 19);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "📅 Đến ngày:";
            // 
            // dtpTo
            // 
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpTo.Location = new System.Drawing.Point(390, 22);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(160, 25);
            this.dtpTo.TabIndex = 3;
            // 
            // btnLoadRevenue
            // 
            this.btnLoadRevenue.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnLoadRevenue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadRevenue.FlatAppearance.BorderSize = 0;
            this.btnLoadRevenue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 105, 217);
            this.btnLoadRevenue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadRevenue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLoadRevenue.ForeColor = System.Drawing.Color.White;
            this.btnLoadRevenue.Location = new System.Drawing.Point(570, 18);
            this.btnLoadRevenue.Name = "btnLoadRevenue";
            this.btnLoadRevenue.Size = new System.Drawing.Size(130, 35);
            this.btnLoadRevenue.TabIndex = 4;
            this.btnLoadRevenue.Text = "📥 Tải báo cáo";
            this.btnLoadRevenue.UseVisualStyleBackColor = false;
            // 
            // lblRevenueTotal
            // 
            this.lblRevenueTotal.AutoSize = true;
            this.lblRevenueTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblRevenueTotal.ForeColor = System.Drawing.Color.FromArgb(34, 139, 34);
            this.lblRevenueTotal.Location = new System.Drawing.Point(720, 22);
            this.lblRevenueTotal.Name = "lblRevenueTotal";
            this.lblRevenueTotal.Size = new System.Drawing.Size(200, 21);
            this.lblRevenueTotal.TabIndex = 5;
            this.lblRevenueTotal.Text = "💰 Tổng doanh thu: 0 VNĐ";
            // 
            // tabInventory
            // 
            this.tabInventory.Controls.Add(this.pnlInventory);
            this.tabInventory.Location = new System.Drawing.Point(4, 28);
            this.tabInventory.Name = "tabInventory";
            this.tabInventory.Padding = new System.Windows.Forms.Padding(3);
            this.tabInventory.Size = new System.Drawing.Size(992, 553);
            this.tabInventory.TabIndex = 1;
            this.tabInventory.Text = "📦 Tồn kho";
            this.tabInventory.UseVisualStyleBackColor = true;
            // 
            // pnlInventory
            // 
            this.pnlInventory.Controls.Add(this.gridInventory);
            this.pnlInventory.Controls.Add(this.pnlInventoryFilter);
            this.pnlInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInventory.Location = new System.Drawing.Point(3, 3);
            this.pnlInventory.Name = "pnlInventory";
            this.pnlInventory.Size = new System.Drawing.Size(986, 547);
            this.pnlInventory.TabIndex = 0;
            // 
            // gridInventory
            // 
            this.gridInventory.AllowUserToAddRows = false;
            this.gridInventory.BackgroundColor = System.Drawing.Color.White;
            this.gridInventory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridInventory.ColumnHeadersHeight = 35;
            this.gridInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridInventory.Location = new System.Drawing.Point(0, 70);
            this.gridInventory.Name = "gridInventory";
            this.gridInventory.ReadOnly = true;
            this.gridInventory.RowTemplate.Height = 30;
            this.gridInventory.Size = new System.Drawing.Size(986, 477);
            this.gridInventory.TabIndex = 1;
            // 
            // pnlInventoryFilter
            // 
            this.pnlInventoryFilter.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlInventoryFilter.Controls.Add(this.lblThreshold);
            this.pnlInventoryFilter.Controls.Add(this.numLowStockThreshold);
            this.pnlInventoryFilter.Controls.Add(this.btnLoadInventory);
            this.pnlInventoryFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInventoryFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlInventoryFilter.Name = "pnlInventoryFilter";
            this.pnlInventoryFilter.Size = new System.Drawing.Size(986, 70);
            this.pnlInventoryFilter.TabIndex = 0;
            // 
            // lblThreshold
            // 
            this.lblThreshold.AutoSize = true;
            this.lblThreshold.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblThreshold.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblThreshold.Location = new System.Drawing.Point(15, 25);
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.Size = new System.Drawing.Size(140, 19);
            this.lblThreshold.TabIndex = 0;
            this.lblThreshold.Text = "⚠️ Ngưỡng tồn thấp:";
            // 
            // numLowStockThreshold
            // 
            this.numLowStockThreshold.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numLowStockThreshold.Location = new System.Drawing.Point(165, 22);
            this.numLowStockThreshold.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numLowStockThreshold.Name = "numLowStockThreshold";
            this.numLowStockThreshold.Size = new System.Drawing.Size(100, 25);
            this.numLowStockThreshold.TabIndex = 1;
            this.numLowStockThreshold.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnLoadInventory
            // 
            this.btnLoadInventory.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnLoadInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadInventory.FlatAppearance.BorderSize = 0;
            this.btnLoadInventory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 105, 217);
            this.btnLoadInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadInventory.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLoadInventory.ForeColor = System.Drawing.Color.White;
            this.btnLoadInventory.Location = new System.Drawing.Point(290, 18);
            this.btnLoadInventory.Name = "btnLoadInventory";
            this.btnLoadInventory.Size = new System.Drawing.Size(130, 35);
            this.btnLoadInventory.TabIndex = 2;
            this.btnLoadInventory.Text = "📥 Tải báo cáo";
            this.btnLoadInventory.UseVisualStyleBackColor = false;
            // 
            // tabCustomers
            // 
            this.tabCustomers.Controls.Add(this.pnlCustomers);
            this.tabCustomers.Location = new System.Drawing.Point(4, 28);
            this.tabCustomers.Name = "tabCustomers";
            this.tabCustomers.Size = new System.Drawing.Size(992, 553);
            this.tabCustomers.TabIndex = 2;
            this.tabCustomers.Text = "👥 Khách hàng";
            this.tabCustomers.UseVisualStyleBackColor = true;
            // 
            // pnlCustomers
            // 
            this.pnlCustomers.Controls.Add(this.gridCustomers);
            this.pnlCustomers.Controls.Add(this.pnlCustomersFilter);
            this.pnlCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCustomers.Location = new System.Drawing.Point(0, 0);
            this.pnlCustomers.Name = "pnlCustomers";
            this.pnlCustomers.Size = new System.Drawing.Size(992, 553);
            this.pnlCustomers.TabIndex = 0;
            // 
            // gridCustomers
            // 
            this.gridCustomers.AllowUserToAddRows = false;
            this.gridCustomers.BackgroundColor = System.Drawing.Color.White;
            this.gridCustomers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridCustomers.ColumnHeadersHeight = 35;
            this.gridCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCustomers.Location = new System.Drawing.Point(0, 70);
            this.gridCustomers.Name = "gridCustomers";
            this.gridCustomers.ReadOnly = true;
            this.gridCustomers.RowTemplate.Height = 30;
            this.gridCustomers.Size = new System.Drawing.Size(992, 483);
            this.gridCustomers.TabIndex = 1;
            // 
            // pnlCustomersFilter
            // 
            this.pnlCustomersFilter.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlCustomersFilter.Controls.Add(this.lblTopN);
            this.pnlCustomersFilter.Controls.Add(this.numTopCustomers);
            this.pnlCustomersFilter.Controls.Add(this.btnLoadCustomers);
            this.pnlCustomersFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCustomersFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlCustomersFilter.Name = "pnlCustomersFilter";
            this.pnlCustomersFilter.Size = new System.Drawing.Size(992, 70);
            this.pnlCustomersFilter.TabIndex = 0;
            // 
            // lblTopN
            // 
            this.lblTopN.AutoSize = true;
            this.lblTopN.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTopN.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblTopN.Location = new System.Drawing.Point(15, 25);
            this.lblTopN.Name = "lblTopN";
            this.lblTopN.Size = new System.Drawing.Size(145, 19);
            this.lblTopN.TabIndex = 0;
            this.lblTopN.Text = "🏆 Top N khách hàng:";
            // 
            // numTopCustomers
            // 
            this.numTopCustomers.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numTopCustomers.Location = new System.Drawing.Point(170, 22);
            this.numTopCustomers.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numTopCustomers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTopCustomers.Name = "numTopCustomers";
            this.numTopCustomers.Size = new System.Drawing.Size(100, 25);
            this.numTopCustomers.TabIndex = 1;
            this.numTopCustomers.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnLoadCustomers
            // 
            this.btnLoadCustomers.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnLoadCustomers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadCustomers.FlatAppearance.BorderSize = 0;
            this.btnLoadCustomers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 105, 217);
            this.btnLoadCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadCustomers.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLoadCustomers.ForeColor = System.Drawing.Color.White;
            this.btnLoadCustomers.Location = new System.Drawing.Point(295, 18);
            this.btnLoadCustomers.Name = "btnLoadCustomers";
            this.btnLoadCustomers.Size = new System.Drawing.Size(130, 35);
            this.btnLoadCustomers.TabIndex = 2;
            this.btnLoadCustomers.Text = "📥 Tải báo cáo";
            this.btnLoadCustomers.UseVisualStyleBackColor = false;
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Báo cáo thống kê";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabRevenue.ResumeLayout(false);
            this.pnlRevenue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRevenue)).EndInit();
            this.pnlRevenueFilter.ResumeLayout(false);
            this.pnlRevenueFilter.PerformLayout();
            this.tabInventory.ResumeLayout(false);
            this.pnlInventory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridInventory)).EndInit();
            this.pnlInventoryFilter.ResumeLayout(false);
            this.pnlInventoryFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLowStockThreshold)).EndInit();
            this.tabCustomers.ResumeLayout(false);
            this.pnlCustomers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).EndInit();
            this.pnlCustomersFilter.ResumeLayout(false);
            this.pnlCustomersFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTopCustomers)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderIcon;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabRevenue;
        private System.Windows.Forms.Panel pnlRevenue;
        private System.Windows.Forms.DataGridView gridRevenue;
        private System.Windows.Forms.Panel pnlRevenueFilter;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnLoadRevenue;
        private System.Windows.Forms.Label lblRevenueTotal;
        private System.Windows.Forms.TabPage tabInventory;
        private System.Windows.Forms.Panel pnlInventory;
        private System.Windows.Forms.DataGridView gridInventory;
        private System.Windows.Forms.Panel pnlInventoryFilter;
        private System.Windows.Forms.Label lblThreshold;
        private System.Windows.Forms.NumericUpDown numLowStockThreshold;
        private System.Windows.Forms.Button btnLoadInventory;
        private System.Windows.Forms.TabPage tabCustomers;
        private System.Windows.Forms.Panel pnlCustomers;
        private System.Windows.Forms.DataGridView gridCustomers;
        private System.Windows.Forms.Panel pnlCustomersFilter;
        private System.Windows.Forms.Label lblTopN;
        private System.Windows.Forms.NumericUpDown numTopCustomers;
        private System.Windows.Forms.Button btnLoadCustomers;
    }
}
