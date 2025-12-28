using Microsoft.Data.SqlClient;
using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Data.Repositories
{
    public class PaymentTransactionRepository : RepositoryBase, IPaymentTransactionRepository
    {
        /// <summary>
        /// Tạo mới payment transaction record
        /// </summary>
        public PaymentTransaction Create(PaymentTransaction transaction)
        {
            using var conn = CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            
            cmd.CommandText = @"
                INSERT INTO PaymentTransactions 
                (OrderId, PayOSOrderCode, PaymentLinkId, Amount, Description, 
                 Bin, AccountNumber, AccountName, BankName, Currency,
                 Status, CheckoutUrl, QrCode, CreatedAt, ExpiredAt,
                 Source, IpAddress, RawData)
                OUTPUT INSERTED.Id
                VALUES 
                (@OrderId, @PayOSOrderCode, @PaymentLinkId, @Amount, @Description,
                 @Bin, @AccountNumber, @AccountName, @BankName, @Currency,
                 @Status, @CheckoutUrl, @QrCode, @CreatedAt, @ExpiredAt,
                 @Source, @IpAddress, @RawData)";
            
            cmd.Parameters.AddWithValue("@OrderId", transaction.OrderId);
            cmd.Parameters.AddWithValue("@PayOSOrderCode", transaction.PayOSOrderCode);
            cmd.Parameters.AddWithValue("@PaymentLinkId", (object?)transaction.PaymentLinkId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Amount", transaction.Amount);
            cmd.Parameters.AddWithValue("@Description", (object?)transaction.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Bin", (object?)transaction.Bin ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AccountNumber", (object?)transaction.AccountNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AccountName", (object?)transaction.AccountName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@BankName", (object?)transaction.BankName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Currency", (object?)transaction.Currency ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", (object?)transaction.Status ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CheckoutUrl", (object?)transaction.CheckoutUrl ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@QrCode", (object?)transaction.QrCode ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedAt", transaction.CreatedAt);
            cmd.Parameters.AddWithValue("@ExpiredAt", (object?)transaction.ExpiredAt ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Source", (object?)transaction.Source ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IpAddress", (object?)transaction.IpAddress ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RawData", (object?)transaction.RawData ?? DBNull.Value);
            
            transaction.Id = (int)cmd.ExecuteScalar();
            return transaction;
        }
        
        /// <summary>
        /// Cập nhật payment transaction
        /// </summary>
        public void Update(PaymentTransaction transaction)
        {
            using var conn = CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            
            cmd.CommandText = @"
                UPDATE PaymentTransactions SET
                    Status = @Status,
                    PaidAt = @PaidAt,
                    Reference = @Reference,
                    TransactionDateTime = @TransactionDateTime,
                    WebhookId = @WebhookId,
                    RawData = @RawData,
                    UpdatedAt = @UpdatedAt
                WHERE Id = @Id";
            
            cmd.Parameters.AddWithValue("@Id", transaction.Id);
            cmd.Parameters.AddWithValue("@Status", (object?)transaction.Status ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PaidAt", (object?)transaction.PaidAt ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Reference", (object?)transaction.Reference ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TransactionDateTime", (object?)transaction.TransactionDateTime ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@WebhookId", (object?)transaction.WebhookId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RawData", (object?)transaction.RawData ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
            
            cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Lấy payment transaction theo PayOSOrderCode
        /// </summary>
        public PaymentTransaction? GetByPayOSOrderCode(long payOSOrderCode)
        {
            using var conn = CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            
            cmd.CommandText = "SELECT * FROM PaymentTransactions WHERE PayOSOrderCode = @PayOSOrderCode";
            cmd.Parameters.AddWithValue("@PayOSOrderCode", payOSOrderCode);
            
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapFromReader(reader);
            }
            return null;
        }
        
        /// <summary>
        /// Lấy tất cả payment transactions của một order
        /// </summary>
        public IEnumerable<PaymentTransaction> GetByOrderId(int orderId)
        {
            var transactions = new List<PaymentTransaction>();
            
            using var conn = CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            
            cmd.CommandText = "SELECT * FROM PaymentTransactions WHERE OrderId = @OrderId ORDER BY CreatedAt DESC";
            cmd.Parameters.AddWithValue("@OrderId", orderId);
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                transactions.Add(MapFromReader(reader));
            }
            return transactions;
        }
        
        /// <summary>
        /// Lấy payment transaction theo ID
        /// </summary>
        public PaymentTransaction? GetById(int id)
        {
            using var conn = CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            
            cmd.CommandText = "SELECT * FROM PaymentTransactions WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
            
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapFromReader(reader);
            }
            return null;
        }
        
        /// <summary>
        /// Lấy tất cả payment transactions với paging
        /// </summary>
        public IEnumerable<PaymentTransaction> GetAll(int page = 1, int pageSize = 50)
        {
            var transactions = new List<PaymentTransaction>();
            
            using var conn = CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            
            cmd.CommandText = @"
                SELECT * FROM PaymentTransactions 
                ORDER BY CreatedAt DESC
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            
            cmd.Parameters.AddWithValue("@Offset", (page - 1) * pageSize);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                transactions.Add(MapFromReader(reader));
            }
            return transactions;
        }
        
        /// <summary>
        /// Lấy payment transactions theo status
        /// </summary>
        public IEnumerable<PaymentTransaction> GetByStatus(string status, int page = 1, int pageSize = 50)
        {
            var transactions = new List<PaymentTransaction>();
            
            using var conn = CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            
            cmd.CommandText = @"
                SELECT * FROM PaymentTransactions 
                WHERE Status = @Status
                ORDER BY CreatedAt DESC
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Offset", (page - 1) * pageSize);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                transactions.Add(MapFromReader(reader));
            }
            return transactions;
        }
        
        /// <summary>
        /// Cập nhật status khi nhận webhook
        /// </summary>
        public void UpdateStatusFromWebhook(long payOSOrderCode, string status, string? reference, string? transactionDateTime, string? webhookId, string? rawData)
        {
            using var conn = CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            
            cmd.CommandText = @"
                UPDATE PaymentTransactions SET
                    Status = @Status,
                    Reference = @Reference,
                    TransactionDateTime = @TransactionDateTime,
                    WebhookId = @WebhookId,
                    RawData = COALESCE(@RawData, RawData),
                    PaidAt = CASE WHEN @Status = 'PAID' THEN GETDATE() ELSE PaidAt END,
                    UpdatedAt = GETDATE()
                WHERE PayOSOrderCode = @PayOSOrderCode";
            
            cmd.Parameters.AddWithValue("@PayOSOrderCode", payOSOrderCode);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Reference", (object?)reference ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TransactionDateTime", (object?)transactionDateTime ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@WebhookId", (object?)webhookId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RawData", (object?)rawData ?? DBNull.Value);
            
            cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Map SqlDataReader to PaymentTransaction entity
        /// </summary>
        private static PaymentTransaction MapFromReader(SqlDataReader reader)
        {
            return new PaymentTransaction
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
                PayOSOrderCode = reader.GetInt64(reader.GetOrdinal("PayOSOrderCode")),
                PaymentLinkId = reader.IsDBNull(reader.GetOrdinal("PaymentLinkId")) ? null : reader.GetString(reader.GetOrdinal("PaymentLinkId")),
                Amount = reader.GetInt32(reader.GetOrdinal("Amount")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                Bin = reader.IsDBNull(reader.GetOrdinal("Bin")) ? null : reader.GetString(reader.GetOrdinal("Bin")),
                AccountNumber = reader.IsDBNull(reader.GetOrdinal("AccountNumber")) ? null : reader.GetString(reader.GetOrdinal("AccountNumber")),
                AccountName = reader.IsDBNull(reader.GetOrdinal("AccountName")) ? null : reader.GetString(reader.GetOrdinal("AccountName")),
                BankName = reader.IsDBNull(reader.GetOrdinal("BankName")) ? null : reader.GetString(reader.GetOrdinal("BankName")),
                Currency = reader.IsDBNull(reader.GetOrdinal("Currency")) ? null : reader.GetString(reader.GetOrdinal("Currency")),
                Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString(reader.GetOrdinal("Status")),
                CheckoutUrl = reader.IsDBNull(reader.GetOrdinal("CheckoutUrl")) ? null : reader.GetString(reader.GetOrdinal("CheckoutUrl")),
                QrCode = reader.IsDBNull(reader.GetOrdinal("QrCode")) ? null : reader.GetString(reader.GetOrdinal("QrCode")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                ExpiredAt = reader.IsDBNull(reader.GetOrdinal("ExpiredAt")) ? null : reader.GetDateTime(reader.GetOrdinal("ExpiredAt")),
                PaidAt = reader.IsDBNull(reader.GetOrdinal("PaidAt")) ? null : reader.GetDateTime(reader.GetOrdinal("PaidAt")),
                Reference = reader.IsDBNull(reader.GetOrdinal("Reference")) ? null : reader.GetString(reader.GetOrdinal("Reference")),
                TransactionDateTime = reader.IsDBNull(reader.GetOrdinal("TransactionDateTime")) ? null : reader.GetString(reader.GetOrdinal("TransactionDateTime")),
                WebhookId = reader.IsDBNull(reader.GetOrdinal("WebhookId")) ? null : reader.GetString(reader.GetOrdinal("WebhookId")),
                RawData = reader.IsDBNull(reader.GetOrdinal("RawData")) ? null : reader.GetString(reader.GetOrdinal("RawData")),
                Source = reader.IsDBNull(reader.GetOrdinal("Source")) ? null : reader.GetString(reader.GetOrdinal("Source")),
                IpAddress = reader.IsDBNull(reader.GetOrdinal("IpAddress")) ? null : reader.GetString(reader.GetOrdinal("IpAddress")),
                UpdatedAt = reader.IsDBNull(reader.GetOrdinal("UpdatedAt")) ? null : reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
            };
        }
    }
}
