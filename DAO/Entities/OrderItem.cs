namespace WinFormsFashionShop.Data.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // Đơn giá tại thời điểm bán
        public decimal LineTotal { get; set; } // Thành tiền = Quantity * UnitPrice
    }
}