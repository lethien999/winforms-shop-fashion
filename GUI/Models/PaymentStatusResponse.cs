namespace WinFormsFashionShop.Presentation.Models
{
    /// <summary>
    /// Response model khi kiểm tra trạng thái thanh toán
    /// </summary>
    public class PaymentStatusResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public PaymentStatusData? Data { get; set; }
    }

    public class PaymentStatusData
    {
        public int OrderId { get; set; }
        public int? PayOSOrderCode { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? PaidAt { get; set; }
        public string? TransactionId { get; set; }
    }
}

