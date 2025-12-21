namespace API.Models
{
    /// <summary>
    /// Request model để tạo payment link từ PayOS
    /// </summary>
    public class CreatePaymentRequest
    {
        public int OrderId { get; set; } // ID đơn hàng trong hệ thống
        public int Amount { get; set; } // Số tiền (VND, integer)
        public string Description { get; set; } = string.Empty; // Mô tả đơn hàng
        public string? ReturnUrl { get; set; } // URL trả về sau khi thanh toán
        public string? CancelUrl { get; set; } // URL trả về nếu hủy thanh toán
    }
}

