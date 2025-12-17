using System.Collections.Generic;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Services
{
    /// <summary>
    /// Service for dashboard operations.
    /// Single responsibility: aggregates dashboard data from multiple services.
    /// </summary>
    public interface IDashboardService
    {
        /// <summary>
        /// Gets dashboard statistics including revenue, orders, products, customers, and inventory alerts.
        /// </summary>
        DashboardStatistics GetStatistics();

        /// <summary>
        /// Gets recent orders for dashboard display.
        /// </summary>
        /// <param name="count">Number of recent orders to retrieve</param>
        IEnumerable<OrderDTO> GetRecentOrders(int count);
    }

    /// <summary>
    /// DTO for dashboard statistics.
    /// </summary>
    public class DashboardStatistics
    {
        public decimal TotalRevenue { get; set; }
        public decimal TodayRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int TodayOrdersCount { get; set; }
        public int TotalProducts { get; set; }
        public int TotalCustomers { get; set; }
        public int LowStockCount { get; set; }
    }
}
