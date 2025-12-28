namespace API.Models
{
    /// <summary>
    /// Response model khi tạo payment link thành công
    /// Bao gồm đầy đủ thông tin từ PayOS để hiển thị chi tiết thanh toán
    /// </summary>
    public class CreatePaymentResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public PaymentData? Data { get; set; }
    }

    /// <summary>
    /// Thông tin thanh toán chi tiết từ PayOS
    /// Bao gồm thông tin tài khoản ngân hàng để hiển thị cho người dùng
    /// </summary>
    public class PaymentData
    {
        /// <summary>PayOS order code (unique identifier)</summary>
        public int OrderCode { get; set; }
        
        /// <summary>QR code data (EMV string hoặc base64 image)</summary>
        public string QrCode { get; set; } = string.Empty;
        
        /// <summary>Checkout URL để mở trên web</summary>
        public string CheckoutUrl { get; set; } = string.Empty;
        
        /// <summary>Số tiền thanh toán (VNĐ)</summary>
        public int Amount { get; set; }
        
        /// <summary>Mô tả thanh toán</summary>
        public string Description { get; set; } = string.Empty;
        
        // ========== THÔNG TIN NGÂN HÀNG (từ PayOS response) ==========
        
        /// <summary>Mã BIN ngân hàng (VD: "970422" = MB Bank)</summary>
        public string? Bin { get; set; }
        
        /// <summary>Số tài khoản nhận tiền</summary>
        public string? AccountNumber { get; set; }
        
        /// <summary>Tên tài khoản nhận tiền</summary>
        public string? AccountName { get; set; }
        
        /// <summary>ID của payment link từ PayOS</summary>
        public string? PaymentLinkId { get; set; }
        
        /// <summary>Trạng thái thanh toán (PENDING, PAID, CANCELLED, etc.)</summary>
        public string? Status { get; set; }
        
        /// <summary>Loại tiền tệ (VND)</summary>
        public string Currency { get; set; } = "VND";
        
        // ========== THÔNG TIN BỔ SUNG ==========
        
        /// <summary>Tên ngân hàng (mapping từ BIN)</summary>
        public string? BankName { get; set; }
        
        /// <summary>Thời gian tạo payment link</summary>
        public DateTime? CreatedAt { get; set; }
        
        /// <summary>Thời gian hết hạn payment link</summary>
        public DateTime? ExpiredAt { get; set; }
    }
}

