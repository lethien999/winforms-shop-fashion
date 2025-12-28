namespace WinFormsFashionShop.Presentation.Forms
{
    partial class InventoryAdjustmentForm
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
            pnlAdd = new Panel();
            lblProduct = new Label();
            txtProductSearch = new TextBox();
            lblQuantity = new Label();
            numQuantity = new NumericUpDown();
            btnAdd = new Button();
            grid = new DataGridView();
            pnlButtons = new Panel();
            btnSave = new Button();
            btnRefresh = new Button();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
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
            pnlHeader.Padding = new Padding(17, 16, 17, 16);
            pnlHeader.Size = new Size(1029, 80);
            pnlHeader.TabIndex = 0;
            // 
            // picLogo
            // 
            picLogo.Image = GUI.Properties.Resources.Logo_3T;
            picLogo.Location = new Point(17, 16);
            picLogo.Margin = new Padding(3, 4, 3, 4);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(41, 48);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(64, 24);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(322, 29);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "📦 NHẬP HÀNG - TỒN KHO";
            // 
            // pnlAdd
            // 
            pnlAdd.BackColor = Color.FromArgb(248, 249, 250);
            pnlAdd.Controls.Add(lblProduct);
            pnlAdd.Controls.Add(txtProductSearch);
            pnlAdd.Controls.Add(lblQuantity);
            pnlAdd.Controls.Add(numQuantity);
            pnlAdd.Controls.Add(btnAdd);
            pnlAdd.Dock = DockStyle.Top;
            pnlAdd.Location = new Point(0, 80);
            pnlAdd.Margin = new Padding(3, 4, 3, 4);
            pnlAdd.Name = "pnlAdd";
            pnlAdd.Padding = new Padding(17, 20, 17, 20);
            pnlAdd.Size = new Size(1029, 107);
            pnlAdd.TabIndex = 0;
            // 
            // lblProduct
            // 
            lblProduct.AutoSize = true;
            lblProduct.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblProduct.Location = new Point(0, 33);
            lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(124, 23);
            lblProduct.TabIndex = 0;
            lblProduct.Text = "📦 Sản phẩm:";
            // 
            // txtProductSearch
            // 
            txtProductSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtProductSearch.BorderStyle = BorderStyle.FixedSingle;
            txtProductSearch.Font = new Font("Segoe UI", 11F);
            txtProductSearch.Location = new Point(126, 29);
            txtProductSearch.Margin = new Padding(3, 4, 3, 4);
            txtProductSearch.Name = "txtProductSearch";
            txtProductSearch.PlaceholderText = "🔍 Nhập mã hoặc tên sản phẩm...";
            txtProductSearch.Size = new Size(491, 32);
            txtProductSearch.TabIndex = 1;
            // 
            // lblQuantity
            // 
            lblQuantity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblQuantity.Location = new Point(631, 33);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(117, 23);
            lblQuantity.TabIndex = 2;
            lblQuantity.Text = "📥 Số lượng:";
            // 
            // numQuantity
            // 
            numQuantity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numQuantity.Font = new Font("Segoe UI", 11F);
            numQuantity.Location = new Point(754, 29);
            numQuantity.Margin = new Padding(3, 4, 3, 4);
            numQuantity.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(114, 32);
            numQuantity.TabIndex = 3;
            numQuantity.TextAlign = HorizontalAlignment.Center;
            numQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdd.BackColor = Color.FromArgb(0, 123, 255);
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 105, 217);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(886, 24);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(126, 53);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.ColumnHeadersHeight = 32;
            grid.Dock = DockStyle.Fill;
            grid.EnableHeadersVisualStyles = false;
            grid.Location = new Point(0, 187);
            grid.Margin = new Padding(3, 4, 3, 4);
            grid.Name = "grid";
            grid.RowHeadersWidth = 40;
            grid.RowTemplate.Height = 32;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(1029, 600);
            grid.TabIndex = 1;
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.FromArgb(248, 249, 250);
            pnlButtons.Controls.Add(btnSave);
            pnlButtons.Controls.Add(btnRefresh);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Location = new Point(0, 787);
            pnlButtons.Margin = new Padding(3, 4, 3, 4);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Padding = new Padding(17, 16, 17, 16);
            pnlButtons.Size = new Size(1029, 93);
            pnlButtons.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(34, 139, 34);
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(28, 120, 28);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(17, 19);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(183, 56);
            btnSave.TabIndex = 0;
            btnSave.Text = "💾 Lưu điều chỉnh (F12)";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(108, 117, 125);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 98, 104);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(211, 19);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(149, 56);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "🔄 Làm mới (F5)";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // InventoryAdjustmentForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 250);
            ClientSize = new Size(1029, 880);
            Controls.Add(grid);
            Controls.Add(pnlButtons);
            Controls.Add(pnlAdd);
            Controls.Add(pnlHeader);
            Margin = new Padding(0);
            MinimumSize = new Size(912, 651);
            Name = "InventoryAdjustmentForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nhập hàng - Cập nhật tồn kho";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlAdd.ResumeLayout(false);
            pnlAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlAdd;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtProductSearch;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
    }
}
