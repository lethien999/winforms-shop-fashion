using System;

namespace WinFormsFashionShop.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductCode { get; set; } = string.Empty; // SKU/ProductCode
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; } // Price -> UnitPrice
        public string Unit { get; set; } = "cái"; // đơn vị tính
        public string? ImagePath { get; set; } // Đường dẫn ảnh sản phẩm
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        
        // Backward compatibility
        [Obsolete("Use ProductCode instead")]
        public string Sku 
        { 
            get => ProductCode; 
            set => ProductCode = value; 
        }
        
        [Obsolete("Use UnitPrice instead")]
        public decimal Price 
        { 
            get => UnitPrice; 
            set => UnitPrice = value; 
        }
    }
}