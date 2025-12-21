using System;
using System.Collections.Generic;
using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Data.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        IEnumerable<Order> GetByDateRange(DateTime from, DateTime to);
        IEnumerable<Order> GetByCustomerId(int customerId);
        IEnumerable<Order> GetByUserId(int userId);
        Order? GetByPayOSOrderCode(int payOSOrderCode);
        
        /// <summary>
        /// Xử lý webhook PayOS qua Stored Procedure với Transaction, Idempotency, Audit log
        /// </summary>
        ProcessWebhookResult ProcessPayOSWebhook(
            string webhookId,
            int payOSOrderCode,
            string code,
            int amount,
            string? reference,
            string? paymentLinkId,
            string? rawData,
            string? ipAddress,
            string? userAgent);
    }
    
    /// <summary>
    /// Kết quả xử lý webhook từ Stored Procedure
    /// </summary>
    public class ProcessWebhookResult
    {
        public string Result { get; set; } = string.Empty; // Success, Failed, Error
        public string Message { get; set; } = string.Empty;
        public int? OrderId { get; set; }
        public string? CurrentStatus { get; set; }
    }
}