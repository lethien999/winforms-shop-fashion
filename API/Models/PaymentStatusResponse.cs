namespace API.Models
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
        public int OrderId { get; set; } // ID đơn hàng trong hệ thống
        public int? PayOSOrderCode { get; set; } // PayOS order code
        public string Status { get; set; } = string.Empty; // PENDING, PAID, CANCELLED
        public DateTime? PaidAt { get; set; } // Thời gian thanh toán thành công
        public string? TransactionId { get; set; } // Transaction ID từ PayOS
    }
}

