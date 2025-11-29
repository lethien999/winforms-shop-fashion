using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsFashionShop.Business.Constants;
using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.Data.Repositories;

namespace WinFormsFashionShop.Business.Services
{
    public class ReportService : IReportService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public ReportService(
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IInventoryRepository inventoryRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
            _inventoryRepository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public RevenueReport GetRevenueReport(DateTime from, DateTime to)
        {
            if (from > to) throw new ArgumentException("From date must be earlier than To date");

            var orders = _orderRepository.GetByDateRange(from, to).ToList();
            
            // Populate items for each order
            var allOrderItems = _orderItemRepository.GetAll().ToList();
            foreach (var order in orders)
            {
                order.Items = allOrderItems.Where(item => item.OrderId == order.Id).ToList();
            }

            var report = new RevenueReport
            {
                FromDate = from,
                ToDate = to,
                TotalOrders = orders.Count,
                TotalRevenue = orders.Sum(o => o.TotalAmount),
                Orders = orders
            };

            return report;
        }

        public InventoryReport GetInventoryReport(int lowStockThreshold = InventoryConstants.DefaultLowStockThreshold)
        {
            var inventories = _inventoryRepository.GetAll().ToList();
            var products = _productRepository.GetAll().ToList();
            var items = new List<InventoryItem>();

            foreach (var inventory in inventories)
            {
                var product = products.FirstOrDefault(p => p.Id == inventory.ProductId);
                if (product != null)
                {
                    items.Add(new InventoryItem
                    {
                        ProductId = inventory.ProductId,
                        ProductName = product.Name,
                        ProductCode = product.ProductCode,
                        QuantityInStock = inventory.QuantityInStock,
                        IsLowStock = inventory.QuantityInStock < lowStockThreshold
                    });
                }
            }

            var report = new InventoryReport
            {
                TotalProducts = items.Count,
                LowStockCount = items.Count(i => i.IsLowStock),
                Items = items
            };

            return report;
        }

        public IEnumerable<Order> GetCustomerPurchaseHistory(int customerId)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

            var orders = _orderRepository.GetByCustomerId(customerId).ToList();
            
            // Populate items for each order
            var allOrderItems = _orderItemRepository.GetAll().ToList();
            foreach (var order in orders)
            {
                order.Items = allOrderItems.Where(item => item.OrderId == order.Id).ToList();
            }

            return orders;
        }

        public IEnumerable<CustomerSpending> GetTopCustomers(int topN = 10)
        {
            if (topN <= 0) throw new ArgumentOutOfRangeException(nameof(topN));

            var orders = _orderRepository.GetAll().ToList();
            var customers = _customerRepository.GetAll().ToList();

            var customerSpending = new Dictionary<int, CustomerSpending>();

            foreach (var order in orders)
            {
                if (!order.CustomerId.HasValue) continue;

                var customerId = order.CustomerId.Value;
                if (!customerSpending.ContainsKey(customerId))
                {
                    var customer = customers.FirstOrDefault(c => c.Id == customerId);
                    customerSpending[customerId] = new CustomerSpending
                    {
                        CustomerId = customerId,
                        CustomerName = customer?.CustomerName ?? "Unknown",
                        OrderCount = 0,
                        TotalSpent = 0
                    };
                }

                customerSpending[customerId].OrderCount++;
                customerSpending[customerId].TotalSpent += order.TotalAmount;
            }

            return customerSpending.Values
                .OrderByDescending(c => c.TotalSpent)
                .Take(topN);
        }
    }
}

