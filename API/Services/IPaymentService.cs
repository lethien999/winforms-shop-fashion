using API.Models;
using WinFormsFashionShop.DTO;

namespace API.Services
{
    /// <summary>
    /// Service interface để xử lý thanh toán PayOS
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Tạo payment link từ PayOS
        /// </summary>
        Task<CreatePaymentResponse> CreatePaymentLinkAsync(CreatePaymentRequest request);

        /// <summary>
        /// Kiểm tra trạng thái thanh toán từ database
        /// </summary>
        Task<PaymentStatusResponse> GetPaymentStatusAsync(int orderId);

        /// <summary>
        /// Xử lý webhook từ PayOS
        /// </summary>
        Task<bool> HandleWebhookAsync(PayOSWebhookRequest webhookRequest, string? ipAddress = null, string? userAgent = null);

        /// <summary>
        /// Sync PayOSOrderCode tự động (hiện tại không hỗ trợ vì PayOS API không có endpoint để list payments)
        /// </summary>
        Task<object> SyncPayOSOrderCodesAsync(int? orderId = null);

        /// <summary>
        /// Force update payment status khi PayOS API không kết nối được nhưng đã thanh toán trên PayOS web
        /// </summary>
        Task<PaymentStatusResponse> ForceUpdatePaidStatusAsync(int orderId);
    }
}

