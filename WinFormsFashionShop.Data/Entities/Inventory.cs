namespace WinFormsFashionShop.Data.Entities
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int QuantityOnHand { get; set; }
    }
}