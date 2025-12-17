namespace WinFormsFashionShop.Presentation.Forms
{
    partial class UserManagementForm
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
            lblRoleFilter = new Label();
            cmbRoleFilter = new ComboBox();
            lblStatusFilter = new Label();
            cmbStatusFilter = new ComboBox();
            gridUsers = new DataGridView();
            pnlButtons = new Panel();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnChangePassword = new Button();
            btnActivate = new Button();
            btnRefresh = new Button();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridUsers).BeginInit();
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
            pnlHeader.Size = new Size(1024, 60);
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
            lblTitle.Size = new Size(320, 29);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "👤 QUẢN LÝ NGƯỜI DÙNG";
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = Color.White;
            pnlSearch.BorderStyle = BorderStyle.FixedSingle;
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Controls.Add(lblRoleFilter);
            pnlSearch.Controls.Add(cmbRoleFilter);
            pnlSearch.Controls.Add(lblStatusFilter);
            pnlSearch.Controls.Add(cmbStatusFilter);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 60);
            pnlSearch.Margin = new Padding(0);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(12, 10, 12, 10);
            pnlSearch.Size = new Size(1024, 70);
            pnlSearch.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Arial", 10F);
            txtSearch.Location = new Point(17, 27);
            txtSearch.Margin = new Padding(0);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Tìm theo tên đăng nhập/tên người dùng...";
            txtSearch.Size = new Size(285, 27);
            txtSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(0, 123, 255);
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Arial", 10F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(318, 19);
            btnSearch.Margin = new Padding(0);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(114, 37);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // lblRoleFilter
            // 
            lblRoleFilter.AutoSize = true;
            lblRoleFilter.Location = new Point(457, 28);
            lblRoleFilter.Name = "lblRoleFilter";
            lblRoleFilter.Size = new Size(55, 20);
            lblRoleFilter.TabIndex = 2;
            lblRoleFilter.Text = "Vai trò:";
            // 
            // cmbRoleFilter
            // 
            cmbRoleFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRoleFilter.FormattingEnabled = true;
            cmbRoleFilter.Location = new Point(525, 24);
            cmbRoleFilter.Margin = new Padding(3, 4, 3, 4);
            cmbRoleFilter.Name = "cmbRoleFilter";
            cmbRoleFilter.Size = new Size(137, 28);
            cmbRoleFilter.TabIndex = 3;
            // 
            // lblStatusFilter
            // 
            lblStatusFilter.AutoSize = true;
            lblStatusFilter.Location = new Point(685, 28);
            lblStatusFilter.Name = "lblStatusFilter";
            lblStatusFilter.Size = new Size(78, 20);
            lblStatusFilter.TabIndex = 4;
            lblStatusFilter.Text = "Trạng thái:";
            // 
            // cmbStatusFilter
            // 
            cmbStatusFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusFilter.FormattingEnabled = true;
            cmbStatusFilter.Location = new Point(789, 24);
            cmbStatusFilter.Margin = new Padding(0);
            cmbStatusFilter.Name = "cmbStatusFilter";
            cmbStatusFilter.Size = new Size(135, 28);
            cmbStatusFilter.TabIndex = 5;
            // 
            // gridUsers
            // 
            gridUsers.AllowUserToAddRows = false;
            gridUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridUsers.ColumnHeadersHeight = 32;
            gridUsers.Dock = DockStyle.Fill;
            gridUsers.Location = new Point(0, 130);
            gridUsers.Margin = new Padding(0);
            gridUsers.Name = "gridUsers";
            gridUsers.ReadOnly = true;
            gridUsers.RowHeadersWidth = 40;
            gridUsers.RowTemplate.Height = 32;
            gridUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridUsers.Size = new Size(1024, 530);
            gridUsers.TabIndex = 1;
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnAdd);
            pnlButtons.Controls.Add(btnEdit);
            pnlButtons.Controls.Add(btnDelete);
            pnlButtons.Controls.Add(btnChangePassword);
            pnlButtons.Controls.Add(btnActivate);
            pnlButtons.Controls.Add(btnRefresh);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Location = new Point(0, 660);
            pnlButtons.Margin = new Padding(0);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Padding = new Padding(12, 8, 12, 8);
            pnlButtons.Size = new Size(1024, 60);
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
            // btnChangePassword
            // 
            btnChangePassword.Location = new Point(330, 12);
            btnChangePassword.Margin = new Padding(0);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(120, 35);
            btnChangePassword.TabIndex = 3;
            btnChangePassword.Text = "Đổi mật khẩu";
            btnChangePassword.UseVisualStyleBackColor = true;
            // 
            // btnActivate
            // 
            btnActivate.Location = new Point(456, 12);
            btnActivate.Margin = new Padding(0);
            btnActivate.Name = "btnActivate";
            btnActivate.Size = new Size(130, 35);
            btnActivate.TabIndex = 4;
            btnActivate.Text = "Kích hoạt/Ngừng";
            btnActivate.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(592, 12);
            btnRefresh.Margin = new Padding(0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 35);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Làm mới";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // UserManagementForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 250);
            ClientSize = new Size(1024, 720);
            Controls.Add(gridUsers);
            Controls.Add(pnlButtons);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
            Margin = new Padding(0);
            MinimumSize = new Size(900, 600);
            Name = "UserManagementForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản lý Người dùng";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridUsers).EndInit();
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
        private System.Windows.Forms.Label lblRoleFilter;
        private System.Windows.Forms.ComboBox cmbRoleFilter;
        private System.Windows.Forms.Label lblStatusFilter;
        private System.Windows.Forms.ComboBox cmbStatusFilter;
        private System.Windows.Forms.DataGridView gridUsers;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.Button btnRefresh;
    }
}

