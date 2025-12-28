namespace WinFormsFashionShop.Presentation.Forms
{
    partial class CategoryEditDialog
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
            lblName = new Label();
            txtName = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            chkIsActive = new CheckBox();
            pnlButtons = new Panel();
            btnCancel = new Button();
            btnOK = new Button();
            pnlHeader.SuspendLayout();
            pnlContent.SuspendLayout();
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
            pnlHeader.Size = new Size(514, 80);
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
            lblHeaderIcon.Text = "📁";
            lblHeaderIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(74, 24);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(234, 32);
            lblHeaderTitle.TabIndex = 1;
            lblHeaderTitle.Text = "Thông tin danh mục";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(lblName);
            pnlContent.Controls.Add(txtName);
            pnlContent.Controls.Add(lblDescription);
            pnlContent.Controls.Add(txtDescription);
            pnlContent.Controls.Add(chkIsActive);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 80);
            pnlContent.Margin = new Padding(3, 4, 3, 4);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(29, 27, 29, 13);
            pnlContent.Size = new Size(514, 280);
            pnlContent.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10F);
            lblName.ForeColor = Color.FromArgb(64, 64, 64);
            lblName.Location = new Point(16, 36);
            lblName.Name = "lblName";
            lblName.Size = new Size(155, 23);
            lblName.TabIndex = 0;
            lblName.Text = "🏷️  Tên danh mục:";
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 10F);
            txtName.Location = new Point(177, 29);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(308, 30);
            txtName.TabIndex = 1;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 10F);
            lblDescription.ForeColor = Color.FromArgb(64, 64, 64);
            lblDescription.Location = new Point(17, 85);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(92, 23);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "📝  Mô tả:";
            // 
            // txtDescription
            // 
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Font = new Font("Segoe UI", 10F);
            txtDescription.Location = new Point(177, 83);
            txtDescription.Margin = new Padding(3, 4, 3, 4);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(308, 93);
            txtDescription.TabIndex = 3;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Checked = true;
            chkIsActive.CheckState = CheckState.Checked;
            chkIsActive.Font = new Font("Segoe UI", 10F);
            chkIsActive.ForeColor = Color.FromArgb(64, 64, 64);
            chkIsActive.Location = new Point(177, 200);
            chkIsActive.Margin = new Padding(3, 4, 3, 4);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(147, 27);
            chkIsActive.TabIndex = 4;
            chkIsActive.Text = "✅  Hoạt động";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.FromArgb(248, 249, 250);
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnOK);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Location = new Point(0, 360);
            pnlButtons.Margin = new Padding(3, 4, 3, 4);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(514, 80);
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
            btnCancel.Location = new Point(377, 16);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(126, 51);
            btnCancel.TabIndex = 6;
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
            btnOK.Location = new Point(240, 16);
            btnOK.Margin = new Padding(3, 4, 3, 4);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(126, 51);
            btnOK.TabIndex = 5;
            btnOK.Text = "✓  Lưu";
            btnOK.UseVisualStyleBackColor = false;
            // 
            // CategoryEditDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnCancel;
            ClientSize = new Size(514, 440);
            Controls.Add(pnlContent);
            Controls.Add(pnlButtons);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CategoryEditDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thông tin danh mục";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderIcon;
        private Label lblHeaderTitle;
        private Panel pnlContent;
        private Label lblName;
        private TextBox txtName;
        private Label lblDescription;
        private TextBox txtDescription;
        private CheckBox chkIsActive;
        private Panel pnlButtons;
        private Button btnOK;
        private Button btnCancel;
    }
}
