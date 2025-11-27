using System;
using System.Collections.Generic;

namespace WinFormsFashionShop.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}