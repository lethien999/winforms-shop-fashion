namespace WinFormsFashionShop.Presentation.Forms
{
    partial class OrderForm
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
            pnlCustomer = new Panel();
            lblCustomer = new Label();
            cmbCustomer = new ComboBox();
            btnNewCustomer = new Button();
            pnlProduct = new Panel();
            lblProduct = new Label();
            txtProductSearch = new TextBox();
            btnAddProduct = new Button();
            splitContainer = new SplitContainer();
            gridOrderItems = new DataGridView();
            pnlProductImage = new Panel();
            picProductImage = new PictureBox();
            lblProductImage = new Label();
            pnlTotal = new Panel();
            lblPaymentMethod = new Label();
            cmbPaymentMethod = new ComboBox();
            lblDiscount = new Label();
            txtDiscountPercent = new TextBox();
            lblDiscountAmount = new Label();
            txtDiscountAmount = new TextBox();
            lblTotal = new Label();
            btnSaveOrder = new Button();
            btnCancel = new Button();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlCustomer.SuspendLayout();
            pnlProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridOrderItems).BeginInit();
            pnlProductImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picProductImage).BeginInit();
            pnlTotal.SuspendLayout();
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
            pnlHeader.Size = new Size(1100, 60);
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
            lblTitle.Size = new Size(349, 29);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "📝 LẬP HÓA ĐƠN BÁN HÀNG";
            // 
            // pnlCustomer
            // 
            pnlCustomer.Controls.Add(lblCustomer);
            pnlCustomer.Controls.Add(cmbCustomer);
            pnlCustomer.Controls.Add(btnNewCustomer);
            pnlCustomer.Dock = DockStyle.Top;
            pnlCustomer.Location = new Point(0, 60);
            pnlCustomer.Margin = new Padding(0);
            pnlCustomer.Name = "pnlCustomer";
            pnlCustomer.Padding = new Padding(15, 12, 15, 12);
            pnlCustomer.Size = new Size(1100, 65);
            pnlCustomer.TabIndex = 0;
            // 
            // lblCustomer
            // 
            lblCustomer.AutoSize = true;
            lblCustomer.Font = new Font("Arial", 10F);
            lblCustomer.Location = new Point(15, 20);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(102, 19);
            lblCustomer.TabIndex = 0;
            lblCustomer.Text = "Khách hàng:";
            // 
            // cmbCustomer
            // 
            cmbCustomer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCustomer.Font = new Font("Arial", 10F);
            cmbCustomer.FormattingEnabled = true;
            cmbCustomer.Location = new Point(120, 17);
            cmbCustomer.Margin = new Padding(0);
            cmbCustomer.Name = "cmbCustomer";
            cmbCustomer.Size = new Size(860, 27);
            cmbCustomer.TabIndex = 1;
            // 
            // btnNewCustomer
            // 
            btnNewCustomer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNewCustomer.BackColor = Color.FromArgb(0, 123, 255);
            btnNewCustomer.Cursor = Cursors.Hand;
            btnNewCustomer.FlatAppearance.BorderSize = 0;
            btnNewCustomer.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 105, 217);
            btnNewCustomer.FlatStyle = FlatStyle.Flat;
            btnNewCustomer.Font = new Font("Arial", 10F, FontStyle.Bold);
            btnNewCustomer.ForeColor = Color.White;
            btnNewCustomer.Location = new Point(990, 15);
            btnNewCustomer.Margin = new Padding(0);
            btnNewCustomer.Name = "btnNewCustomer";
            btnNewCustomer.Size = new Size(95, 30);
            btnNewCustomer.TabIndex = 2;
            btnNewCustomer.Text = "➕ Thêm";
            btnNewCustomer.UseVisualStyleBackColor = false;
            // 
            // pnlProduct
            // 
            pnlProduct.Controls.Add(lblProduct);
            pnlProduct.Controls.Add(txtProductSearch);
            pnlProduct.Controls.Add(btnAddProduct);
            pnlProduct.Dock = DockStyle.Top;
            pnlProduct.Location = new Point(0, 125);
            pnlProduct.Margin = new Padding(0);
            pnlProduct.Name = "pnlProduct";
            pnlProduct.Padding = new Padding(15, 12, 15, 12);
            pnlProduct.Size = new Size(1100, 65);
            pnlProduct.TabIndex = 1;
            // 
            // lblProduct
            // 
            lblProduct.AutoSize = true;
            lblProduct.Font = new Font("Arial", 10F);
            lblProduct.Location = new Point(15, 20);
            lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(88, 19);
            lblProduct.TabIndex = 0;
            lblProduct.Text = "Sản phẩm:";
            // 
            // txtProductSearch
            // 
            txtProductSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtProductSearch.Font = new Font("Arial", 10F);
            txtProductSearch.Location = new Point(120, 17);
            txtProductSearch.Margin = new Padding(0);
            txtProductSearch.Name = "txtProductSearch";
            txtProductSearch.PlaceholderText = "Nhập mã/tên sản phẩm hoặc quét mã vạch (Enter)...";
            txtProductSearch.Size = new Size(860, 27);
            txtProductSearch.TabIndex = 1;
            // 
            // btnAddProduct
            // 
            btnAddProduct.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddProduct.BackColor = Color.FromArgb(0, 123, 255);
            btnAddProduct.Cursor = Cursors.Hand;
            btnAddProduct.FlatAppearance.BorderSize = 0;
            btnAddProduct.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 105, 217);
            btnAddProduct.FlatStyle = FlatStyle.Flat;
            btnAddProduct.Font = new Font("Arial", 10F, FontStyle.Bold);
            btnAddProduct.ForeColor = Color.White;
            btnAddProduct.Location = new Point(990, 15);
            btnAddProduct.Margin = new Padding(0);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(95, 30);
            btnAddProduct.TabIndex = 2;
            btnAddProduct.Text = "➕ Thêm";
            btnAddProduct.UseVisualStyleBackColor = false;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 190);
            splitContainer.Margin = new Padding(0);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(gridOrderItems);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(pnlProductImage);
            splitContainer.Size = new Size(1100, 430);
            splitContainer.SplitterDistance = 750;
            splitContainer.SplitterWidth = 5;
            splitContainer.TabIndex = 2;
            // 
            // gridOrderItems
            // 
            gridOrderItems.AllowUserToAddRows = false;
            gridOrderItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridOrderItems.BackgroundColor = Color.White;
            gridOrderItems.BorderStyle = BorderStyle.None;
            gridOrderItems.ColumnHeadersHeight = 35;
            gridOrderItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridOrderItems.Dock = DockStyle.Fill;
            gridOrderItems.EnableHeadersVisualStyles = false;
            gridOrderItems.Location = new Point(0, 0);
            gridOrderItems.Margin = new Padding(0);
            gridOrderItems.Name = "gridOrderItems";
            gridOrderItems.RowHeadersWidth = 40;
            gridOrderItems.RowTemplate.Height = 32;
            gridOrderItems.Size = new Size(750, 430);
            gridOrderItems.TabIndex = 0;
            // 
            // pnlProductImage
            // 
            pnlProductImage.BackColor = Color.White;
            pnlProductImage.BorderStyle = BorderStyle.FixedSingle;
            pnlProductImage.Controls.Add(picProductImage);
            pnlProductImage.Controls.Add(lblProductImage);
            pnlProductImage.Dock = DockStyle.Fill;
            pnlProductImage.Location = new Point(0, 0);
            pnlProductImage.Margin = new Padding(0);
            pnlProductImage.Name = "pnlProductImage";
            pnlProductImage.Padding = new Padding(15);
            pnlProductImage.Size = new Size(345, 430);
            pnlProductImage.TabIndex = 0;
            // 
            // picProductImage
            // 
            picProductImage.BackColor = Color.FromArgb(248, 248, 248);
            picProductImage.BorderStyle = BorderStyle.FixedSingle;
            picProductImage.Dock = DockStyle.Fill;
            picProductImage.Location = new Point(15, 45);
            picProductImage.Margin = new Padding(0);
            picProductImage.Name = "picProductImage";
            picProductImage.Size = new Size(313, 368);
            picProductImage.SizeMode = PictureBoxSizeMode.Zoom;
            picProductImage.TabIndex = 1;
            picProductImage.TabStop = false;
            // 
            // lblProductImage
            // 
            lblProductImage.Dock = DockStyle.Top;
            lblProductImage.Font = new Font("Arial", 11F, FontStyle.Bold);
            lblProductImage.Location = new Point(15, 15);
            lblProductImage.Name = "lblProductImage";
            lblProductImage.Size = new Size(313, 30);
            lblProductImage.TabIndex = 0;
            lblProductImage.Text = "Ảnh sản phẩm";
            lblProductImage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlTotal
            // 
            pnlTotal.Controls.Add(lblPaymentMethod);
            pnlTotal.Controls.Add(cmbPaymentMethod);
            pnlTotal.Controls.Add(lblDiscount);
            pnlTotal.Controls.Add(txtDiscountPercent);
            pnlTotal.Controls.Add(lblDiscountAmount);
            pnlTotal.Controls.Add(txtDiscountAmount);
            pnlTotal.Controls.Add(lblTotal);
            pnlTotal.Controls.Add(btnSaveOrder);
            pnlTotal.Controls.Add(btnCancel);
            pnlTotal.Dock = DockStyle.Bottom;
            pnlTotal.Location = new Point(0, 620);
            pnlTotal.Margin = new Padding(0);
            pnlTotal.Name = "pnlTotal";
            pnlTotal.Padding = new Padding(15);
            pnlTotal.Size = new Size(1100, 130);
            pnlTotal.TabIndex = 3;
            // 
            // lblPaymentMethod
            // 
            lblPaymentMethod.AutoSize = true;
            lblPaymentMethod.Font = new Font("Arial", 10F);
            lblPaymentMethod.Location = new Point(20, 55);
            lblPaymentMethod.Name = "lblPaymentMethod";
            lblPaymentMethod.Size = new Size(193, 19);
            lblPaymentMethod.TabIndex = 7;
            lblPaymentMethod.Text = "Phương thức thanh toán:";
            // 
            // cmbPaymentMethod
            // 
            cmbPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaymentMethod.Font = new Font("Arial", 10F);
            cmbPaymentMethod.FormattingEnabled = true;
            cmbPaymentMethod.Location = new Point(216, 52);
            cmbPaymentMethod.Margin = new Padding(0);
            cmbPaymentMethod.Name = "cmbPaymentMethod";
            cmbPaymentMethod.Size = new Size(250, 27);
            cmbPaymentMethod.TabIndex = 8;
            // 
            // lblDiscount
            // 
            lblDiscount.AutoSize = true;
            lblDiscount.Font = new Font("Arial", 10F);
            lblDiscount.Location = new Point(20, 20);
            lblDiscount.Name = "lblDiscount";
            lblDiscount.Size = new Size(111, 19);
            lblDiscount.TabIndex = 0;
            lblDiscount.Text = "Giảm giá (%):";
            // 
            // txtDiscountPercent
            // 
            txtDiscountPercent.Font = new Font("Arial", 10F);
            txtDiscountPercent.Location = new Point(130, 17);
            txtDiscountPercent.Margin = new Padding(0);
            txtDiscountPercent.Name = "txtDiscountPercent";
            txtDiscountPercent.Size = new Size(100, 27);
            txtDiscountPercent.TabIndex = 1;
            txtDiscountPercent.Text = "0";
            // 
            // lblDiscountAmount
            // 
            lblDiscountAmount.AutoSize = true;
            lblDiscountAmount.Font = new Font("Arial", 10F);
            lblDiscountAmount.Location = new Point(250, 20);
            lblDiscountAmount.Name = "lblDiscountAmount";
            lblDiscountAmount.Size = new Size(130, 19);
            lblDiscountAmount.TabIndex = 2;
            lblDiscountAmount.Text = "Giảm giá (VNĐ):";
            // 
            // txtDiscountAmount
            // 
            txtDiscountAmount.Font = new Font("Arial", 10F);
            txtDiscountAmount.Location = new Point(383, 17);
            txtDiscountAmount.Margin = new Padding(0);
            txtDiscountAmount.Name = "txtDiscountAmount";
            txtDiscountAmount.Size = new Size(150, 27);
            txtDiscountAmount.TabIndex = 3;
            txtDiscountAmount.Text = "0";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Arial", 14F, FontStyle.Bold);
            lblTotal.ForeColor = Color.Red;
            lblTotal.Location = new Point(20, 95);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(207, 29);
            lblTotal.TabIndex = 4;
            lblTotal.Text = "Tổng tiền: 0 VNĐ";
            // 
            // btnSaveOrder
            // 
            btnSaveOrder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSaveOrder.BackColor = Color.FromArgb(34, 139, 34);
            btnSaveOrder.Cursor = Cursors.Hand;
            btnSaveOrder.FlatAppearance.BorderSize = 0;
            btnSaveOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(28, 120, 28);
            btnSaveOrder.FlatStyle = FlatStyle.Flat;
            btnSaveOrder.Font = new Font("Arial", 11F, FontStyle.Bold);
            btnSaveOrder.ForeColor = Color.White;
            btnSaveOrder.Location = new Point(755, 20);
            btnSaveOrder.Margin = new Padding(0);
            btnSaveOrder.Name = "btnSaveOrder";
            btnSaveOrder.Size = new Size(185, 45);
            btnSaveOrder.TabIndex = 5;
            btnSaveOrder.Text = "💾 Lưu hóa đơn";
            btnSaveOrder.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 98, 104);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Arial", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(950, 20);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 45);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "❌ Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // OrderForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 250);
            ClientSize = new Size(1100, 750);
            Controls.Add(splitContainer);
            Controls.Add(pnlTotal);
            Controls.Add(pnlProduct);
            Controls.Add(pnlCustomer);
            Controls.Add(pnlHeader);
            Margin = new Padding(0);
            MinimumSize = new Size(950, 650);
            Name = "OrderForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Lập hóa đơn bán hàng";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlCustomer.ResumeLayout(false);
            pnlCustomer.PerformLayout();
            pnlProduct.ResumeLayout(false);
            pnlProduct.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridOrderItems).EndInit();
            pnlProductImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picProductImage).EndInit();
            pnlTotal.ResumeLayout(false);
            pnlTotal.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlCustomer;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Button btnNewCustomer;
        private System.Windows.Forms.Panel pnlProduct;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtProductSearch;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView gridOrderItems;
        private System.Windows.Forms.Panel pnlProductImage;
        private System.Windows.Forms.Label lblProductImage;
        private System.Windows.Forms.PictureBox picProductImage;
        private System.Windows.Forms.Panel pnlTotal;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.TextBox txtDiscountPercent;
        private System.Windows.Forms.Label lblDiscountAmount;
        private System.Windows.Forms.TextBox txtDiscountAmount;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnSaveOrder;
        private System.Windows.Forms.Button btnCancel;
    }
}
