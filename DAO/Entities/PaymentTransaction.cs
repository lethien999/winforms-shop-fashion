using System;

namespace WinFormsFashionShop.Data.Entities
{
    /// <summary>
    /// Entity lưu trữ thông tin giao dịch thanh toán PayOS
    /// Dùng để tracking toàn bộ lifecycle của payment link
    /// </summary>
    public class PaymentTransaction
    {
        public int Id { get; set; }
        
        /// <summary>
        /// OrderId trong hệ thống local
        /// </summary>
        public int OrderId { get; set; }
        
        /// <summary>
        /// PayOS Order Code - unique identifier từ PayOS
        /// </summary>
        public long PayOSOrderCode { get; set; }
        
        /// <summary>
        /// Payment Link ID từ PayOS
        /// </summary>
        public string? PaymentLinkId { get; set; }
        
        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        public int Amount { get; set; }
        
        /// <summary>
        /// Nội dung chuyển khoản
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// Mã BIN ngân hàng nhận
        /// </summary>
        public string? Bin { get; set; }
        
        /// <summary>
        /// Số tài khoản nhận
        /// </summary>
        public string? AccountNumber { get; set; }
        
        /// <summary>
        /// Tên tài khoản nhận
        /// </summary>
        public string? AccountName { get; set; }
        
        /// <summary>
        /// Tên ngân hàng (được map từ BIN)
        /// </summary>
        public string? BankName { get; set; }
        
        /// <summary>
        /// Mã tiền tệ (VND)
        /// </summary>
        public string? Currency { get; set; }
        
        /// <summary>
        /// Trạng thái giao dịch: PENDING, PAID, CANCELLED, EXPIRED
        /// </summary>
        public string? Status { get; set; }
        
        /// <summary>
        /// Checkout URL từ PayOS
        /// </summary>
        public string? CheckoutUrl { get; set; }
        
        /// <summary>
        /// QR Code data (base64 hoặc URL)
        /// </summary>
        public string? QrCode { get; set; }
        
        /// <summary>
        /// Thời gian tạo payment link
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Thời gian payment link hết hạn
        /// </summary>
        public DateTime? ExpiredAt { get; set; }
        
        /// <summary>
        /// Thời gian thanh toán thành công
        /// </summary>
        public DateTime? PaidAt { get; set; }
        
        /// <summary>
        /// Mã reference từ ngân hàng (khi đã thanh toán)
        /// </summary>
        public string? Reference { get; set; }
        
        /// <summary>
        /// Transaction DateTime từ webhook
        /// </summary>
        public string? TransactionDateTime { get; set; }
        
        /// <summary>
        /// Mã webhook ID (để trace)
        /// </summary>
        public string? WebhookId { get; set; }
        
        /// <summary>
        /// Raw JSON data từ PayOS (để debug)
        /// </summary>
        public string? RawData { get; set; }
        
        /// <summary>
        /// Nguồn tạo: GUI, API, WEBHOOK
        /// </summary>
        public string? Source { get; set; }
        
        /// <summary>
        /// IP Address của request
        /// </summary>
        public string? IpAddress { get; set; }
        
        /// <summary>
        /// Lần cập nhật cuối cùng
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation property
        public Order? Order { get; set; }
    }
}
