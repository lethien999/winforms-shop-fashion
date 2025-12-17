namespace WinFormsFashionShop.Presentation.Forms
{
    partial class ProductSelectionDialog
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
            lblSearch = new Label();
            _txtSearch = new TextBox();
            lblCategory = new Label();
            _cmbCategoryFilter = new ComboBox();
            splitContainer = new SplitContainer();
            pnlGrid = new Panel();
            _gridProducts = new DataGridView();
            pnlDetail = new Panel();
            lblDetailTitle = new Label();
            _picProductImage = new PictureBox();
            _lblProductInfo = new Label();
            pnlFooter = new Panel();
            _btnSelect = new Button();
            _btnCancel = new Button();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_gridProducts).BeginInit();
            pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_picProductImage).BeginInit();
            pnlFooter.SuspendLayout();
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
            lblTitle.Size = new Size(246, 27);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "CHỌN SẢN PHẨM";
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = Color.White;
            pnlSearch.BorderStyle = BorderStyle.FixedSingle;
            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Controls.Add(_txtSearch);
            pnlSearch.Controls.Add(lblCategory);
            pnlSearch.Controls.Add(_cmbCategoryFilter);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 60);
            pnlSearch.Margin = new Padding(0);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(12, 10, 12, 10);
            pnlSearch.Size = new Size(900, 70);
            pnlSearch.TabIndex = 1;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Arial", 10F);
            lblSearch.Location = new Point(12, 18);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(75, 19);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Tìm kiếm:";
            // 
            // _txtSearch
            // 
            _txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _txtSearch.Font = new Font("Arial", 10F);
            _txtSearch.Location = new Point(95, 18);
            _txtSearch.Margin = new Padding(0);
            _txtSearch.Name = "_txtSearch";
            _txtSearch.PlaceholderText = "Nhập mã hoặc tên sản phẩm...";
            _txtSearch.Size = new Size(400, 27);
            _txtSearch.TabIndex = 1;
            // 
            // lblCategory
            // 
            lblCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Arial", 10F);
            lblCategory.Location = new Point(505, 21);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(84, 19);
            lblCategory.TabIndex = 2;
            lblCategory.Text = "Danh mục:";
            // 
            // _cmbCategoryFilter
            // 
            _cmbCategoryFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cmbCategoryFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            _cmbCategoryFilter.Font = new Font("Arial", 10F);
            _cmbCategoryFilter.FormattingEnabled = true;
            _cmbCategoryFilter.Location = new Point(595, 18);
            _cmbCategoryFilter.Margin = new Padding(0);
            _cmbCategoryFilter.Name = "_cmbCategoryFilter";
            _cmbCategoryFilter.Size = new Size(193, 27);
            _cmbCategoryFilter.TabIndex = 3;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 130);
            splitContainer.Margin = new Padding(0);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(pnlGrid);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(pnlDetail);
            splitContainer.Size = new Size(900, 460);
            splitContainer.SplitterDistance = 300;
            splitContainer.SplitterWidth = 5;
            splitContainer.TabIndex = 2;
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(_gridProducts);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(0, 0);
            pnlGrid.Margin = new Padding(0);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(10);
            pnlGrid.Size = new Size(900, 300);
            pnlGrid.TabIndex = 0;
            // 
            // _gridProducts
            // 
            _gridProducts.AllowUserToAddRows = false;
            _gridProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _gridProducts.BackgroundColor = Color.White;
            _gridProducts.BorderStyle = BorderStyle.None;
            _gridProducts.ColumnHeadersHeight = 35;
            _gridProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _gridProducts.Dock = DockStyle.Fill;
            _gridProducts.EnableHeadersVisualStyles = false;
            _gridProducts.Location = new Point(10, 10);
            _gridProducts.Margin = new Padding(0);
            _gridProducts.MultiSelect = false;
            _gridProducts.Name = "_gridProducts";
            _gridProducts.ReadOnly = true;
            _gridProducts.RowHeadersWidth = 40;
            _gridProducts.RowTemplate.Height = 32;
            _gridProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _gridProducts.Size = new Size(880, 280);
            _gridProducts.TabIndex = 0;
            // 
            // pnlDetail
            // 
            pnlDetail.BackColor = Color.FromArgb(245, 245, 250);
            pnlDetail.BorderStyle = BorderStyle.FixedSingle;
            pnlDetail.Controls.Add(lblDetailTitle);
            pnlDetail.Controls.Add(_picProductImage);
            pnlDetail.Controls.Add(_lblProductInfo);
            pnlDetail.Dock = DockStyle.Fill;
            pnlDetail.Location = new Point(0, 0);
            pnlDetail.Margin = new Padding(0);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Padding = new Padding(15);
            pnlDetail.Size = new Size(900, 155);
            pnlDetail.TabIndex = 0;
            // 
            // lblDetailTitle
            // 
            lblDetailTitle.AutoSize = true;
            lblDetailTitle.Font = new Font("Arial", 11F, FontStyle.Bold);
            lblDetailTitle.Location = new Point(15, 15);
            lblDetailTitle.Name = "lblDetailTitle";
            lblDetailTitle.Size = new Size(170, 22);
            lblDetailTitle.TabIndex = 0;
            lblDetailTitle.Text = "Thông tin sản phẩm";
            // 
            // _picProductImage
            // 
            _picProductImage.BackColor = Color.White;
            _picProductImage.BorderStyle = BorderStyle.FixedSingle;
            _picProductImage.Location = new Point(15, 45);
            _picProductImage.Margin = new Padding(0);
            _picProductImage.Name = "_picProductImage";
            _picProductImage.Size = new Size(150, 95);
            _picProductImage.SizeMode = PictureBoxSizeMode.Zoom;
            _picProductImage.TabIndex = 1;
            _picProductImage.TabStop = false;
            // 
            // _lblProductInfo
            // 
            _lblProductInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _lblProductInfo.Font = new Font("Arial", 10F);
            _lblProductInfo.Location = new Point(175, 45);
            _lblProductInfo.Name = "_lblProductInfo";
            _lblProductInfo.Size = new Size(710, 95);
            _lblProductInfo.TabIndex = 2;
            _lblProductInfo.Text = "Chọn sản phẩm để xem thông tin";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(240, 240, 240);
            pnlFooter.BorderStyle = BorderStyle.FixedSingle;
            pnlFooter.Controls.Add(_btnSelect);
            pnlFooter.Controls.Add(_btnCancel);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 590);
            pnlFooter.Margin = new Padding(0);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(15);
            pnlFooter.Size = new Size(900, 60);
            pnlFooter.TabIndex = 3;
            // 
            // _btnSelect
            // 
            _btnSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnSelect.BackColor = Color.FromArgb(34, 139, 34);
            _btnSelect.FlatAppearance.BorderSize = 0;
            _btnSelect.FlatAppearance.MouseOverBackColor = Color.FromArgb(28, 120, 28);
            _btnSelect.FlatStyle = FlatStyle.Flat;
            _btnSelect.Font = new Font("Arial", 10F, FontStyle.Bold);
            _btnSelect.ForeColor = Color.White;
            _btnSelect.Location = new Point(700, 12);
            _btnSelect.Margin = new Padding(0);
            _btnSelect.Name = "_btnSelect";
            _btnSelect.Size = new Size(90, 35);
            _btnSelect.TabIndex = 0;
            _btnSelect.Text = "Chọn";
            _btnSelect.UseVisualStyleBackColor = false;
            _btnSelect.Cursor = Cursors.Hand;
            // 
            // _btnCancel
            // 
            _btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            _btnCancel.FlatAppearance.BorderSize = 0;
            _btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 98, 104);
            _btnCancel.FlatStyle = FlatStyle.Flat;
            _btnCancel.Font = new Font("Arial", 10F, FontStyle.Bold);
            _btnCancel.ForeColor = Color.White;
            _btnCancel.Location = new Point(800, 12);
            _btnCancel.Margin = new Padding(0);
            _btnCancel.Name = "_btnCancel";
            _btnCancel.Size = new Size(85, 35);
            _btnCancel.TabIndex = 1;
            _btnCancel.Text = "Hủy";
            _btnCancel.UseVisualStyleBackColor = false;
            _btnCancel.Cursor = Cursors.Hand;
            // 
            // ProductSelectionDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 250);
            ClientSize = new Size(900, 650);
            Controls.Add(splitContainer);
            Controls.Add(pnlFooter);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(0);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(800, 600);
            Name = "ProductSelectionDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chọn sản phẩm";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_gridProducts).EndInit();
            pnlDetail.ResumeLayout(false);
            pnlDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_picProductImage).EndInit();
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox _txtSearch;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox _cmbCategoryFilter;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView _gridProducts;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.PictureBox _picProductImage;
        private System.Windows.Forms.Label _lblProductInfo;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button _btnSelect;
        private System.Windows.Forms.Button _btnCancel;
    }
}

