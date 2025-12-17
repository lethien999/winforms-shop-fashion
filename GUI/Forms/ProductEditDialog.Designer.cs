namespace WinFormsFashionShop.Presentation.Forms
{
    partial class ProductEditDialog
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
            lblCode = new Label();
            txtCode = new TextBox();
            lblName = new Label();
            txtName = new TextBox();
            lblCategory = new Label();
            cmbCategory = new ComboBox();
            lblPrice = new Label();
            txtPrice = new TextBox();
            lblUnit = new Label();
            txtUnit = new TextBox();
            lblImage = new Label();
            picProductImage = new PictureBox();
            btnSelectImage = new Button();
            btnRemoveImage = new Button();
            chkIsActive = new CheckBox();
            btnOK = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)picProductImage).BeginInit();
            SuspendLayout();
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.Location = new Point(10, 20);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(50, 15);
            lblCode.TabIndex = 0;
            lblCode.Text = "Mã SP:";
            // 
            // txtCode
            // 
            txtCode.Location = new Point(120, 20);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(250, 23);
            txtCode.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 60);
            lblName.Name = "lblName";
            lblName.Size = new Size(50, 15);
            lblName.TabIndex = 2;
            lblName.Text = "Tên SP:";
            // 
            // txtName
            // 
            txtName.Location = new Point(120, 60);
            txtName.Name = "txtName";
            txtName.Size = new Size(250, 23);
            txtName.TabIndex = 3;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(10, 100);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(68, 15);
            lblCategory.TabIndex = 4;
            lblCategory.Text = "Danh mục:";
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(120, 100);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(250, 23);
            cmbCategory.TabIndex = 5;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(10, 140);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(55, 15);
            lblPrice.TabIndex = 6;
            lblPrice.Text = "Giá bán:";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(120, 140);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(250, 23);
            txtPrice.TabIndex = 7;
            // 
            // lblUnit
            // 
            lblUnit.AutoSize = true;
            lblUnit.Location = new Point(10, 180);
            lblUnit.Name = "lblUnit";
            lblUnit.Size = new Size(52, 15);
            lblUnit.TabIndex = 8;
            lblUnit.Text = "Đơn vị:";
            // 
            // txtUnit
            // 
            txtUnit.Location = new Point(120, 180);
            txtUnit.Name = "txtUnit";
            txtUnit.Size = new Size(250, 23);
            txtUnit.TabIndex = 9;
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Location = new Point(10, 220);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(52, 15);
            lblImage.TabIndex = 10;
            lblImage.Text = "Ảnh SP:";
            // 
            // picProductImage
            // 
            picProductImage.BorderStyle = BorderStyle.FixedSingle;
            picProductImage.Location = new Point(120, 220);
            picProductImage.Name = "picProductImage";
            picProductImage.Size = new Size(150, 150);
            picProductImage.SizeMode = PictureBoxSizeMode.Zoom;
            picProductImage.TabIndex = 11;
            picProductImage.TabStop = false;
            // 
            // btnSelectImage
            // 
            btnSelectImage.Location = new Point(280, 220);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(90, 30);
            btnSelectImage.TabIndex = 12;
            btnSelectImage.Text = "Chọn ảnh";
            btnSelectImage.UseVisualStyleBackColor = true;
            // 
            // btnRemoveImage
            // 
            btnRemoveImage.Location = new Point(280, 260);
            btnRemoveImage.Name = "btnRemoveImage";
            btnRemoveImage.Size = new Size(90, 30);
            btnRemoveImage.TabIndex = 13;
            btnRemoveImage.Text = "Xóa ảnh";
            btnRemoveImage.UseVisualStyleBackColor = true;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Checked = true;
            chkIsActive.CheckState = CheckState.Checked;
            chkIsActive.Location = new Point(120, 380);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(75, 19);
            chkIsActive.TabIndex = 14;
            chkIsActive.Text = "Hoạt động";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(120, 420);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(100, 30);
            btnOK.TabIndex = 15;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(230, 420);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 30);
            btnCancel.TabIndex = 16;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // ProductEditDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(600, 500);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(chkIsActive);
            Controls.Add(btnRemoveImage);
            Controls.Add(btnSelectImage);
            Controls.Add(picProductImage);
            Controls.Add(lblImage);
            Controls.Add(txtUnit);
            Controls.Add(lblUnit);
            Controls.Add(txtPrice);
            Controls.Add(lblPrice);
            Controls.Add(cmbCategory);
            Controls.Add(lblCategory);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(txtCode);
            Controls.Add(lblCode);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProductEditDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thêm sản phẩm mới";
            ((System.ComponentModel.ISupportInitialize)picProductImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCode;
        private TextBox txtCode;
        private Label lblName;
        private TextBox txtName;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblPrice;
        private TextBox txtPrice;
        private Label lblUnit;
        private TextBox txtUnit;
        private Label lblImage;
        private PictureBox picProductImage;
        private Button btnSelectImage;
        private Button btnRemoveImage;
        private CheckBox chkIsActive;
        private Button btnOK;
        private Button btnCancel;
    }
}
