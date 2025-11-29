using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsFashionShop.Business.Mappers;
using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.Data.Repositories;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IInventoryService _inventoryService;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        
        public OrderService(
            IOrderRepository orderRepository, 
            IOrderItemRepository orderItemRepository, 
            IInventoryService inventoryService,
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _inventoryService = inventoryService;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<OrderDTO> GetAllOrders()
        {
            var orders = _orderRepository.GetAll().ToList();
            PopulateOrderItems(orders);
            return orders.Select(o => MapToDTO(o));
        }

        public OrderDTO? GetOrderById(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            var order = _orderRepository.GetById(id);
            if (order == null) return null;
            
            PopulateOrderItems(new List<Order> { order });
            return MapToDTO(order);
        }

        public void CreateOrder(CreateOrderDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.Items == null || dto.Items.Count == 0) 
                throw new ArgumentException("Order must have items", nameof(dto.Items));
            if (dto.UserId <= 0) 
                throw new ArgumentException("UserId is required", nameof(dto.UserId));

            // Check stock availability for all items before creating order
            foreach (var item in dto.Items)
            {
                if (!CheckStockAvailability(item.ProductId, item.Quantity))
                {
                    throw new InvalidOperationException($"Insufficient stock for product ID {item.ProductId}");
                }
            }

            // Generate order code
            var orderCode = GenerateOrderCode();

            // Convert DTO to Entity
            var order = OrderMapper.ToEntity(dto, orderCode);
            
            // Calculate line totals and order total
            CalculateLineTotals(order.Items);
            order.TotalAmount = CalculateOrderTotal(order.Items, dto.DiscountPercent, dto.DiscountAmount);

            // Insert order
            _orderRepository.Insert(order);

            // Insert items and decrease inventory
            foreach (var item in order.Items)
            {
                item.OrderId = order.Id;
                _orderItemRepository.Insert(item);
                _inventoryService.DecreaseStock(item.ProductId, item.Quantity);
            }
        }

        public void UpdateOrder(UpdateOrderDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.Id <= 0) throw new ArgumentException("Order Id invalid", nameof(dto.Id));
            
            var existingOrder = _orderRepository.GetById(dto.Id)
                ?? throw new InvalidOperationException("Order not found");
            
            PopulateOrderItems(new List<Order> { existingOrder });
            
            var updatedOrder = OrderMapper.ToEntity(dto, existingOrder);
            updatedOrder.TotalAmount = CalculateOrderTotal(updatedOrder.Items);
            
            _orderRepository.Update(updatedOrder);
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

        private decimal CalculateOrderTotal(List<OrderItem> items, decimal? discountPercent = null, decimal? discountAmount = null)
        {
            decimal total = 0m;
            foreach (var item in items)
            {
                if (item.Quantity <= 0) throw new ArgumentException("Item quantity must be greater than zero");
                if (item.UnitPrice < 0) throw new ArgumentException("Item unit price cannot be negative");
                total += item.LineTotal;
            }

            // Apply discount
            if (discountPercent.HasValue && discountPercent.Value > 0)
            {
                total = total * (1 - discountPercent.Value / 100);
            }
            else if (discountAmount.HasValue && discountAmount.Value > 0)
            {
                total = Math.Max(0, total - discountAmount.Value);
            }

            return total;
        }

        public IEnumerable<OrderDTO> GetOrdersByDateRange(DateTime from, DateTime to)
        {
            if (from > to) throw new ArgumentException("From date must be earlier than To date");
            var orders = _orderRepository.GetByDateRange(from, to).ToList();
            PopulateOrderItems(orders);
            return orders.Select(MapToDTO);
        }

        public IEnumerable<OrderDTO> GetOrdersByCustomerId(int customerId)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));
            var orders = _orderRepository.GetByCustomerId(customerId).ToList();
            PopulateOrderItems(orders);
            return orders.Select(MapToDTO);
        }

        public IEnumerable<OrderDTO> GetOrdersByUserId(int userId)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
            var orders = _orderRepository.GetByUserId(userId).ToList();
            PopulateOrderItems(orders);
            return orders.Select(MapToDTO);
        }

        public bool CheckStockAvailability(int productId, int quantity)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            
            return _inventoryService.CheckStockAvailability(productId, quantity);
        }

        public string GenerateOrderCode()
        {
            // Generate order code: ORD + YYYYMMDD + sequential number
            var datePrefix = DateTime.Now.ToString("yyyyMMdd");
            var existingOrders = _orderRepository.GetByDateRange(DateTime.Now.Date, DateTime.Now.Date.AddDays(1)).ToList();
            var sequence = existingOrders.Count + 1;
            return $"ORD{datePrefix}{sequence:D4}";
        }

        private void PopulateOrderItems(List<Order> orders)
        {
            var allOrderItems = _orderItemRepository.GetAll().ToList();
            foreach (var order in orders)
            {
                order.Items = allOrderItems.Where(item => item.OrderId == order.Id).ToList();
            }
        }

        private void CalculateLineTotals(List<OrderItem> items)
        {
            foreach (var item in items)
            {
                item.LineTotal = item.Quantity * item.UnitPrice;
            }
        }

        private OrderDTO MapToDTO(Order order)
        {
            string? customerName = null;
            if (order.CustomerId.HasValue)
            {
                var customer = _customerRepository.GetById(order.CustomerId.Value);
                customerName = customer?.CustomerName;
            }

            var user = _userRepository.GetById(order.UserId);
            var userName = user?.FullName;

            var orderDTO = OrderMapper.ToDTO(order, customerName, userName);
            
            // Populate product names for items
            foreach (var itemDTO in orderDTO.Items)
            {
                var product = _productRepository.GetById(itemDTO.ProductId);
                if (product != null)
                {
                    itemDTO.ProductName = product.Name;
                    itemDTO.ProductCode = product.ProductCode;
                }
            }

            return orderDTO;
        }
    }
}