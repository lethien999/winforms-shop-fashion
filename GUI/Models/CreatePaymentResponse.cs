namespace WinFormsFashionShop.Presentation.Models
{
    /// <summary>
    /// Response model khi tạo payment link
    /// </summary>
    public class CreatePaymentResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public PaymentData? Data { get; set; }
    }

    public class PaymentData
    {
        public int OrderCode { get; set; }
        public string QrCode { get; set; } = string.Empty;
        public string CheckoutUrl { get; set; } = string.Empty;
        public int Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

