namespace WinFormsFashionShop.DTO
{
    /// <summary>
    /// Data Transfer Object for Product - used for transferring product data between layers
    /// </summary>
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; } // For display purposes
        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = "cái";
        public string? ImagePath { get; set; } // Đường dẫn ảnh sản phẩm
        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// DTO for creating a new product
    /// </summary>
    public class CreateProductDTO
    {
        public string ProductCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = "cái";
        public string? ImagePath { get; set; } // Đường dẫn ảnh sản phẩm (optional khi tạo mới)
    }

    /// <summary>
    /// DTO for updating an existing product
    /// </summary>
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = "cái";
        public string? ImagePath { get; set; } // Đường dẫn ảnh sản phẩm (optional khi cập nhật)
        public bool IsActive { get; set; } = true;
    }
}

