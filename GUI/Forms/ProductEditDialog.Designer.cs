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
            btnCancel = new Button();
            btnOK = new Button();
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
            pnlHeader.Margin = new Padding(3, 4, 3, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(663, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderIcon
            // 
            lblHeaderIcon.Font = new Font("Segoe UI", 24F);
            lblHeaderIcon.ForeColor = Color.White;
            lblHeaderIcon.Location = new Point(17, 11);
            lblHeaderIcon.Name = "lblHeaderIcon";
            lblHeaderIcon.Size = new Size(57, 60);
            lblHeaderIcon.TabIndex = 0;
            lblHeaderIcon.Text = "👗";
            lblHeaderIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(74, 24);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(232, 32);
            lblHeaderTitle.TabIndex = 1;
            lblHeaderTitle.Text = "Thông tin sản phẩm";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(pnlLeft);
            pnlContent.Controls.Add(pnlRight);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 80);
            pnlContent.Margin = new Padding(3, 4, 3, 4);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(663, 387);
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
            pnlLeft.Margin = new Padding(3, 4, 3, 4);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new Padding(23, 20, 11, 20);
            pnlLeft.Size = new Size(434, 387);
            pnlLeft.TabIndex = 0;
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.Font = new Font("Segoe UI", 10F);
            lblCode.ForeColor = Color.FromArgb(64, 64, 64);
            lblCode.Location = new Point(23, 27);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(95, 23);
            lblCode.TabIndex = 0;
            lblCode.Text = "🏷️  Mã SP:";
            // 
            // txtCode
            // 
            txtCode.BorderStyle = BorderStyle.FixedSingle;
            txtCode.Font = new Font("Segoe UI", 10F);
            txtCode.Location = new Point(149, 23);
            txtCode.Margin = new Padding(3, 4, 3, 4);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(263, 30);
            txtCode.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10F);
            lblName.ForeColor = Color.FromArgb(64, 64, 64);
            lblName.Location = new Point(23, 77);
            lblName.Name = "lblName";
            lblName.Size = new Size(97, 23);
            lblName.TabIndex = 2;
            lblName.Text = "📝  Tên SP:";
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 10F);
            txtName.Location = new Point(149, 73);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(263, 30);
            txtName.TabIndex = 3;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 10F);
            lblCategory.ForeColor = Color.FromArgb(64, 64, 64);
            lblCategory.Location = new Point(23, 128);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(126, 23);
            lblCategory.TabIndex = 4;
            lblCategory.Text = "📁  Danh mục:";
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FlatStyle = FlatStyle.Flat;
            cmbCategory.Font = new Font("Segoe UI", 10F);
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(149, 124);
            cmbCategory.Margin = new Padding(3, 4, 3, 4);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(262, 31);
            cmbCategory.TabIndex = 5;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 10F);
            lblPrice.ForeColor = Color.FromArgb(64, 64, 64);
            lblPrice.Location = new Point(23, 179);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(106, 23);
            lblPrice.TabIndex = 6;
            lblPrice.Text = "💰  Giá bán:";
            // 
            // txtPrice
            // 
            txtPrice.BorderStyle = BorderStyle.FixedSingle;
            txtPrice.Font = new Font("Segoe UI", 10F);
            txtPrice.Location = new Point(149, 175);
            txtPrice.Margin = new Padding(3, 4, 3, 4);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(263, 30);
            txtPrice.TabIndex = 7;
            // 
            // lblUnit
            // 
            lblUnit.AutoSize = true;
            lblUnit.Font = new Font("Segoe UI", 10F);
            lblUnit.ForeColor = Color.FromArgb(64, 64, 64);
            lblUnit.Location = new Point(23, 229);
            lblUnit.Name = "lblUnit";
            lblUnit.Size = new Size(96, 23);
            lblUnit.TabIndex = 8;
            lblUnit.Text = "📦  Đơn vị:";
            // 
            // txtUnit
            // 
            txtUnit.BorderStyle = BorderStyle.FixedSingle;
            txtUnit.Font = new Font("Segoe UI", 10F);
            txtUnit.Location = new Point(149, 225);
            txtUnit.Margin = new Padding(3, 4, 3, 4);
            txtUnit.Name = "txtUnit";
            txtUnit.Size = new Size(263, 30);
            txtUnit.TabIndex = 9;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Checked = true;
            chkIsActive.CheckState = CheckState.Checked;
            chkIsActive.Font = new Font("Segoe UI", 10F);
            chkIsActive.ForeColor = Color.FromArgb(64, 64, 64);
            chkIsActive.Location = new Point(149, 280);
            chkIsActive.Margin = new Padding(3, 4, 3, 4);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(114, 27);
            chkIsActive.TabIndex = 10;
            chkIsActive.Text = "Hoạt động";
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
            pnlRight.Location = new Point(434, 0);
            pnlRight.Margin = new Padding(3, 4, 3, 4);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(11, 13, 11, 13);
            pnlRight.Size = new Size(229, 387);
            pnlRight.TabIndex = 1;
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblImage.ForeColor = Color.FromArgb(70, 130, 180);
            lblImage.Location = new Point(63, 20);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(110, 23);
            lblImage.TabIndex = 10;
            lblImage.Text = "📸 Hình ảnh";
            // 
            // picProductImage
            // 
            picProductImage.BackColor = Color.White;
            picProductImage.BorderStyle = BorderStyle.FixedSingle;
            picProductImage.Location = new Point(29, 60);
            picProductImage.Margin = new Padding(3, 4, 3, 4);
            picProductImage.Name = "picProductImage";
            picProductImage.Size = new Size(171, 199);
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
            btnSelectImage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSelectImage.ForeColor = Color.White;
            btnSelectImage.Location = new Point(29, 273);
            btnSelectImage.Margin = new Padding(3, 4, 3, 4);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(80, 43);
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
            btnRemoveImage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRemoveImage.ForeColor = Color.White;
            btnRemoveImage.Location = new Point(120, 273);
            btnRemoveImage.Margin = new Padding(3, 4, 3, 4);
            btnRemoveImage.Name = "btnRemoveImage";
            btnRemoveImage.Size = new Size(80, 43);
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
            pnlButtons.Location = new Point(0, 467);
            pnlButtons.Margin = new Padding(3, 4, 3, 4);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(663, 80);
            pnlButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 98, 104);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(526, 16);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(126, 51);
            btnCancel.TabIndex = 16;
            btnCancel.Text = "✕  Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.FromArgb(34, 139, 34);
            btnOK.Cursor = Cursors.Hand;
            btnOK.DialogResult = DialogResult.OK;
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 167, 69);
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnOK.ForeColor = Color.White;
            btnOK.Location = new Point(389, 16);
            btnOK.Margin = new Padding(3, 4, 3, 4);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(126, 51);
            btnOK.TabIndex = 15;
            btnOK.Text = "✓  Lưu";
            btnOK.UseVisualStyleBackColor = false;
            // 
            // ProductEditDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnCancel;
            ClientSize = new Size(663, 547);
            Controls.Add(pnlContent);
            Controls.Add(pnlButtons);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
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
