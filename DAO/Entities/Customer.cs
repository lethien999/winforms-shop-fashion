using System;

namespace WinFormsFashionShop.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty; // FullName -> CustomerName
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        
        // Backward compatibility
        [Obsolete("Use CustomerName instead")]
        public string FullName 
        { 
            get => CustomerName; 
            set => CustomerName = value; 
        }
    }
}