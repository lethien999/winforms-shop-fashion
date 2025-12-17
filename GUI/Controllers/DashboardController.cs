using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Presentation.Helpers;
using UIThemeConstants = WinFormsFashionShop.Presentation.Helpers.UIThemeConstants;

namespace WinFormsFashionShop.Presentation.Controllers
{
    /// <summary>
    /// Controller for dashboard operations.
    /// Single responsibility: orchestrates dashboard data loading and UI updates.
    /// </summary>
    public class DashboardController
    {
        private readonly IDashboardService _dashboardService;
        private readonly IDashboardCardFactory _cardFactory;
        private readonly IErrorHandler _errorHandler;
        private readonly UserDTO _currentUser;

        public DashboardController(
            IDashboardService dashboardService,
            IDashboardCardFactory cardFactory,
            IErrorHandler errorHandler,
            UserDTO currentUser)
        {
            _dashboardService = dashboardService ?? throw new ArgumentNullException(nameof(dashboardService));
            _cardFactory = cardFactory ?? throw new ArgumentNullException(nameof(cardFactory));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        /// <summary>
        /// Gets formatted user info text.
        /// Single responsibility: only formats user info.
        /// </summary>
        public string GetUserInfoText()
        {
            return $"👤 {_currentUser.FullName} | 🎭 {_currentUser.Role}";
        }

        /// <summary>
        /// Gets dashboard statistics.
        /// Single responsibility: only retrieves statistics.
        /// </summary>
        public DashboardStatistics GetStatistics()
        {
            try
            {
                return _dashboardService.GetStatistics();
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Không thể tải thống kê: {ex.Message}");
                return new DashboardStatistics();
            }
        }

        /// <summary>
        /// Loads and creates statistics cards.
        /// Single responsibility: only loads statistics and creates cards.
        /// </summary>
        public List<Panel> LoadStatisticsCards()
        {
            try
            {
                var stats = _dashboardService.GetStatistics();
                var cards = new List<Panel>();

                var card1 = _cardFactory.CreateStatCard(
                    "Doanh thu tháng này",
                    $"{stats.TotalRevenue:N0} VNĐ",
                    "💰",
                    UIThemeConstants.Colors.CardRevenue,
                    180, 150
                );
                card1.Margin = new Padding(0, 0, 15, 0);
                cards.Add(card1);

                var card2 = _cardFactory.CreateStatCard(
                    "Đơn hàng hôm nay",
                    $"{stats.TodayOrdersCount} đơn",
                    "📦",
                    UIThemeConstants.Colors.CardOrders,
                    180, 150
                );
                card2.Margin = new Padding(0, 0, 15, 0);
                cards.Add(card2);

                var card3 = _cardFactory.CreateStatCard(
                    "Tổng sản phẩm",
                    $"{stats.TotalProducts} sản phẩm",
                    "🛍️",
                    UIThemeConstants.Colors.CardProducts,
                    180, 150
                );
                card3.Margin = new Padding(0, 0, 15, 0);
                cards.Add(card3);

                var card4 = _cardFactory.CreateStatCard(
                    "Tổng khách hàng",
                    $"{stats.TotalCustomers} khách",
                    "👥",
                    UIThemeConstants.Colors.CardCustomers,
                    180, 150
                );
                card4.Margin = new Padding(0, 0, 15, 0);
                cards.Add(card4);

                var card5 = _cardFactory.CreateStatCard(
                    "Cảnh báo tồn kho",
                    $"{stats.LowStockCount} sản phẩm",
                    "⚠️",
                    UIThemeConstants.Colors.CardLowStock,
                    180, 150
                );
                card5.Margin = new Padding(0, 0, 0, 0);
                cards.Add(card5);

                return cards;
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Không thể tải thống kê: {ex.Message}");
                return new List<Panel>();
            }
        }

        /// <summary>
        /// Creates quick action cards based on user role.
        /// Single responsibility: only creates action cards.
        /// </summary>
        public List<Panel> CreateQuickActionCards(
            Action openCreateOrder,
            Action openProductManagement,
            Action openOrderManagement,
            Action openCustomerManagement,
            Action openInventoryAdjustment,
            Action openReports,
            Action? openUserManagement = null)
        {
            var actions = new List<Panel>();

            var action1 = _cardFactory.CreateActionCard(
                "Lập hóa đơn",
                "Tạo đơn hàng mới",
                "📝",
                (s, e) => openCreateOrder(),
                UIThemeConstants.Colors.BackgroundWhite
            );
            action1.Margin = new Padding(0, 0, 15, 15);
            actions.Add(action1);

            var action2 = _cardFactory.CreateActionCard(
                "Quản lý sản phẩm",
                "Xem, thêm, sửa sản phẩm",
                "🛍️",
                (s, e) => openProductManagement(),
                UIThemeConstants.Colors.BackgroundWhite
            );
            action2.Margin = new Padding(0, 0, 15, 15);
            actions.Add(action2);

            var action3 = _cardFactory.CreateActionCard(
                "Quản lý đơn hàng",
                "Xem danh sách đơn hàng",
                "📋",
                (s, e) => openOrderManagement(),
                UIThemeConstants.Colors.BackgroundWhite
            );
            action3.Margin = new Padding(0, 0, 15, 15);
            actions.Add(action3);

            var action4 = _cardFactory.CreateActionCard(
                "Quản lý khách hàng",
                "Xem thông tin khách hàng",
                "👥",
                (s, e) => openCustomerManagement(),
                UIThemeConstants.Colors.BackgroundWhite
            );
            action4.Margin = new Padding(0, 0, 15, 15);
            actions.Add(action4);

            var action5 = _cardFactory.CreateActionCard(
                "Nhập hàng",
                "Cập nhật tồn kho",
                "📦",
                (s, e) => openInventoryAdjustment(),
                UIThemeConstants.Colors.BackgroundWhite
            );
            action5.Margin = new Padding(0, 0, 15, 15);
            actions.Add(action5);

            var action6 = _cardFactory.CreateActionCard(
                "Báo cáo",
                "Xem báo cáo doanh thu",
                "📊",
                (s, e) => openReports(),
                UIThemeConstants.Colors.BackgroundWhite
            );
            action6.Margin = new Padding(0, 0, 0, 15);
            actions.Add(action6);

            // Add admin-only action if user is admin
            if (_currentUser.Role == UserRole.Admin && openUserManagement != null)
            {
                var action7 = _cardFactory.CreateActionCard(
                    "Quản lý người dùng",
                    "Quản trị hệ thống",
                    "👤",
                    (s, e) => openUserManagement(),
                    UIThemeConstants.Colors.BackgroundWhite
                );
                action7.Margin = new Padding(0, 0, 15, 15);
                actions.Add(action7);
            }

            return actions;
        }

        /// <summary>
        /// Loads recent orders for display.
        /// Single responsibility: only loads recent orders data.
        /// </summary>
        public List<OrderDTO> LoadRecentOrders(int count = 10)
        {
            try
            {
                return _dashboardService.GetRecentOrders(count).ToList();
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Không thể tải đơn hàng gần đây: {ex.Message}");
                return new List<OrderDTO>();
            }
        }

        /// <summary>
        /// Configures grid columns for recent orders display.
        /// Single responsibility: only configures grid columns.
        /// </summary>
        public void ConfigureRecentOrdersGrid(DataGridView grid)
        {
            if (grid.Columns.Count == 0)
            {
                grid.Columns.Add("OrderCode", "Mã đơn");
                grid.Columns["OrderCode"].Width = 150;

                grid.Columns.Add("OrderDate", "Ngày đặt");
                grid.Columns["OrderDate"].Width = 150;
                grid.Columns["OrderDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                grid.Columns.Add("CustomerName", "Khách hàng");
                grid.Columns["CustomerName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                grid.Columns.Add("TotalAmount", "Tổng tiền");
                grid.Columns["TotalAmount"].Width = 150;
                grid.Columns["TotalAmount"].DefaultCellStyle.Format = "N0";
                grid.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                grid.Columns.Add("PaymentMethod", "Phương thức TT");
                grid.Columns["PaymentMethod"].Width = 150;

                grid.Columns.Add("Status", "Trạng thái");
                grid.Columns["Status"].Width = 120;
            }
            else
            {
                // Configure existing columns
                if (grid.Columns["OrderDate"] != null)
                {
                    grid.Columns["OrderDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }
                if (grid.Columns["TotalAmount"] != null)
                {
                    grid.Columns["TotalAmount"].DefaultCellStyle.Format = "N0";
                    grid.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (grid.Columns["CustomerName"] != null)
                {
                    grid.Columns["CustomerName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }

        /// <summary>
        /// Populates grid with recent orders data.
        /// Single responsibility: only populates grid with data.
        /// </summary>
        public void PopulateRecentOrdersGrid(DataGridView grid, List<OrderDTO> orders)
        {
            grid.Rows.Clear();
            foreach (var order in orders)
            {
                var rowIndex = grid.Rows.Add(
                    order.OrderCode,
                    order.OrderDate,
                    order.CustomerName ?? "Khách lẻ",
                    order.TotalAmount,
                    order.PaymentMethod ?? "",
                    order.Status ?? "Hoàn thành"
                );
                // Store OrderDTO in row Tag for easy access
                if (rowIndex >= 0 && rowIndex < grid.Rows.Count)
                {
                    grid.Rows[rowIndex].Tag = order;
                }
            }
        }

        /// <summary>
        /// Gets formatted recent orders label text.
        /// Single responsibility: only formats label text.
        /// </summary>
        public string GetRecentOrdersLabelText(int orderCount)
        {
            return $"📋 Đơn hàng gần đây ({orderCount} đơn)";
        }
    }
}
