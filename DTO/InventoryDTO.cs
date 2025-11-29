namespace WinFormsFashionShop.DTO
{
    /// <summary>
    /// Data Transfer Object for Inventory
    /// </summary>
    public class InventoryDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; } // For display
        public string? ProductCode { get; set; } // For display
        public int QuantityInStock { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    /// <summary>
    /// DTO for adjusting inventory
    /// </summary>
    public class AdjustInventoryDTO
    {
        public int ProductId { get; set; }
        public int QuantityChange { get; set; } // Positive for increase, negative for decrease
        public string? Reason { get; set; }
    }
}

