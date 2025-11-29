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
            this.pnlCustomer = new System.Windows.Forms.Panel();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.btnNewCustomer = new System.Windows.Forms.Button();
            this.pnlProduct = new System.Windows.Forms.Panel();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtProductSearch = new System.Windows.Forms.TextBox();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.gridOrderItems = new System.Windows.Forms.DataGridView();
            this.pnlTotal = new System.Windows.Forms.Panel();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtDiscountPercent = new System.Windows.Forms.TextBox();
            this.lblDiscountAmount = new System.Windows.Forms.Label();
            this.txtDiscountAmount = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnSaveOrder = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlCustomer.SuspendLayout();
            this.pnlProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrderItems)).BeginInit();
            this.pnlTotal.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCustomer
            // 
            this.pnlCustomer.Controls.Add(this.lblCustomer);
            this.pnlCustomer.Controls.Add(this.cmbCustomer);
            this.pnlCustomer.Controls.Add(this.btnNewCustomer);
            this.pnlCustomer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCustomer.Location = new System.Drawing.Point(0, 0);
            this.pnlCustomer.Name = "pnlCustomer";
            this.pnlCustomer.Size = new System.Drawing.Size(1000, 50);
            this.pnlCustomer.TabIndex = 0;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(10, 18);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(75, 15);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Khách hàng:";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(100, 15);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(300, 23);
            this.cmbCustomer.TabIndex = 1;
            // 
            // btnNewCustomer
            // 
            this.btnNewCustomer.Location = new System.Drawing.Point(410, 15);
            this.btnNewCustomer.Name = "btnNewCustomer";
            this.btnNewCustomer.Size = new System.Drawing.Size(120, 23);
            this.btnNewCustomer.TabIndex = 2;
            this.btnNewCustomer.Text = "Thêm khách mới";
            this.btnNewCustomer.UseVisualStyleBackColor = true;
            // 
            // pnlProduct
            // 
            this.pnlProduct.Controls.Add(this.lblProduct);
            this.pnlProduct.Controls.Add(this.txtProductSearch);
            this.pnlProduct.Controls.Add(this.btnAddProduct);
            this.pnlProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProduct.Location = new System.Drawing.Point(0, 50);
            this.pnlProduct.Name = "pnlProduct";
            this.pnlProduct.Size = new System.Drawing.Size(1000, 50);
            this.pnlProduct.TabIndex = 1;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(10, 18);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(62, 15);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Sản phẩm:";
            // 
            // txtProductSearch
            // 
            this.txtProductSearch.Location = new System.Drawing.Point(100, 15);
            this.txtProductSearch.Name = "txtProductSearch";
            this.txtProductSearch.PlaceholderText = "Nhập mã hoặc tên sản phẩm...";
            this.txtProductSearch.Size = new System.Drawing.Size(300, 23);
            this.txtProductSearch.TabIndex = 1;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(410, 15);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(100, 23);
            this.btnAddProduct.TabIndex = 2;
            this.btnAddProduct.Text = "Thêm";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            // 
            // gridOrderItems
            // 
            this.gridOrderItems.AllowUserToAddRows = false;
            this.gridOrderItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridOrderItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOrderItems.Location = new System.Drawing.Point(0, 100);
            this.gridOrderItems.Name = "gridOrderItems";
            this.gridOrderItems.Size = new System.Drawing.Size(1000, 480);
            this.gridOrderItems.TabIndex = 2;
            // 
            // pnlTotal
            // 
            this.pnlTotal.Controls.Add(this.lblPaymentMethod);
            this.pnlTotal.Controls.Add(this.cmbPaymentMethod);
            this.pnlTotal.Controls.Add(this.lblDiscount);
            this.pnlTotal.Controls.Add(this.txtDiscountPercent);
            this.pnlTotal.Controls.Add(this.lblDiscountAmount);
            this.pnlTotal.Controls.Add(this.txtDiscountAmount);
            this.pnlTotal.Controls.Add(this.lblTotal);
            this.pnlTotal.Controls.Add(this.btnSaveOrder);
            this.pnlTotal.Controls.Add(this.btnCancel);
            this.pnlTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTotal.Location = new System.Drawing.Point(0, 580);
            this.pnlTotal.Name = "pnlTotal";
            this.pnlTotal.Size = new System.Drawing.Size(1000, 120);
            this.pnlTotal.TabIndex = 3;
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new System.Drawing.Point(520, 43);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(120, 15);
            this.lblPaymentMethod.TabIndex = 7;
            this.lblPaymentMethod.Text = "Phương thức thanh toán:";
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Location = new System.Drawing.Point(650, 40);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(150, 23);
            this.cmbPaymentMethod.TabIndex = 8;
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(10, 13);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(85, 15);
            this.lblDiscount.TabIndex = 0;
            this.lblDiscount.Text = "Giảm giá (%):";
            // 
            // txtDiscountPercent
            // 
            this.txtDiscountPercent.Location = new System.Drawing.Point(120, 10);
            this.txtDiscountPercent.Name = "txtDiscountPercent";
            this.txtDiscountPercent.Size = new System.Drawing.Size(100, 23);
            this.txtDiscountPercent.TabIndex = 1;
            this.txtDiscountPercent.Text = "0";
            // 
            // lblDiscountAmount
            // 
            this.lblDiscountAmount.AutoSize = true;
            this.lblDiscountAmount.Location = new System.Drawing.Point(240, 13);
            this.lblDiscountAmount.Name = "lblDiscountAmount";
            this.lblDiscountAmount.Size = new System.Drawing.Size(100, 15);
            this.lblDiscountAmount.TabIndex = 2;
            this.lblDiscountAmount.Text = "Giảm giá (VNĐ):";
            // 
            // txtDiscountAmount
            // 
            this.txtDiscountAmount.Location = new System.Drawing.Point(350, 10);
            this.txtDiscountAmount.Name = "txtDiscountAmount";
            this.txtDiscountAmount.Size = new System.Drawing.Size(150, 23);
            this.txtDiscountAmount.TabIndex = 3;
            this.txtDiscountAmount.Text = "0";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotal.Location = new System.Drawing.Point(10, 43);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(120, 18);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Tổng tiền: 0 VNĐ";
            // 
            // btnSaveOrder
            // 
            this.btnSaveOrder.BackColor = System.Drawing.Color.Green;
            this.btnSaveOrder.ForeColor = System.Drawing.Color.White;
            this.btnSaveOrder.Location = new System.Drawing.Point(520, 12);
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(120, 40);
            this.btnSaveOrder.TabIndex = 5;
            this.btnSaveOrder.Text = "Lưu hóa đơn";
            this.btnSaveOrder.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(650, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.gridOrderItems);
            this.Controls.Add(this.pnlTotal);
            this.Controls.Add(this.pnlProduct);
            this.Controls.Add(this.pnlCustomer);
            this.Name = "OrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lập hóa đơn bán hàng";
            this.pnlCustomer.ResumeLayout(false);
            this.pnlCustomer.PerformLayout();
            this.pnlProduct.ResumeLayout(false);
            this.pnlProduct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrderItems)).EndInit();
            this.pnlTotal.ResumeLayout(false);
            this.pnlTotal.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlCustomer;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Button btnNewCustomer;
        private System.Windows.Forms.Panel pnlProduct;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtProductSearch;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.DataGridView gridOrderItems;
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
