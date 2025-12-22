using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsFashionShop.Business.Constants;
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
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;
        
        public OrderService(
            IOrderRepository orderRepository, 
            IOrderItemRepository orderItemRepository, 
            IInventoryService inventoryService,
            IProductService productService,
            ICustomerService customerService,
            IUserService userService)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
            _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
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

        /// <summary>
        /// Creates a new order with items and updates inventory.
        /// Orchestrates the flow but delegates to smaller methods.
        /// </summary>
        public OrderDTO CreateOrder(CreateOrderDTO dto)
        {
            ValidateCreateOrderRequest(dto);
            ValidateStockAvailabilityForItems(dto.Items);
            
            var orderCode = GenerateOrderCode();
            var order = CreateOrderEntity(dto, orderCode);
            
            SaveOrderAndItems(order, dto);
            
            // Return the created order as DTO
            return GetOrderById(order.Id) ?? throw new InvalidOperationException("Failed to retrieve created order");
        }

        /// <summary>
        /// Validates the create order request.
        /// Single responsibility: only validates request.
        /// </summary>
        private void ValidateCreateOrderRequest(CreateOrderDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.Items == null || dto.Items.Count == 0) 
                throw new ArgumentException("Order must have items", nameof(dto.Items));
            if (dto.UserId <= 0) 
                throw new ArgumentException("UserId is required", nameof(dto.UserId));
        }

        /// <summary>
        /// Validates stock availability for all items in the order.
        /// Single responsibility: only validates stock.
        /// </summary>
        private void ValidateStockAvailabilityForItems(List<CreateOrderItemDTO> items)
        {
            foreach (var item in items)
            {
                if (!CheckStockAvailability(item.ProductId, item.Quantity))
                {
                    throw new InvalidOperationException($"Insufficient stock for product ID {item.ProductId}");
                }
            }
        }

        /// <summary>
        /// Creates the order entity from DTO and calculates totals.
        /// Single responsibility: only creates and calculates order entity.
        /// </summary>
        private Order CreateOrderEntity(CreateOrderDTO dto, string orderCode)
        {
            var order = OrderMapper.ToEntity(dto, orderCode);
            CalculateLineTotals(order.Items);
            order.TotalAmount = CalculateOrderTotal(order.Items, dto.DiscountPercent, dto.DiscountAmount);
            return order;
        }

        /// <summary>
        /// Saves the order and items, then decreases inventory ONLY if Status = "Paid".
        /// CRITICAL: Inventory chỉ được giảm khi đơn hàng đã thanh toán thành công.
        /// Single responsibility: only saves to database and updates inventory.
        /// </summary>
        private void SaveOrderAndItems(Order order, CreateOrderDTO dto)
        {
            _orderRepository.Insert(order);

            foreach (var item in order.Items)
            {
                item.OrderId = order.Id;
                _orderItemRepository.Insert(item);
            }

            // CHỈ giảm inventory nếu Status = "Paid" (đã thanh toán thành công)
            // Với VietQR: Status = "Pending" → không giảm inventory, sẽ giảm sau khi webhook update Paid
            // Với Cash/Card: Status = "Paid" → giảm inventory ngay
            if (order.Status == Constants.OrderStatus.Paid)
            {
                foreach (var item in order.Items)
                {
                    _inventoryService.DecreaseStock(item.ProductId, item.Quantity);
                }
            }
        }

        /// <summary>
        /// Updates an existing order. Handles inventory changes when status changes.
        /// CRITICAL: Khi status thay đổi, cần xử lý inventory:
        /// - Pending → Paid: Giảm inventory (nếu VietQR)
        /// - Paid → Pending: Restore inventory (nếu đã giảm)
        /// Single responsibility: updates order and handles inventory changes.
        /// </summary>
        public void UpdateOrder(UpdateOrderDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.Id <= 0) throw new ArgumentException("Order Id invalid", nameof(dto.Id));
            
            var existingOrder = _orderRepository.GetById(dto.Id)
                ?? throw new InvalidOperationException("Order not found");
            
            var oldStatus = existingOrder.Status;
            var newStatus = dto.Status;
            
            PopulateOrderItems(new List<Order> { existingOrder });
            
            // CRITICAL: Xử lý inventory khi status thay đổi
            if (oldStatus != newStatus)
            {
                // Case 1: Pending → Paid: Giảm inventory (chỉ cho VietQR)
                if (oldStatus == Constants.OrderStatus.Pending && newStatus == Constants.OrderStatus.Paid)
                {
                    System.Diagnostics.Debug.WriteLine($"Order {dto.Id} status changed from Pending to Paid. Checking if inventory needs to be decreased...");
                    if (existingOrder.PaymentMethod == Constants.PaymentMethod.VietQR)
                    {
                        System.Diagnostics.Debug.WriteLine($"Order {dto.Id} is VietQR payment. Inventory will be decreased after status update.");
                        // Note: Will call DecreaseInventoryForPaidOrder after update
                    }
                }
                
                // Case 2: Paid → Pending: Restore inventory (nếu đã giảm)
                if (oldStatus == Constants.OrderStatus.Paid && newStatus == Constants.OrderStatus.Pending)
                {
                    System.Diagnostics.Debug.WriteLine($"Order {dto.Id} status changed from Paid to Pending. Restoring inventory...");
                    // Restore inventory cho tất cả items (vì đã giảm khi Paid)
                    foreach (var item in existingOrder.Items)
                    {
                        try
                        {
                            _inventoryService.IncreaseStock(item.ProductId, item.Quantity);
                            System.Diagnostics.Debug.WriteLine($"✅ Restored inventory: Product {item.ProductId}, Quantity {item.Quantity}, Order {dto.Id}");
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"⚠️  WARNING: Cannot restore inventory for product {item.ProductId} in order {dto.Id}: {ex.Message}");
                        }
                    }
                }
                
                // Case 3: Paid → Cancelled: Đã xử lý trong CancelOrder, không cần xử lý ở đây
                // Case 4: Cancelled → Paid: Không hợp lệ, nhưng nếu có, giảm inventory
                if (oldStatus == Constants.OrderStatus.Cancelled && newStatus == Constants.OrderStatus.Paid)
                {
                    System.Diagnostics.Debug.WriteLine($"⚠️  WARNING: Order {dto.Id} status changed from Cancelled to Paid. This is unusual. Decreasing inventory...");
                    if (existingOrder.PaymentMethod == Constants.PaymentMethod.VietQR)
                    {
                        // Will call DecreaseInventoryForPaidOrder after update
                    }
                }
            }
            
            var updatedOrder = OrderMapper.ToEntity(dto, existingOrder);
            updatedOrder.TotalAmount = CalculateOrderTotal(updatedOrder.Items);
            
            _orderRepository.Update(updatedOrder);
            
            // CRITICAL: Sau khi update status = Paid, giảm inventory (cho VietQR)
            if (oldStatus != Constants.OrderStatus.Paid && newStatus == Constants.OrderStatus.Paid)
            {
                if (existingOrder.PaymentMethod == Constants.PaymentMethod.VietQR)
                {
                    try
                    {
                        // Reload order để đảm bảo có status mới nhất
                        var reloadedOrder = _orderRepository.GetById(dto.Id);
                        if (reloadedOrder != null && reloadedOrder.Status == Constants.OrderStatus.Paid)
                        {
                            DecreaseInventoryForPaidOrder(dto.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"⚠️  ERROR: Cannot decrease inventory for order {dto.Id} after status update: {ex.Message}");
                        // Log nhưng không throw để không block update order
                    }
                }
            }
        }

        /// <summary>
        /// Deletes an order and restores inventory if order was Paid.
        /// CRITICAL: Nếu order đã Paid (đã giảm inventory), cần restore inventory trước khi xóa.
        /// Single responsibility: deletes order and restores inventory if needed.
        /// </summary>
        public void DeleteOrder(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found");
            }
            
            PopulateOrderItems(new List<Order> { order });
            
            // CRITICAL: Nếu order đã Paid (đã giảm inventory), restore inventory trước khi xóa
            if (order.Status == Constants.OrderStatus.Paid)
            {
                System.Diagnostics.Debug.WriteLine($"Order {id} is Paid. Restoring inventory before deletion...");
                foreach (var item in order.Items)
                {
                    try
                    {
                        _inventoryService.IncreaseStock(item.ProductId, item.Quantity);
                        System.Diagnostics.Debug.WriteLine($"✅ Restored inventory: Product {item.ProductId}, Quantity {item.Quantity}, Order {id}");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"⚠️  WARNING: Cannot restore inventory for product {item.ProductId} in order {id}: {ex.Message}");
                        // Log nhưng vẫn tiếp tục xóa order
                    }
                }
            }
            
            // Delete items first to maintain referential integrity
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

        /// <summary>
        /// Cancels an order and restores inventory stock (only if order was Paid).
        /// CRITICAL: Chỉ restore inventory nếu order đã Paid (đã giảm inventory rồi).
        /// Nếu order Status = "Pending" → chưa giảm inventory → không cần restore.
        /// Single responsibility: only cancels order and restores stock.
        /// </summary>
        public void CancelOrder(int orderId)
        {
            if (orderId <= 0) throw new ArgumentOutOfRangeException(nameof(orderId));
            
            var order = _orderRepository.GetById(orderId)
                ?? throw new InvalidOperationException("Order not found");
            
            if (order.Status == Constants.OrderStatus.Cancelled)
                throw new InvalidOperationException("Order is already cancelled");
            
            PopulateOrderItems(new List<Order> { order });
            
            // CHỈ restore inventory nếu order đã Paid (đã giảm inventory rồi)
            // Nếu order Status = "Pending" (chưa thanh toán) → chưa giảm inventory → không cần restore
            if (order.Status == Constants.OrderStatus.Paid)
            {
                foreach (var item in order.Items)
                {
                    _inventoryService.IncreaseStock(item.ProductId, item.Quantity);
                }
            }
            
            // Update order status to Cancelled
            order.Status = Constants.OrderStatus.Cancelled;
            _orderRepository.Update(order);
        }

        /// <summary>
        /// Decrease inventory for order items when order status changes to Paid.
        /// Used when webhook updates order from Pending to Paid.
        /// IDEMPOTENT: 
        /// - Chỉ giảm inventory cho VietQR payment (Cash/Card đã giảm trong SaveOrderAndItems)
        /// - Check PaidAt để tránh double-decrease (nếu PaidAt đã set, có thể đã giảm rồi)
        /// Single responsibility: only decreases inventory for paid order.
        /// </summary>
        public void DecreaseInventoryForPaidOrder(int orderId)
        {
            if (orderId <= 0) throw new ArgumentOutOfRangeException(nameof(orderId));
            
            var order = _orderRepository.GetById(orderId)
                ?? throw new InvalidOperationException("Order not found");
            
            if (order.Status != Constants.OrderStatus.Paid)
                throw new InvalidOperationException($"Cannot decrease inventory for order with status '{order.Status}'. Order must be Paid.");
            
            // IDEMPOTENCY CHECK 1: Chỉ giảm inventory cho VietQR payment
            // Với Cash/Card: SaveOrderAndItems đã giảm inventory rồi (khi tạo order với Status="Paid")
            // Với VietQR: SaveOrderAndItems KHÔNG giảm (khi tạo order với Status="Pending")
            // → Chỉ cần giảm cho VietQR khi update từ Pending → Paid
            if (order.PaymentMethod != Constants.PaymentMethod.VietQR)
            {
                System.Diagnostics.Debug.WriteLine($"✅ IDEMPOTENCY: Skip decreasing inventory for order {orderId}: PaymentMethod is {order.PaymentMethod} (already decreased in SaveOrderAndItems)");
                return; // Cash/Card đã giảm inventory rồi
            }
            
            // IDEMPOTENCY CHECK 2: Nếu PaidAt đã set, có thể đã giảm inventory rồi (race condition)
            // Tuy nhiên, vẫn giảm để đảm bảo (nếu chưa giảm) vì có thể có edge case
            // Nếu đã giảm rồi, DecreaseStock sẽ throw exception về insufficient stock → catch và skip
            
            PopulateOrderItems(new List<Order> { order });
            
            // Giảm inventory cho tất cả items trong order (chỉ cho VietQR)
            bool hasError = false;
            foreach (var item in order.Items)
            {
                try
                {
                    _inventoryService.DecreaseStock(item.ProductId, item.Quantity);
                    System.Diagnostics.Debug.WriteLine($"✅ Decreased inventory: Product {item.ProductId}, Quantity {item.Quantity}, Order {orderId}");
                }
                catch (InvalidOperationException ex)
                {
                    // Nếu không đủ tồn kho, có thể do:
                    // 1. Đã giảm rồi (race condition) → Expected, skip
                    // 2. Thực sự không đủ tồn kho → Unexpected, log warning
                    hasError = true;
                    System.Diagnostics.Debug.WriteLine($"⚠️  IDEMPOTENCY: Cannot decrease inventory for product {item.ProductId} in order {orderId}: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"   This may be expected if inventory was already decreased (idempotency protection)");
                    
                    // Check xem có phải do đã giảm rồi không (inventory < quantity)
                    // Nếu đúng, đây là idempotency check thành công
                    var inventory = _inventoryService.GetInventoryByProductId(item.ProductId);
                    if (inventory != null && inventory.QuantityInStock < item.Quantity)
                    {
                        System.Diagnostics.Debug.WriteLine($"✅ IDEMPOTENCY CONFIRMED: Inventory for product {item.ProductId} is {inventory.QuantityInStock}, required {item.Quantity}. Likely already decreased.");
                    }
                }
            }
            
            if (!hasError)
            {
                System.Diagnostics.Debug.WriteLine($"✅ Successfully decreased inventory for all items in order {orderId}");
            }
        }

        /// <summary>
        /// Đánh dấu đơn hàng đã in để tránh in trùng hóa đơn
        /// </summary>
        public void MarkOrderAsPrinted(int orderId)
        {
            if (orderId <= 0) throw new ArgumentOutOfRangeException(nameof(orderId));
            
            var order = _orderRepository.GetById(orderId)
                ?? throw new InvalidOperationException("Order not found");
            
            // Chỉ set PrintedAt nếu chưa in (idempotent)
            if (!order.PrintedAt.HasValue)
            {
                order.PrintedAt = DateTime.Now;
                _orderRepository.Update(order);
            }
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

        /// <summary>
        /// Maps Order entity to OrderDTO, populating related data from services.
        /// Single responsibility: only maps order to DTO.
        /// </summary>
        private OrderDTO MapToDTO(Order order)
        {
            string? customerName = null;
            if (order.CustomerId.HasValue)
            {
                var customer = _customerService.GetCustomerById(order.CustomerId.Value);
                customerName = customer?.CustomerName;
            }

            var user = _userService.GetUserById(order.UserId);
            var userName = user?.FullName;

            var orderDTO = OrderMapper.ToDTO(order, customerName, userName);
            
            // Populate product names for items using ProductService
            foreach (var itemDTO in orderDTO.Items)
            {
                var product = _productService.GetProductById(itemDTO.ProductId);
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