namespace WinFormsFashionShop.Presentation.Models
{
    /// <summary>
    /// Request model để tạo payment link
    /// </summary>
    public class CreatePaymentRequest
    {
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ReturnUrl { get; set; }
        public string? CancelUrl { get; set; }
    }
}

