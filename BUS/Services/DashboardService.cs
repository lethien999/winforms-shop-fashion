using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Services
{
    /// <summary>
    /// Service for dashboard operations.
    /// Single responsibility: aggregates dashboard data from multiple services.
    /// </summary>
    public class DashboardService : IDashboardService
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IReportService _reportService;

        public DashboardService(
            IOrderService orderService,
            IProductService productService,
            ICustomerService customerService,
            IReportService reportService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
        }

        /// <summary>
        /// Gets dashboard statistics including revenue, orders, products, customers, and inventory alerts.
        /// Single responsibility: only aggregates statistics from various services.
        /// </summary>
        public DashboardStatistics GetStatistics()
        {
            var today = DateTime.Now.Date;
            var monthStart = new DateTime(today.Year, today.Month, 1);

            // Get orders
            var allOrders = _orderService.GetAllOrders().ToList();
            var todayOrders = allOrders.Where(o => o.OrderDate.Date == today).ToList();
            var monthOrders = allOrders.Where(o => o.OrderDate >= monthStart).ToList();

            // Calculate statistics
            var totalRevenue = monthOrders.Sum(o => o.TotalAmount);
            var todayRevenue = todayOrders.Sum(o => o.TotalAmount);
            var totalOrders = allOrders.Count;
            var todayOrdersCount = todayOrders.Count;

            // Get products and customers
            var totalProducts = _productService.GetAllProducts().Count(p => p.IsActive);
            var totalCustomers = _customerService.GetAllCustomers().Count();

            // Get inventory report
            var inventoryReport = _reportService.GetInventoryReport(10);
            var lowStockCount = inventoryReport.LowStockCount;

            return new DashboardStatistics
            {
                TotalRevenue = totalRevenue,
                TodayRevenue = todayRevenue,
                TotalOrders = totalOrders,
                TodayOrdersCount = todayOrdersCount,
                TotalProducts = totalProducts,
                TotalCustomers = totalCustomers,
                LowStockCount = lowStockCount
            };
        }

        /// <summary>
        /// Gets recent orders for dashboard display.
        /// Single responsibility: only retrieves recent orders.
        /// </summary>
        public IEnumerable<OrderDTO> GetRecentOrders(int count)
        {
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than zero");

            return _orderService.GetAllOrders()
                .OrderByDescending(o => o.OrderDate)
                .Take(count);
        }
    }
}
