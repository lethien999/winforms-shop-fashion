namespace WinFormsFashionShop.Presentation.Forms
{
    partial class MainForm
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
            menuStrip = new MenuStrip();
            pnlHeader = new Panel();
            picLogo = new PictureBox();
            lblWelcome = new Label();
            lblUserInfo = new Label();
            pnlDashboard = new Panel();
            pnlRecentOrders = new Panel();
            gridRecentOrders = new DataGridView();
            colOrderCode = new DataGridViewTextBoxColumn();
            colOrderDate = new DataGridViewTextBoxColumn();
            colCustomerName = new DataGridViewTextBoxColumn();
            colTotalAmount = new DataGridViewTextBoxColumn();
            colPaymentMethod = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            lblRecentOrders = new Label();
            pnlQuickActions = new Panel();
            pnlActionsFlow = new FlowLayoutPanel();
            cardAction1 = new Panel();
            lblAction1Icon = new Label();
            lblAction1Title = new Label();
            lblAction1Desc = new Label();
            cardAction2 = new Panel();
            lblAction2Icon = new Label();
            lblAction2Title = new Label();
            lblAction2Desc = new Label();
            cardAction3 = new Panel();
            lblAction3Icon = new Label();
            lblAction3Title = new Label();
            lblAction3Desc = new Label();
            cardAction4 = new Panel();
            lblAction4Icon = new Label();
            lblAction4Title = new Label();
            lblAction4Desc = new Label();
            cardAction5 = new Panel();
            lblAction5Icon = new Label();
            lblAction5Title = new Label();
            lblAction5Desc = new Label();
            cardAction6 = new Panel();
            lblAction6Icon = new Label();
            lblAction6Title = new Label();
            lblAction6Desc = new Label();
            cardAction7 = new Panel();
            lblAction7Icon = new Label();
            lblAction7Title = new Label();
            lblAction7Desc = new Label();
            lblQuickActions = new Label();
            pnlStatsCards = new FlowLayoutPanel();
            cardStat1 = new Panel();
            lblStat1Icon = new Label();
            lblStat1Title = new Label();
            lblStat1Value = new Label();
            cardStat2 = new Panel();
            lblStat2Icon = new Label();
            lblStat2Title = new Label();
            lblStat2Value = new Label();
            cardStat3 = new Panel();
            lblStat3Icon = new Label();
            lblStat3Title = new Label();
            lblStat3Value = new Label();
            cardStat4 = new Panel();
            lblStat4Icon = new Label();
            lblStat4Title = new Label();
            lblStat4Value = new Label();
            cardStat5 = new Panel();
            lblStat5Icon = new Label();
            lblStat5Title = new Label();
            lblStat5Value = new Label();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlDashboard.SuspendLayout();
            pnlRecentOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridRecentOrders).BeginInit();
            pnlQuickActions.SuspendLayout();
            pnlActionsFlow.SuspendLayout();
            cardAction1.SuspendLayout();
            cardAction2.SuspendLayout();
            cardAction3.SuspendLayout();
            cardAction4.SuspendLayout();
            cardAction5.SuspendLayout();
            cardAction6.SuspendLayout();
            cardAction7.SuspendLayout();
            pnlStatsCards.SuspendLayout();
            cardStat1.SuspendLayout();
            cardStat2.SuspendLayout();
            cardStat3.SuspendLayout();
            cardStat4.SuspendLayout();
            cardStat5.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.BackColor = Color.FromArgb(70, 130, 180);
            menuStrip.ForeColor = Color.White;
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(7, 3, 0, 3);
            menuStrip.Size = new Size(1583, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(70, 130, 180);
            pnlHeader.Controls.Add(picLogo);
            pnlHeader.Controls.Add(lblWelcome);
            pnlHeader.Controls.Add(lblUserInfo);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 24);
            pnlHeader.Margin = new Padding(3, 4, 3, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(23, 20, 23, 20);
            pnlHeader.Size = new Size(1583, 107);
            pnlHeader.TabIndex = 1;
            // 
            // picLogo
            // 
            picLogo.Image = GUI.Properties.Resources.Logo_3T;
            picLogo.Location = new Point(23, 20);
            picLogo.Margin = new Padding(3, 4, 3, 4);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(57, 67);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Arial", 18F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(91, 27);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(309, 35);
            lblWelcome.TabIndex = 1;
            lblWelcome.Text = "🏠 Dashboard Chính";
            // 
            // lblUserInfo
            // 
            lblUserInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserInfo.Font = new Font("Arial", 10F);
            lblUserInfo.ForeColor = Color.White;
            lblUserInfo.Location = new Point(1275, 27);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(286, 40);
            lblUserInfo.TabIndex = 2;
            lblUserInfo.Text = "Người dùng: ... | Vai trò: ...";
            lblUserInfo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlDashboard
            // 
            pnlDashboard.AutoScroll = true;
            pnlDashboard.Controls.Add(pnlRecentOrders);
            pnlDashboard.Controls.Add(pnlQuickActions);
            pnlDashboard.Controls.Add(pnlStatsCards);
            pnlDashboard.Dock = DockStyle.Fill;
            pnlDashboard.Location = new Point(0, 131);
            pnlDashboard.Margin = new Padding(3, 4, 3, 4);
            pnlDashboard.Name = "pnlDashboard";
            pnlDashboard.Padding = new Padding(17, 20, 17, 20);
            pnlDashboard.Size = new Size(1583, 892);
            pnlDashboard.TabIndex = 2;
            // 
            // pnlRecentOrders
            // 
            pnlRecentOrders.AutoSize = true;
            pnlRecentOrders.Controls.Add(gridRecentOrders);
            pnlRecentOrders.Controls.Add(lblRecentOrders);
            pnlRecentOrders.Dock = DockStyle.Top;
            pnlRecentOrders.Location = new Point(17, 547);
            pnlRecentOrders.Margin = new Padding(0);
            pnlRecentOrders.Name = "pnlRecentOrders";
            pnlRecentOrders.Size = new Size(1549, 334);
            pnlRecentOrders.TabIndex = 2;
            // 
            // gridRecentOrders
            // 
            gridRecentOrders.AllowUserToAddRows = false;
            gridRecentOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridRecentOrders.BackgroundColor = Color.White;
            gridRecentOrders.BorderStyle = BorderStyle.None;
            gridRecentOrders.ColumnHeadersHeight = 35;
            gridRecentOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridRecentOrders.Columns.AddRange(new DataGridViewColumn[] { colOrderCode, colOrderDate, colCustomerName, colTotalAmount, colPaymentMethod, colStatus });
            gridRecentOrders.EnableHeadersVisualStyles = false;
            gridRecentOrders.Location = new Point(0, 47);
            gridRecentOrders.Margin = new Padding(0, 11, 0, 0);
            gridRecentOrders.MultiSelect = false;
            gridRecentOrders.Name = "gridRecentOrders";
            gridRecentOrders.ReadOnly = true;
            gridRecentOrders.RowHeadersWidth = 40;
            gridRecentOrders.RowTemplate.Height = 32;
            gridRecentOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridRecentOrders.Size = new Size(1337, 287);
            gridRecentOrders.TabIndex = 1;
            // 
            // colOrderCode
            // 
            colOrderCode.HeaderText = "Mã đơn";
            colOrderCode.MinimumWidth = 6;
            colOrderCode.Name = "colOrderCode";
            colOrderCode.ReadOnly = true;
            // 
            // colOrderDate
            // 
            colOrderDate.HeaderText = "Ngày đặt";
            colOrderDate.MinimumWidth = 6;
            colOrderDate.Name = "colOrderDate";
            colOrderDate.ReadOnly = true;
            // 
            // colCustomerName
            // 
            colCustomerName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCustomerName.HeaderText = "Khách hàng";
            colCustomerName.MinimumWidth = 6;
            colCustomerName.Name = "colCustomerName";
            colCustomerName.ReadOnly = true;
            // 
            // colTotalAmount
            // 
            colTotalAmount.HeaderText = "Tổng tiền";
            colTotalAmount.MinimumWidth = 6;
            colTotalAmount.Name = "colTotalAmount";
            colTotalAmount.ReadOnly = true;
            // 
            // colPaymentMethod
            // 
            colPaymentMethod.HeaderText = "Phương thức TT";
            colPaymentMethod.MinimumWidth = 6;
            colPaymentMethod.Name = "colPaymentMethod";
            colPaymentMethod.ReadOnly = true;
            // 
            // colStatus
            // 
            colStatus.HeaderText = "Trạng thái";
            colStatus.MinimumWidth = 6;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            // 
            // lblRecentOrders
            // 
            lblRecentOrders.AutoSize = true;
            lblRecentOrders.Font = new Font("Arial", 14F, FontStyle.Bold);
            lblRecentOrders.ForeColor = Color.FromArgb(30, 30, 40);
            lblRecentOrders.Location = new Point(0, 0);
            lblRecentOrders.Name = "lblRecentOrders";
            lblRecentOrders.Size = new Size(259, 29);
            lblRecentOrders.TabIndex = 0;
            lblRecentOrders.Text = "📋 Đơn hàng gần đây";
            // 
            // pnlQuickActions
            // 
            pnlQuickActions.AutoSize = true;
            pnlQuickActions.Controls.Add(pnlActionsFlow);
            pnlQuickActions.Controls.Add(lblQuickActions);
            pnlQuickActions.Dock = DockStyle.Top;
            pnlQuickActions.Location = new Point(17, 240);
            pnlQuickActions.Margin = new Padding(3, 4, 3, 4);
            pnlQuickActions.Name = "pnlQuickActions";
            pnlQuickActions.Padding = new Padding(0, 0, 0, 20);
            pnlQuickActions.Size = new Size(1549, 307);
            pnlQuickActions.TabIndex = 1;
            // 
            // pnlActionsFlow
            // 
            pnlActionsFlow.AutoScroll = true;
            pnlActionsFlow.AutoSize = true;
            pnlActionsFlow.Controls.Add(cardAction1);
            pnlActionsFlow.Controls.Add(cardAction2);
            pnlActionsFlow.Controls.Add(cardAction3);
            pnlActionsFlow.Controls.Add(cardAction4);
            pnlActionsFlow.Controls.Add(cardAction5);
            pnlActionsFlow.Controls.Add(cardAction6);
            pnlActionsFlow.Controls.Add(cardAction7);
            pnlActionsFlow.Location = new Point(0, 47);
            pnlActionsFlow.Margin = new Padding(0);
            pnlActionsFlow.Name = "pnlActionsFlow";
            pnlActionsFlow.Size = new Size(1543, 240);
            pnlActionsFlow.TabIndex = 1;
            pnlActionsFlow.WrapContents = false;
            // 
            // cardAction1
            // 
            cardAction1.BackColor = Color.White;
            cardAction1.BorderStyle = BorderStyle.FixedSingle;
            cardAction1.Controls.Add(lblAction1Icon);
            cardAction1.Controls.Add(lblAction1Title);
            cardAction1.Controls.Add(lblAction1Desc);
            cardAction1.Location = new Point(0, 0);
            cardAction1.Margin = new Padding(0, 0, 17, 20);
            cardAction1.Name = "cardAction1";
            cardAction1.Padding = new Padding(17, 20, 17, 20);
            cardAction1.Size = new Size(205, 219);
            cardAction1.TabIndex = 0;
            // 
            // lblAction1Icon
            // 
            lblAction1Icon.AutoSize = true;
            lblAction1Icon.Font = new Font("Segoe UI Emoji", 32F, FontStyle.Bold);
            lblAction1Icon.ForeColor = Color.FromArgb(70, 130, 180);
            lblAction1Icon.Location = new Point(46, 16);
            lblAction1Icon.Name = "lblAction1Icon";
            lblAction1Icon.Size = new Size(104, 72);
            lblAction1Icon.TabIndex = 0;
            lblAction1Icon.Text = "📝";
            // 
            // lblAction1Title
            // 
            lblAction1Title.Font = new Font("Arial", 11F, FontStyle.Bold);
            lblAction1Title.ForeColor = Color.FromArgb(30, 30, 40);
            lblAction1Title.Location = new Point(14, 108);
            lblAction1Title.Name = "lblAction1Title";
            lblAction1Title.Size = new Size(178, 29);
            lblAction1Title.TabIndex = 1;
            lblAction1Title.Text = "Lập hóa đơn";
            // 
            // lblAction1Desc
            // 
            lblAction1Desc.Font = new Font("Arial", 9F);
            lblAction1Desc.ForeColor = Color.FromArgb(108, 117, 125);
            lblAction1Desc.Location = new Point(14, 155);
            lblAction1Desc.Name = "lblAction1Desc";
            lblAction1Desc.Size = new Size(178, 42);
            lblAction1Desc.TabIndex = 2;
            lblAction1Desc.Text = "Tạo đơn hàng mới";
            // 
            // cardAction2
            // 
            cardAction2.BackColor = Color.White;
            cardAction2.BorderStyle = BorderStyle.FixedSingle;
            cardAction2.Controls.Add(lblAction2Icon);
            cardAction2.Controls.Add(lblAction2Title);
            cardAction2.Controls.Add(lblAction2Desc);
            cardAction2.Location = new Point(222, 0);
            cardAction2.Margin = new Padding(0, 0, 17, 20);
            cardAction2.Name = "cardAction2";
            cardAction2.Padding = new Padding(17, 20, 17, 20);
            cardAction2.Size = new Size(205, 219);
            cardAction2.TabIndex = 1;
            // 
            // lblAction2Icon
            // 
            lblAction2Icon.AutoSize = true;
            lblAction2Icon.Font = new Font("Segoe UI Emoji", 32F, FontStyle.Bold);
            lblAction2Icon.ForeColor = Color.FromArgb(70, 130, 180);
            lblAction2Icon.Location = new Point(56, 16);
            lblAction2Icon.Name = "lblAction2Icon";
            lblAction2Icon.Size = new Size(104, 72);
            lblAction2Icon.TabIndex = 0;
            lblAction2Icon.Text = "🛍️";
            // 
            // lblAction2Title
            // 
            lblAction2Title.Font = new Font("Arial", 11F, FontStyle.Bold);
            lblAction2Title.ForeColor = Color.FromArgb(30, 30, 40);
            lblAction2Title.Location = new Point(14, 111);
            lblAction2Title.Name = "lblAction2Title";
            lblAction2Title.Size = new Size(178, 29);
            lblAction2Title.TabIndex = 1;
            lblAction2Title.Text = "Quản lý sản phẩm";
            // 
            // lblAction2Desc
            // 
            lblAction2Desc.Font = new Font("Arial", 9F);
            lblAction2Desc.ForeColor = Color.FromArgb(108, 117, 125);
            lblAction2Desc.Location = new Point(10, 155);
            lblAction2Desc.Name = "lblAction2Desc";
            lblAction2Desc.Size = new Size(178, 36);
            lblAction2Desc.TabIndex = 2;
            lblAction2Desc.Text = "Xem, thêm, sửa sản phẩm";
            // 
            // cardAction3
            // 
            cardAction3.BackColor = Color.White;
            cardAction3.BorderStyle = BorderStyle.FixedSingle;
            cardAction3.Controls.Add(lblAction3Icon);
            cardAction3.Controls.Add(lblAction3Title);
            cardAction3.Controls.Add(lblAction3Desc);
            cardAction3.Location = new Point(444, 0);
            cardAction3.Margin = new Padding(0, 0, 17, 20);
            cardAction3.Name = "cardAction3";
            cardAction3.Padding = new Padding(17, 20, 17, 20);
            cardAction3.Size = new Size(205, 219);
            cardAction3.TabIndex = 2;
            // 
            // lblAction3Icon
            // 
            lblAction3Icon.AutoSize = true;
            lblAction3Icon.Font = new Font("Segoe UI Emoji", 32F, FontStyle.Bold);
            lblAction3Icon.ForeColor = Color.FromArgb(70, 130, 180);
            lblAction3Icon.Location = new Point(50, 16);
            lblAction3Icon.Name = "lblAction3Icon";
            lblAction3Icon.Size = new Size(104, 72);
            lblAction3Icon.TabIndex = 0;
            lblAction3Icon.Text = "📋";
            // 
            // lblAction3Title
            // 
            lblAction3Title.Font = new Font("Arial", 11F, FontStyle.Bold);
            lblAction3Title.ForeColor = Color.FromArgb(30, 30, 40);
            lblAction3Title.Location = new Point(11, 108);
            lblAction3Title.Name = "lblAction3Title";
            lblAction3Title.Size = new Size(178, 29);
            lblAction3Title.TabIndex = 1;
            lblAction3Title.Text = "Quản lý đơn hàng";
            // 
            // lblAction3Desc
            // 
            lblAction3Desc.Font = new Font("Arial", 9F);
            lblAction3Desc.ForeColor = Color.FromArgb(108, 117, 125);
            lblAction3Desc.Location = new Point(14, 155);
            lblAction3Desc.Name = "lblAction3Desc";
            lblAction3Desc.Size = new Size(178, 36);
            lblAction3Desc.TabIndex = 2;
            lblAction3Desc.Text = "Xem danh sách đơn hàng";
            // 
            // cardAction4
            // 
            cardAction4.BackColor = Color.White;
            cardAction4.BorderStyle = BorderStyle.FixedSingle;
            cardAction4.Controls.Add(lblAction4Icon);
            cardAction4.Controls.Add(lblAction4Title);
            cardAction4.Controls.Add(lblAction4Desc);
            cardAction4.Location = new Point(666, 0);
            cardAction4.Margin = new Padding(0, 0, 17, 20);
            cardAction4.Name = "cardAction4";
            cardAction4.Padding = new Padding(17, 20, 17, 20);
            cardAction4.Size = new Size(205, 219);
            cardAction4.TabIndex = 3;
            // 
            // lblAction4Icon
            // 
            lblAction4Icon.AutoSize = true;
            lblAction4Icon.Font = new Font("Segoe UI Emoji", 32F, FontStyle.Bold);
            lblAction4Icon.ForeColor = Color.FromArgb(70, 130, 180);
            lblAction4Icon.Location = new Point(54, 16);
            lblAction4Icon.Name = "lblAction4Icon";
            lblAction4Icon.Size = new Size(104, 72);
            lblAction4Icon.TabIndex = 0;
            lblAction4Icon.Text = "👥";
            // 
            // lblAction4Title
            // 
            lblAction4Title.Font = new Font("Arial", 11F, FontStyle.Bold);
            lblAction4Title.ForeColor = Color.FromArgb(30, 30, 40);
            lblAction4Title.Location = new Point(14, 111);
            lblAction4Title.Name = "lblAction4Title";
            lblAction4Title.Size = new Size(178, 29);
            lblAction4Title.TabIndex = 1;
            lblAction4Title.Text = "Quản lý khách hàng";
            // 
            // lblAction4Desc
            // 
            lblAction4Desc.Font = new Font("Arial", 9F);
            lblAction4Desc.ForeColor = Color.FromArgb(108, 117, 125);
            lblAction4Desc.Location = new Point(14, 155);
            lblAction4Desc.Name = "lblAction4Desc";
            lblAction4Desc.Size = new Size(178, 36);
            lblAction4Desc.TabIndex = 2;
            lblAction4Desc.Text = "Xem thông tin khách hàng";
            // 
            // cardAction5
            // 
            cardAction5.BackColor = Color.White;
            cardAction5.BorderStyle = BorderStyle.FixedSingle;
            cardAction5.Controls.Add(lblAction5Icon);
            cardAction5.Controls.Add(lblAction5Title);
            cardAction5.Controls.Add(lblAction5Desc);
            cardAction5.Location = new Point(888, 0);
            cardAction5.Margin = new Padding(0, 0, 17, 20);
            cardAction5.Name = "cardAction5";
            cardAction5.Padding = new Padding(17, 20, 17, 20);
            cardAction5.Size = new Size(205, 219);
            cardAction5.TabIndex = 4;
            // 
            // lblAction5Icon
            // 
            lblAction5Icon.AutoSize = true;
            lblAction5Icon.Font = new Font("Segoe UI Emoji", 32F, FontStyle.Bold);
            lblAction5Icon.ForeColor = Color.FromArgb(70, 130, 180);
            lblAction5Icon.Location = new Point(47, 20);
            lblAction5Icon.Name = "lblAction5Icon";
            lblAction5Icon.Size = new Size(104, 72);
            lblAction5Icon.TabIndex = 0;
            lblAction5Icon.Text = "📦";
            // 
            // lblAction5Title
            // 
            lblAction5Title.Font = new Font("Arial", 11F, FontStyle.Bold);
            lblAction5Title.ForeColor = Color.FromArgb(30, 30, 40);
            lblAction5Title.Location = new Point(47, 108);
            lblAction5Title.Name = "lblAction5Title";
            lblAction5Title.Size = new Size(114, 29);
            lblAction5Title.TabIndex = 1;
            lblAction5Title.Text = "Nhập hàng";
            // 
            // lblAction5Desc
            // 
            lblAction5Desc.Font = new Font("Arial", 9F);
            lblAction5Desc.ForeColor = Color.FromArgb(108, 117, 125);
            lblAction5Desc.Location = new Point(14, 155);
            lblAction5Desc.Name = "lblAction5Desc";
            lblAction5Desc.Size = new Size(178, 36);
            lblAction5Desc.TabIndex = 2;
            lblAction5Desc.Text = "Cập nhật tồn kho";
            // 
            // cardAction6
            // 
            cardAction6.BackColor = Color.White;
            cardAction6.BorderStyle = BorderStyle.FixedSingle;
            cardAction6.Controls.Add(lblAction6Icon);
            cardAction6.Controls.Add(lblAction6Title);
            cardAction6.Controls.Add(lblAction6Desc);
            cardAction6.Location = new Point(1110, 0);
            cardAction6.Margin = new Padding(0, 0, 0, 20);
            cardAction6.Name = "cardAction6";
            cardAction6.Padding = new Padding(17, 20, 17, 20);
            cardAction6.Size = new Size(205, 219);
            cardAction6.TabIndex = 5;
            // 
            // lblAction6Icon
            // 
            lblAction6Icon.AutoSize = true;
            lblAction6Icon.Font = new Font("Segoe UI Emoji", 32F, FontStyle.Bold);
            lblAction6Icon.ForeColor = Color.FromArgb(70, 130, 180);
            lblAction6Icon.Location = new Point(59, 16);
            lblAction6Icon.Name = "lblAction6Icon";
            lblAction6Icon.Size = new Size(104, 72);
            lblAction6Icon.TabIndex = 0;
            lblAction6Icon.Text = "📊";
            // 
            // lblAction6Title
            // 
            lblAction6Title.Font = new Font("Arial", 11F, FontStyle.Bold);
            lblAction6Title.ForeColor = Color.FromArgb(30, 30, 40);
            lblAction6Title.Location = new Point(65, 108);
            lblAction6Title.Name = "lblAction6Title";
            lblAction6Title.Size = new Size(98, 29);
            lblAction6Title.TabIndex = 1;
            lblAction6Title.Text = "Báo cáo";
            // 
            // lblAction6Desc
            // 
            lblAction6Desc.Font = new Font("Arial", 9F);
            lblAction6Desc.ForeColor = Color.FromArgb(108, 117, 125);
            lblAction6Desc.Location = new Point(14, 155);
            lblAction6Desc.Name = "lblAction6Desc";
            lblAction6Desc.Size = new Size(178, 36);
            lblAction6Desc.TabIndex = 2;
            lblAction6Desc.Text = "Xem báo cáo doanh thu";
            // 
            // cardAction7
            // 
            cardAction7.BackColor = Color.White;
            cardAction7.BorderStyle = BorderStyle.FixedSingle;
            cardAction7.Controls.Add(lblAction7Icon);
            cardAction7.Controls.Add(lblAction7Title);
            cardAction7.Controls.Add(lblAction7Desc);
            cardAction7.Location = new Point(1315, 0);
            cardAction7.Margin = new Padding(0, 0, 0, 20);
            cardAction7.Name = "cardAction7";
            cardAction7.Padding = new Padding(17, 20, 17, 20);
            cardAction7.Size = new Size(205, 219);
            cardAction7.TabIndex = 6;
            // 
            // lblAction7Icon
            // 
            lblAction7Icon.AutoSize = true;
            lblAction7Icon.Font = new Font("Segoe UI Emoji", 32F, FontStyle.Bold);
            lblAction7Icon.ForeColor = Color.FromArgb(70, 130, 180);
            lblAction7Icon.Location = new Point(58, 20);
            lblAction7Icon.Name = "lblAction7Icon";
            lblAction7Icon.Size = new Size(104, 72);
            lblAction7Icon.TabIndex = 0;
            lblAction7Icon.Text = "👤";
            // 
            // lblAction7Title
            // 
            lblAction7Title.Font = new Font("Arial", 11F, FontStyle.Bold);
            lblAction7Title.ForeColor = Color.FromArgb(30, 30, 40);
            lblAction7Title.Location = new Point(14, 108);
            lblAction7Title.Name = "lblAction7Title";
            lblAction7Title.Size = new Size(178, 29);
            lblAction7Title.TabIndex = 1;
            lblAction7Title.Text = "Quản lý người dùng";
            // 
            // lblAction7Desc
            // 
            lblAction7Desc.Font = new Font("Arial", 9F);
            lblAction7Desc.ForeColor = Color.FromArgb(108, 117, 125);
            lblAction7Desc.Location = new Point(14, 155);
            lblAction7Desc.Name = "lblAction7Desc";
            lblAction7Desc.Size = new Size(178, 36);
            lblAction7Desc.TabIndex = 2;
            lblAction7Desc.Text = "Quản trị hệ thống";
            // 
            // lblQuickActions
            // 
            lblQuickActions.AutoSize = true;
            lblQuickActions.Font = new Font("Arial", 14F, FontStyle.Bold);
            lblQuickActions.ForeColor = Color.FromArgb(30, 30, 40);
            lblQuickActions.Location = new Point(0, 0);
            lblQuickActions.Name = "lblQuickActions";
            lblQuickActions.Size = new Size(226, 29);
            lblQuickActions.TabIndex = 0;
            lblQuickActions.Text = "⚡ Thao tác nhanh";
            // 
            // pnlStatsCards
            // 
            pnlStatsCards.AutoSize = true;
            pnlStatsCards.Controls.Add(cardStat1);
            pnlStatsCards.Controls.Add(cardStat2);
            pnlStatsCards.Controls.Add(cardStat3);
            pnlStatsCards.Controls.Add(cardStat4);
            pnlStatsCards.Controls.Add(cardStat5);
            pnlStatsCards.Dock = DockStyle.Top;
            pnlStatsCards.Location = new Point(17, 20);
            pnlStatsCards.Margin = new Padding(0);
            pnlStatsCards.Name = "pnlStatsCards";
            pnlStatsCards.Padding = new Padding(0, 0, 0, 20);
            pnlStatsCards.Size = new Size(1549, 220);
            pnlStatsCards.TabIndex = 0;
            pnlStatsCards.WrapContents = false;
            // 
            // cardStat1
            // 
            cardStat1.BackColor = Color.FromArgb(34, 193, 195);
            cardStat1.Controls.Add(lblStat1Icon);
            cardStat1.Controls.Add(lblStat1Title);
            cardStat1.Controls.Add(lblStat1Value);
            cardStat1.Location = new Point(0, 0);
            cardStat1.Margin = new Padding(0, 0, 17, 0);
            cardStat1.Name = "cardStat1";
            cardStat1.Padding = new Padding(17, 20, 17, 20);
            cardStat1.Size = new Size(206, 200);
            cardStat1.TabIndex = 0;
            // 
            // lblStat1Icon
            // 
            lblStat1Icon.AutoSize = true;
            lblStat1Icon.Font = new Font("Segoe UI Emoji", 28F, FontStyle.Bold);
            lblStat1Icon.ForeColor = Color.White;
            lblStat1Icon.Location = new Point(58, 0);
            lblStat1Icon.Name = "lblStat1Icon";
            lblStat1Icon.Size = new Size(93, 63);
            lblStat1Icon.TabIndex = 0;
            lblStat1Icon.Text = "💰";
            // 
            // lblStat1Title
            // 
            lblStat1Title.Font = new Font("Arial", 9F);
            lblStat1Title.ForeColor = Color.FromArgb(200, 255, 255, 255);
            lblStat1Title.Location = new Point(17, 91);
            lblStat1Title.Name = "lblStat1Title";
            lblStat1Title.Size = new Size(171, 23);
            lblStat1Title.TabIndex = 1;
            lblStat1Title.Text = "Doanh thu tháng này";
            lblStat1Title.Click += lblStat1Title_Click;
            // 
            // lblStat1Value
            // 
            lblStat1Value.Font = new Font("Arial", 20F, FontStyle.Bold);
            lblStat1Value.ForeColor = Color.White;
            lblStat1Value.Location = new Point(15, 127);
            lblStat1Value.Name = "lblStat1Value";
            lblStat1Value.Size = new Size(171, 53);
            lblStat1Value.TabIndex = 2;
            lblStat1Value.Text = "0 VNĐ";
            // 
            // cardStat2
            // 
            cardStat2.BackColor = Color.FromArgb(251, 176, 64);
            cardStat2.Controls.Add(lblStat2Icon);
            cardStat2.Controls.Add(lblStat2Title);
            cardStat2.Controls.Add(lblStat2Value);
            cardStat2.Location = new Point(223, 0);
            cardStat2.Margin = new Padding(0, 0, 17, 0);
            cardStat2.Name = "cardStat2";
            cardStat2.Padding = new Padding(17, 20, 17, 20);
            cardStat2.Size = new Size(206, 200);
            cardStat2.TabIndex = 1;
            // 
            // lblStat2Icon
            // 
            lblStat2Icon.AutoSize = true;
            lblStat2Icon.Font = new Font("Segoe UI Emoji", 28F, FontStyle.Bold);
            lblStat2Icon.ForeColor = Color.White;
            lblStat2Icon.Location = new Point(56, 0);
            lblStat2Icon.Name = "lblStat2Icon";
            lblStat2Icon.Size = new Size(93, 63);
            lblStat2Icon.TabIndex = 0;
            lblStat2Icon.Text = "📦";
            // 
            // lblStat2Title
            // 
            lblStat2Title.Font = new Font("Arial", 9F);
            lblStat2Title.ForeColor = Color.FromArgb(200, 255, 255, 255);
            lblStat2Title.Location = new Point(17, 91);
            lblStat2Title.Name = "lblStat2Title";
            lblStat2Title.Size = new Size(171, 23);
            lblStat2Title.TabIndex = 1;
            lblStat2Title.Text = "Đơn hàng hôm nay";
            // 
            // lblStat2Value
            // 
            lblStat2Value.Font = new Font("Arial", 20F, FontStyle.Bold);
            lblStat2Value.ForeColor = Color.White;
            lblStat2Value.Location = new Point(20, 127);
            lblStat2Value.Name = "lblStat2Value";
            lblStat2Value.Size = new Size(171, 53);
            lblStat2Value.TabIndex = 2;
            lblStat2Value.Text = "0 đơn";
            // 
            // cardStat3
            // 
            cardStat3.BackColor = Color.FromArgb(149, 117, 205);
            cardStat3.Controls.Add(lblStat3Icon);
            cardStat3.Controls.Add(lblStat3Title);
            cardStat3.Controls.Add(lblStat3Value);
            cardStat3.Location = new Point(446, 0);
            cardStat3.Margin = new Padding(0, 0, 17, 0);
            cardStat3.Name = "cardStat3";
            cardStat3.Padding = new Padding(17, 20, 17, 20);
            cardStat3.Size = new Size(238, 200);
            cardStat3.TabIndex = 2;
            // 
            // lblStat3Icon
            // 
            lblStat3Icon.AutoSize = true;
            lblStat3Icon.Font = new Font("Segoe UI Emoji", 28F, FontStyle.Bold);
            lblStat3Icon.ForeColor = Color.White;
            lblStat3Icon.Location = new Point(60, 0);
            lblStat3Icon.Name = "lblStat3Icon";
            lblStat3Icon.Size = new Size(93, 63);
            lblStat3Icon.TabIndex = 0;
            lblStat3Icon.Text = "🛍️";
            // 
            // lblStat3Title
            // 
            lblStat3Title.Font = new Font("Arial", 9F);
            lblStat3Title.ForeColor = Color.FromArgb(200, 255, 255, 255);
            lblStat3Title.Location = new Point(17, 91);
            lblStat3Title.Name = "lblStat3Title";
            lblStat3Title.Size = new Size(171, 23);
            lblStat3Title.TabIndex = 1;
            lblStat3Title.Text = "Tổng sản phẩm";
            // 
            // lblStat3Value
            // 
            lblStat3Value.Font = new Font("Arial", 20F, FontStyle.Bold);
            lblStat3Value.ForeColor = Color.White;
            lblStat3Value.Location = new Point(10, 127);
            lblStat3Value.Name = "lblStat3Value";
            lblStat3Value.Size = new Size(221, 53);
            lblStat3Value.TabIndex = 2;
            lblStat3Value.Text = "0 sản phẩm";
            // 
            // cardStat4
            // 
            cardStat4.BackColor = Color.FromArgb(220, 53, 69);
            cardStat4.Controls.Add(lblStat4Icon);
            cardStat4.Controls.Add(lblStat4Title);
            cardStat4.Controls.Add(lblStat4Value);
            cardStat4.Location = new Point(701, 0);
            cardStat4.Margin = new Padding(0, 0, 17, 0);
            cardStat4.Name = "cardStat4";
            cardStat4.Padding = new Padding(17, 20, 17, 20);
            cardStat4.Size = new Size(227, 200);
            cardStat4.TabIndex = 3;
            // 
            // lblStat4Icon
            // 
            lblStat4Icon.AutoSize = true;
            lblStat4Icon.Font = new Font("Segoe UI Emoji", 28F, FontStyle.Bold);
            lblStat4Icon.ForeColor = Color.White;
            lblStat4Icon.Location = new Point(75, 4);
            lblStat4Icon.Name = "lblStat4Icon";
            lblStat4Icon.Size = new Size(93, 63);
            lblStat4Icon.TabIndex = 0;
            lblStat4Icon.Text = "👥";
            // 
            // lblStat4Title
            // 
            lblStat4Title.Font = new Font("Arial", 9F);
            lblStat4Title.ForeColor = Color.FromArgb(200, 255, 255, 255);
            lblStat4Title.Location = new Point(17, 91);
            lblStat4Title.Name = "lblStat4Title";
            lblStat4Title.Size = new Size(171, 23);
            lblStat4Title.TabIndex = 1;
            lblStat4Title.Text = "Tổng khách hàng";
            // 
            // lblStat4Value
            // 
            lblStat4Value.Font = new Font("Arial", 20F, FontStyle.Bold);
            lblStat4Value.ForeColor = Color.White;
            lblStat4Value.Location = new Point(17, 127);
            lblStat4Value.Name = "lblStat4Value";
            lblStat4Value.Size = new Size(151, 53);
            lblStat4Value.TabIndex = 2;
            lblStat4Value.Text = "0 khách";
            // 
            // cardStat5
            // 
            cardStat5.BackColor = Color.FromArgb(255, 193, 7);
            cardStat5.Controls.Add(lblStat5Icon);
            cardStat5.Controls.Add(lblStat5Title);
            cardStat5.Controls.Add(lblStat5Value);
            cardStat5.Location = new Point(945, 0);
            cardStat5.Margin = new Padding(0);
            cardStat5.Name = "cardStat5";
            cardStat5.Padding = new Padding(17, 20, 17, 20);
            cardStat5.Size = new Size(232, 200);
            cardStat5.TabIndex = 4;
            // 
            // lblStat5Icon
            // 
            lblStat5Icon.AutoSize = true;
            lblStat5Icon.Font = new Font("Segoe UI Emoji", 28F, FontStyle.Bold);
            lblStat5Icon.ForeColor = Color.White;
            lblStat5Icon.Location = new Point(75, 0);
            lblStat5Icon.Name = "lblStat5Icon";
            lblStat5Icon.Size = new Size(93, 63);
            lblStat5Icon.TabIndex = 0;
            lblStat5Icon.Text = "⚠️";
            // 
            // lblStat5Title
            // 
            lblStat5Title.Font = new Font("Arial", 9F);
            lblStat5Title.ForeColor = Color.FromArgb(200, 255, 255, 255);
            lblStat5Title.Location = new Point(17, 91);
            lblStat5Title.Name = "lblStat5Title";
            lblStat5Title.Size = new Size(171, 23);
            lblStat5Title.TabIndex = 1;
            lblStat5Title.Text = "Cảnh báo tồn kho";
            // 
            // lblStat5Value
            // 
            lblStat5Value.Font = new Font("Arial", 20F, FontStyle.Bold);
            lblStat5Value.ForeColor = Color.White;
            lblStat5Value.Location = new Point(3, 127);
            lblStat5Value.Name = "lblStat5Value";
            lblStat5Value.Size = new Size(215, 52);
            lblStat5Value.TabIndex = 2;
            lblStat5Value.Text = "0 sản phẩm";
            // 
            // statusStrip
            // 
            statusStrip.BackColor = Color.FromArgb(240, 240, 240);
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 1023);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(1583, 26);
            statusStrip.TabIndex = 3;
            statusStrip.Text = "statusStrip";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(1354, 20);
            statusLabel.Spring = true;
            statusLabel.Text = "Sẵn sàng";
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 250);
            ClientSize = new Size(1583, 1049);
            Controls.Add(pnlDashboard);
            Controls.Add(pnlHeader);
            Controls.Add(menuStrip);
            Controls.Add(statusStrip);
            MainMenuStrip = menuStrip;
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1080, 784);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WinForms Fashion Shop";
            Load += MainForm_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlDashboard.ResumeLayout(false);
            pnlDashboard.PerformLayout();
            pnlRecentOrders.ResumeLayout(false);
            pnlRecentOrders.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridRecentOrders).EndInit();
            pnlQuickActions.ResumeLayout(false);
            pnlQuickActions.PerformLayout();
            pnlActionsFlow.ResumeLayout(false);
            cardAction1.ResumeLayout(false);
            cardAction1.PerformLayout();
            cardAction2.ResumeLayout(false);
            cardAction2.PerformLayout();
            cardAction3.ResumeLayout(false);
            cardAction3.PerformLayout();
            cardAction4.ResumeLayout(false);
            cardAction4.PerformLayout();
            cardAction5.ResumeLayout(false);
            cardAction5.PerformLayout();
            cardAction6.ResumeLayout(false);
            cardAction6.PerformLayout();
            cardAction7.ResumeLayout(false);
            cardAction7.PerformLayout();
            pnlStatsCards.ResumeLayout(false);
            cardStat1.ResumeLayout(false);
            cardStat1.PerformLayout();
            cardStat2.ResumeLayout(false);
            cardStat2.PerformLayout();
            cardStat3.ResumeLayout(false);
            cardStat3.PerformLayout();
            cardStat4.ResumeLayout(false);
            cardStat4.PerformLayout();
            cardStat5.ResumeLayout(false);
            cardStat5.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private Panel pnlHeader;
        private PictureBox picLogo;
        private Label lblWelcome;
        private Label lblUserInfo;
        private Panel pnlDashboard;
        private FlowLayoutPanel pnlStatsCards;
        private Panel cardStat1;
        private Label lblStat1Icon;
        private Label lblStat1Title;
        private Label lblStat1Value;
        private Panel cardStat2;
        private Label lblStat2Icon;
        private Label lblStat2Title;
        private Label lblStat2Value;
        private Panel cardStat3;
        private Label lblStat3Icon;
        private Label lblStat3Title;
        private Label lblStat3Value;
        private Panel cardStat4;
        private Label lblStat4Icon;
        private Label lblStat4Title;
        private Label lblStat4Value;
        private Panel cardStat5;
        private Label lblStat5Icon;
        private Label lblStat5Title;
        private Label lblStat5Value;
        private Panel pnlQuickActions;
        private Label lblQuickActions;
        private FlowLayoutPanel pnlActionsFlow;
        private Panel cardAction1;
        private Label lblAction1Icon;
        private Label lblAction1Title;
        private Label lblAction1Desc;
        private Panel cardAction2;
        private Label lblAction2Icon;
        private Label lblAction2Title;
        private Label lblAction2Desc;
        private Panel cardAction3;
        private Label lblAction3Icon;
        private Label lblAction3Title;
        private Label lblAction3Desc;
        private Panel cardAction4;
        private Label lblAction4Icon;
        private Label lblAction4Title;
        private Label lblAction4Desc;
        private Panel cardAction5;
        private Label lblAction5Icon;
        private Label lblAction5Title;
        private Label lblAction5Desc;
        private Panel cardAction6;
        private Label lblAction6Icon;
        private Label lblAction6Title;
        private Label lblAction6Desc;
        private Panel cardAction7;
        private Label lblAction7Icon;
        private Label lblAction7Title;
        private Label lblAction7Desc;
        private Panel pnlRecentOrders;
        private Label lblRecentOrders;
        private DataGridView gridRecentOrders;
        private DataGridViewTextBoxColumn colOrderCode;
        private DataGridViewTextBoxColumn colOrderDate;
        private DataGridViewTextBoxColumn colCustomerName;
        private DataGridViewTextBoxColumn colTotalAmount;
        private DataGridViewTextBoxColumn colPaymentMethod;
        private DataGridViewTextBoxColumn colStatus;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
    }
}
