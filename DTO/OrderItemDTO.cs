namespace WinFormsFashionShop.DTO
{
    /// <summary>
    /// Data Transfer Object for OrderItem
    /// </summary>
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; } // For display
        public string? ProductCode { get; set; } // For display
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    /// <summary>
    /// DTO for creating a new order item
    /// </summary>
    public class CreateOrderItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

