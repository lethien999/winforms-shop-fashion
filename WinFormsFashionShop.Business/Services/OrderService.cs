using System;
using System.Collections.Generic;
using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.Data.Repositories;

namespace WinFormsFashionShop.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IInventoryService _inventoryService; // injected to adjust stock
        
        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IInventoryService inventoryService)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _inventoryService = inventoryService;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        public Order? GetOrderById(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            var order = _orderRepository.GetById(id);
            if (order == null) return null;
            // populate items
            foreach (var item in _orderItemRepository.GetAll())
            {
                if (item.OrderId == order.Id) order.Items.Add(item);
            }
            return order;
        }

        public void CreateOrder(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            if (order.CustomerId <= 0) throw new ArgumentException("CustomerId required", nameof(order.CustomerId));
            if (order.Items == null || order.Items.Count == 0) throw new ArgumentException("Order must have items", nameof(order.Items));

            // calculate total
            order.Total = CalculateOrderTotal(order);
            _orderRepository.Insert(order);

            // insert items, set OrderId, and decrease inventory
            foreach (var item in order.Items)
            {
                item.OrderId = order.Id;
                _orderItemRepository.Insert(item);
                _inventoryService.DecreaseInventoryForOrder(item.ProductId, item.Quantity);
            }
        }

        public void UpdateOrder(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            if (order.Id <= 0) throw new ArgumentException("Order Id invalid", nameof(order.Id));
            order.Total = CalculateOrderTotal(order);
            _orderRepository.Update(order);
        }

        public void DeleteOrder(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            // delete items first to maintain referential integrity
            foreach (var item in _orderItemRepository.GetAll())
            {
                if (item.OrderId == id) _orderItemRepository.Delete(item.Id);
            }
            _orderRepository.Delete(id);
        }

        private decimal CalculateOrderTotal(Order order)
        {
            decimal total = 0m;
            foreach (var item in order.Items)
            {
                if (item.Quantity <= 0) throw new ArgumentException("Item quantity must be greater than zero");
                if (item.UnitPrice < 0) throw new ArgumentException("Item unit price cannot be negative");
                total += item.UnitPrice * item.Quantity;
            }
            return total;
        }

        public IEnumerable<Order> GetOrdersByDateRange(DateTime from, DateTime to)
        {
            if (from > to) throw new ArgumentException("From date must be earlier than To date");
            var results = new List<Order>();
            foreach (var order in _orderRepository.GetAll())
            {
                if (order.OrderDate.Date >= from.Date && order.OrderDate.Date <= to.Date)
                {
                    // populate items for each matched order
                    foreach (var item in _orderItemRepository.GetAll())
                    {
                        if (item.OrderId == order.Id) order.Items.Add(item);
                    }
                    results.Add(order);
                }
            }
            return results;
        }
    }
}