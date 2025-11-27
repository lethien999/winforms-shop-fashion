namespace WinFormsFashionShop.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}