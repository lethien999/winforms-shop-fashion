using System;
using System.Collections.Generic;

namespace WinFormsFashionShop.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderCode { get; set; } = string.Empty; // Mã hóa đơn
        public int? PayOSOrderCode { get; set; } // Mã đơn hàng PayOS (orderCode từ PayOS API) - unique
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int? CustomerId { get; set; } // nullable - cho phép hóa đơn không gắn khách
        public int UserId { get; set; } // Nhân viên lập hóa đơn
        public decimal TotalAmount { get; set; } // Total -> TotalAmount
        public string? PaymentMethod { get; set; } // Phương thức thanh toán (Tiền mặt, Thẻ, Chuyển khoản...)
        public string? Notes { get; set; }
        public string Status { get; set; } = "Paid"; // Paid, Cancelled, Pending
        public DateTime? PaidAt { get; set; } // Thời gian thanh toán thành công (từ webhook PayOS)
        public string? TransactionId { get; set; } // Transaction ID từ PayOS (để tracking)
        public DateTime? PrintedAt { get; set; } // Thời gian in hóa đơn lần đầu (để tránh in trùng)
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