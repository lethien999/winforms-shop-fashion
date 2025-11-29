using System;
using System.Collections.Generic;
using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Business.Services
{
    public interface IReportService
    {
        /// <summary>
        /// Gets revenue report for a date range
        /// </summary>
        RevenueReport GetRevenueReport(DateTime from, DateTime to);

        /// <summary>
        /// Gets inventory report with low stock items
        /// </summary>
        InventoryReport GetInventoryReport(int lowStockThreshold = 10);

        /// <summary>
        /// Gets customer purchase history
        /// </summary>
        IEnumerable<Order> GetCustomerPurchaseHistory(int customerId);

        /// <summary>
        /// Gets top customers by total spending
        /// </summary>
        IEnumerable<CustomerSpending> GetTopCustomers(int topN = 10);
    }

    public class RevenueReport
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }

    public class InventoryReport
    {
        public int TotalProducts { get; set; }
        public int LowStockCount { get; set; }
        public IEnumerable<InventoryItem> Items { get; set; } = new List<InventoryItem>();
    }

    public class InventoryItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public int QuantityInStock { get; set; }
        public bool IsLowStock { get; set; }
    }

    public class CustomerSpending
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int OrderCount { get; set; }
        public decimal TotalSpent { get; set; }
    }
}

