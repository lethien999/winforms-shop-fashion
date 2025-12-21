namespace API.Models
{
    /// <summary>
    /// Response model khi tạo payment link thành công
    /// </summary>
    public class CreatePaymentResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public PaymentData? Data { get; set; }
    }

    public class PaymentData
    {
        public int OrderCode { get; set; } // PayOS order code
        public string QrCode { get; set; } = string.Empty; // QR code data
        public string CheckoutUrl { get; set; } = string.Empty; // Checkout URL
        public int Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

