namespace WinFormsFashionShop.DTO
{
    /// <summary>
    /// Data Transfer Object for Order
    /// </summary>
    public class OrderDTO
    {
        public int Id { get; set; }
        public string OrderCode { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; } // For display
        public int UserId { get; set; }
        public string? UserName { get; set; } // For display
        public decimal TotalAmount { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = "Paid";
        public List<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
    }

    /// <summary>
    /// DTO for creating a new order
    /// </summary>
    public class CreateOrderDTO
    {
        public int? CustomerId { get; set; }
        public int UserId { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = "Paid";
        public List<CreateOrderItemDTO> Items { get; set; } = new List<CreateOrderItemDTO>();
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
    }

    /// <summary>
    /// DTO for updating an existing order
    /// </summary>
    public class UpdateOrderDTO
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = "Paid";
    }
}

