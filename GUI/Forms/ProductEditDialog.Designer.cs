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
            pnlHeader = new Panel();
            lblHeaderIcon = new Label();
            lblHeaderTitle = new Label();
            pnlContent = new Panel();
            pnlLeft = new Panel();
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
            chkIsActive = new CheckBox();
            pnlRight = new Panel();
            lblImage = new Label();
            picProductImage = new PictureBox();
            btnSelectImage = new Button();
            btnRemoveImage = new Button();
            pnlButtons = new Panel();
            btnOK = new Button();
            btnCancel = new Button();
            pnlHeader.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlLeft.SuspendLayout();
            pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picProductImage).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(70, 130, 180);
            pnlHeader.Controls.Add(lblHeaderIcon);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(580, 60);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderIcon
            // 
            lblHeaderIcon.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            lblHeaderIcon.ForeColor = Color.White;
            lblHeaderIcon.Location = new Point(15, 8);
            lblHeaderIcon.Name = "lblHeaderIcon";
            lblHeaderIcon.Size = new Size(50, 45);
            lblHeaderIcon.TabIndex = 0;
            lblHeaderIcon.Text = "👗";
            lblHeaderIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(65, 18);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(180, 25);
            lblHeaderTitle.TabIndex = 1;
            lblHeaderTitle.Text = "Thông tin sản phẩm";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(pnlLeft);
            pnlContent.Controls.Add(pnlRight);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 60);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(580, 290);
            pnlContent.TabIndex = 1;
            // 
            // pnlLeft
            // 
            pnlLeft.Controls.Add(lblCode);
            pnlLeft.Controls.Add(txtCode);
            pnlLeft.Controls.Add(lblName);
            pnlLeft.Controls.Add(txtName);
            pnlLeft.Controls.Add(lblCategory);
            pnlLeft.Controls.Add(cmbCategory);
            pnlLeft.Controls.Add(lblPrice);
            pnlLeft.Controls.Add(txtPrice);
            pnlLeft.Controls.Add(lblUnit);
            pnlLeft.Controls.Add(txtUnit);
            pnlLeft.Controls.Add(chkIsActive);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new Padding(20, 15, 10, 15);
            pnlLeft.Size = new Size(380, 290);
            pnlLeft.TabIndex = 0;
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblCode.ForeColor = Color.FromArgb(64, 64, 64);
            lblCode.Location = new Point(20, 20);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(80, 19);
            lblCode.TabIndex = 0;
            lblCode.Text = "🏷️  Mã SP:";
            // 
            // txtCode
            // 
            txtCode.BorderStyle = BorderStyle.FixedSingle;
            txtCode.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtCode.Location = new Point(130, 17);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(230, 25);
            txtCode.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblName.ForeColor = Color.FromArgb(64, 64, 64);
            lblName.Location = new Point(20, 58);
            lblName.Name = "lblName";
            lblName.Size = new Size(85, 19);
            lblName.TabIndex = 2;
            lblName.Text = "📝  Tên SP:";
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtName.Location = new Point(130, 55);
            txtName.Name = "txtName";
            txtName.Size = new Size(230, 25);
            txtName.TabIndex = 3;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblCategory.ForeColor = Color.FromArgb(64, 64, 64);
            lblCategory.Location = new Point(20, 96);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(100, 19);
            lblCategory.TabIndex = 4;
            lblCategory.Text = "📁  Danh mục:";
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FlatStyle = FlatStyle.Flat;
            cmbCategory.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(130, 93);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(230, 25);
            cmbCategory.TabIndex = 5;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblPrice.ForeColor = Color.FromArgb(64, 64, 64);
            lblPrice.Location = new Point(20, 134);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(85, 19);
            lblPrice.TabIndex = 6;
            lblPrice.Text = "💰  Giá bán:";
            // 
            // txtPrice
            // 
            txtPrice.BorderStyle = BorderStyle.FixedSingle;
            txtPrice.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtPrice.Location = new Point(130, 131);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(230, 25);
            txtPrice.TabIndex = 7;
            // 
            // lblUnit
            // 
            lblUnit.AutoSize = true;
            lblUnit.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblUnit.ForeColor = Color.FromArgb(64, 64, 64);
            lblUnit.Location = new Point(20, 172);
            lblUnit.Name = "lblUnit";
            lblUnit.Size = new Size(85, 19);
            lblUnit.TabIndex = 8;
            lblUnit.Text = "📦  Đơn vị:";
            // 
            // txtUnit
            // 
            txtUnit.BorderStyle = BorderStyle.FixedSingle;
            txtUnit.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtUnit.Location = new Point(130, 169);
            txtUnit.Name = "txtUnit";
            txtUnit.Size = new Size(230, 25);
            txtUnit.TabIndex = 9;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Checked = true;
            chkIsActive.CheckState = CheckState.Checked;
            chkIsActive.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            chkIsActive.ForeColor = Color.FromArgb(64, 64, 64);
            chkIsActive.Location = new Point(130, 210);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(119, 23);
            chkIsActive.TabIndex = 10;
            chkIsActive.Text = "✅  Hoạt động";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // pnlRight
            // 
            pnlRight.BackColor = Color.FromArgb(248, 249, 250);
            pnlRight.Controls.Add(lblImage);
            pnlRight.Controls.Add(picProductImage);
            pnlRight.Controls.Add(btnSelectImage);
            pnlRight.Controls.Add(btnRemoveImage);
            pnlRight.Dock = DockStyle.Right;
            pnlRight.Location = new Point(380, 0);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(10);
            pnlRight.Size = new Size(200, 290);
            pnlRight.TabIndex = 1;
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblImage.ForeColor = Color.FromArgb(70, 130, 180);
            lblImage.Location = new Point(55, 15);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(90, 19);
            lblImage.TabIndex = 10;
            lblImage.Text = "📸 Hình ảnh";
            // 
            // picProductImage
            // 
            picProductImage.BackColor = Color.White;
            picProductImage.BorderStyle = BorderStyle.FixedSingle;
            picProductImage.Location = new Point(25, 45);
            picProductImage.Name = "picProductImage";
            picProductImage.Size = new Size(150, 150);
            picProductImage.SizeMode = PictureBoxSizeMode.Zoom;
            picProductImage.TabIndex = 11;
            picProductImage.TabStop = false;
            // 
            // btnSelectImage
            // 
            btnSelectImage.BackColor = Color.FromArgb(0, 123, 255);
            btnSelectImage.Cursor = Cursors.Hand;
            btnSelectImage.FlatAppearance.BorderSize = 0;
            btnSelectImage.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 105, 217);
            btnSelectImage.FlatStyle = FlatStyle.Flat;
            btnSelectImage.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnSelectImage.ForeColor = Color.White;
            btnSelectImage.Location = new Point(25, 205);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(70, 32);
            btnSelectImage.TabIndex = 12;
            btnSelectImage.Text = "📂 Chọn";
            btnSelectImage.UseVisualStyleBackColor = false;
            // 
            // btnRemoveImage
            // 
            btnRemoveImage.BackColor = Color.FromArgb(220, 53, 69);
            btnRemoveImage.Cursor = Cursors.Hand;
            btnRemoveImage.FlatAppearance.BorderSize = 0;
            btnRemoveImage.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 35, 51);
            btnRemoveImage.FlatStyle = FlatStyle.Flat;
            btnRemoveImage.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnRemoveImage.ForeColor = Color.White;
            btnRemoveImage.Location = new Point(105, 205);
            btnRemoveImage.Name = "btnRemoveImage";
            btnRemoveImage.Size = new Size(70, 32);
            btnRemoveImage.TabIndex = 13;
            btnRemoveImage.Text = "🗑️ Xóa";
            btnRemoveImage.UseVisualStyleBackColor = false;
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.FromArgb(248, 249, 250);
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnOK);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Location = new Point(0, 350);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(580, 60);
            pnlButtons.TabIndex = 2;
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.FromArgb(34, 139, 34);
            btnOK.Cursor = Cursors.Hand;
            btnOK.DialogResult = DialogResult.OK;
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 167, 69);
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnOK.ForeColor = Color.White;
            btnOK.Location = new Point(340, 12);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(110, 38);
            btnOK.TabIndex = 15;
            btnOK.Text = "✓  Lưu";
            btnOK.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 98, 104);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(460, 12);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(110, 38);
            btnCancel.TabIndex = 16;
            btnCancel.Text = "✕  Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // ProductEditDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnCancel;
            ClientSize = new Size(580, 410);
            Controls.Add(pnlContent);
            Controls.Add(pnlButtons);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProductEditDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thông tin sản phẩm";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlContent.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            pnlLeft.PerformLayout();
            pnlRight.ResumeLayout(false);
            pnlRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picProductImage).EndInit();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderIcon;
        private Label lblHeaderTitle;
        private Panel pnlContent;
        private Panel pnlLeft;
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
        private CheckBox chkIsActive;
        private Panel pnlRight;
        private Label lblImage;
        private PictureBox picProductImage;
        private Button btnSelectImage;
        private Button btnRemoveImage;
        private Panel pnlButtons;
        private Button btnOK;
        private Button btnCancel;
    }
}
