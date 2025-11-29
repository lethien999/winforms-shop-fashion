namespace WinFormsFashionShop.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty; // Name -> CategoryName
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        
        // Backward compatibility
        [Obsolete("Use CategoryName instead")]
        public string Name 
        { 
            get => CategoryName; 
            set => CategoryName = value; 
        }
    }
}