using System;

namespace WinFormsFashionShop.Data.Entities
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int QuantityInStock { get; set; } // QuantityOnHand -> QuantityInStock
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        
        // Backward compatibility
        [Obsolete("Use QuantityInStock instead")]
        public int QuantityOnHand 
        { 
            get => QuantityInStock; 
            set => QuantityInStock = value; 
        }
    }
}