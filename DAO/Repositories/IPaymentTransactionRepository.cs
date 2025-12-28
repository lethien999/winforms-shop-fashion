using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Data.Repositories
{
    public interface IPaymentTransactionRepository
    {
        /// <summary>
        /// Tạo mới payment transaction record
        /// </summary>
        PaymentTransaction Create(PaymentTransaction transaction);
        
        /// <summary>
        /// Cập nhật payment transaction (khi có webhook hoặc check status)
        /// </summary>
        void Update(PaymentTransaction transaction);
        
        /// <summary>
        /// Lấy payment transaction theo PayOSOrderCode
        /// </summary>
        PaymentTransaction? GetByPayOSOrderCode(long payOSOrderCode);
        
        /// <summary>
        /// Lấy tất cả payment transactions của một order
        /// </summary>
        IEnumerable<PaymentTransaction> GetByOrderId(int orderId);
        
        /// <summary>
        /// Lấy payment transaction theo ID
        /// </summary>
        PaymentTransaction? GetById(int id);
        
        /// <summary>
        /// Lấy tất cả payment transactions với paging
        /// </summary>
        IEnumerable<PaymentTransaction> GetAll(int page = 1, int pageSize = 50);
        
        /// <summary>
        /// Lấy payment transactions theo status
        /// </summary>
        IEnumerable<PaymentTransaction> GetByStatus(string status, int page = 1, int pageSize = 50);
        
        /// <summary>
        /// Cập nhật status khi nhận webhook
        /// </summary>
        void UpdateStatusFromWebhook(long payOSOrderCode, string status, string? reference, string? transactionDateTime, string? webhookId, string? rawData);
    }
}
