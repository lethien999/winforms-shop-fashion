namespace WinFormsFashionShop.DTO
{
    /// <summary>
    /// Data Transfer Object for Category
    /// </summary>
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// DTO for creating a new category
    /// </summary>
    public class CreateCategoryDTO
    {
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    /// <summary>
    /// DTO for updating an existing category
    /// </summary>
    public class UpdateCategoryDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

