using System;
using System.Collections.Generic;

namespace WinFormsFashionShop.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderCode { get; set; } = string.Empty; // Mã hóa đơn
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int? CustomerId { get; set; } // nullable - cho phép hóa đơn không gắn khách
        public int UserId { get; set; } // Nhân viên lập hóa đơn
        public decimal TotalAmount { get; set; } // Total -> TotalAmount
        public string? Notes { get; set; }
        public string Status { get; set; } = "Paid"; // Paid, Cancelled
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        
        // Backward compatibility
        [Obsolete("Use TotalAmount instead")]
        public decimal Total 
        { 
            get => TotalAmount; 
            set => TotalAmount = value; 
        }
    }
}