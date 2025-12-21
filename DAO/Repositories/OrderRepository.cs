using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Data.Repositories
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        private const string TableName = "Orders";

        public IEnumerable<Order> GetAll()
        {
            var list = new List<Order>();
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, OrderCode, PayOSOrderCode, OrderDate, CustomerId, UserId, TotalAmount, PaymentMethod, Notes, Status, PaidAt, TransactionId, PrintedAt FROM {TableName}", conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(MapReaderToOrder(rdr));
            }
            return list;
        }

        public Order? GetById(int id)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, OrderCode, PayOSOrderCode, OrderDate, CustomerId, UserId, TotalAmount, PaymentMethod, Notes, Status, PaidAt, TransactionId, PrintedAt FROM {TableName} WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return MapReaderToOrder(rdr);
            }
            return null;
        }

        public void Insert(Order entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"INSERT INTO {TableName} (OrderCode, PayOSOrderCode, OrderDate, CustomerId, UserId, TotalAmount, PaymentMethod, Notes, Status, PaidAt, TransactionId, PrintedAt) VALUES (@OrderCode, @PayOSOrderCode, @OrderDate, @CustomerId, @UserId, @TotalAmount, @PaymentMethod, @Notes, @Status, @PaidAt, @TransactionId, @PrintedAt); SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@OrderCode", entity.OrderCode);
            cmd.Parameters.AddWithValue("@PayOSOrderCode", (object?)entity.PayOSOrderCode ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
            cmd.Parameters.AddWithValue("@CustomerId", (object?)entity.CustomerId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UserId", entity.UserId);
            cmd.Parameters.AddWithValue("@TotalAmount", entity.TotalAmount);
            cmd.Parameters.AddWithValue("@PaymentMethod", (object?)entity.PaymentMethod ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Notes", (object?)entity.Notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", entity.Status);
            cmd.Parameters.AddWithValue("@PaidAt", (object?)entity.PaidAt ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TransactionId", (object?)entity.TransactionId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PrintedAt", (object?)entity.PrintedAt ?? DBNull.Value);
            entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void Update(Order entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"UPDATE {TableName} SET OrderCode=@OrderCode, PayOSOrderCode=@PayOSOrderCode, OrderDate=@OrderDate, CustomerId=@CustomerId, UserId=@UserId, TotalAmount=@TotalAmount, PaymentMethod=@PaymentMethod, Notes=@Notes, Status=@Status, PaidAt=@PaidAt, TransactionId=@TransactionId, PrintedAt=@PrintedAt WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@OrderCode", entity.OrderCode);
            cmd.Parameters.AddWithValue("@PayOSOrderCode", (object?)entity.PayOSOrderCode ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
            cmd.Parameters.AddWithValue("@CustomerId", (object?)entity.CustomerId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UserId", entity.UserId);
            cmd.Parameters.AddWithValue("@TotalAmount", entity.TotalAmount);
            cmd.Parameters.AddWithValue("@PaymentMethod", (object?)entity.PaymentMethod ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Notes", (object?)entity.Notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", entity.Status);
            cmd.Parameters.AddWithValue("@PaidAt", (object?)entity.PaidAt ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TransactionId", (object?)entity.TransactionId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PrintedAt", (object?)entity.PrintedAt ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Order> GetByDateRange(DateTime from, DateTime to)
        {
            var list = new List<Order>();
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, OrderCode, PayOSOrderCode, OrderDate, CustomerId, UserId, TotalAmount, PaymentMethod, Notes, Status, PaidAt, TransactionId, PrintedAt FROM {TableName} WHERE OrderDate >= @From AND OrderDate <= @To", conn);
            cmd.Parameters.AddWithValue("@From", from.Date);
            cmd.Parameters.AddWithValue("@To", to.Date.AddDays(1).AddSeconds(-1)); // End of day
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(MapReaderToOrder(rdr));
            }
            return list;
        }

        public IEnumerable<Order> GetByCustomerId(int customerId)
        {
            var list = new List<Order>();
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, OrderCode, PayOSOrderCode, OrderDate, CustomerId, UserId, TotalAmount, PaymentMethod, Notes, Status, PaidAt, TransactionId, PrintedAt FROM {TableName} WHERE CustomerId = @CustomerId", conn);
            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(MapReaderToOrder(rdr));
            }
            return list;
        }

        public IEnumerable<Order> GetByUserId(int userId)
        {
            var list = new List<Order>();
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, OrderCode, PayOSOrderCode, OrderDate, CustomerId, UserId, TotalAmount, PaymentMethod, Notes, Status, PaidAt, TransactionId, PrintedAt FROM {TableName} WHERE UserId = @UserId", conn);
            cmd.Parameters.AddWithValue("@UserId", userId);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(MapReaderToOrder(rdr));
            }
            return list;
        }

        public Order? GetByPayOSOrderCode(int payOSOrderCode)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, OrderCode, PayOSOrderCode, OrderDate, CustomerId, UserId, TotalAmount, PaymentMethod, Notes, Status, PaidAt, TransactionId, PrintedAt FROM {TableName} WHERE PayOSOrderCode=@PayOSOrderCode", conn);
            cmd.Parameters.AddWithValue("@PayOSOrderCode", payOSOrderCode);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return MapReaderToOrder(rdr);
            }
            return null;
        }

        /// <summary>
        /// Maps SQL data reader to Order entity.
        /// Single responsibility: only maps data from reader to entity.
        /// </summary>
        private Order MapReaderToOrder(SqlDataReader rdr)
        {
            return new Order
            {
                Id = rdr.GetInt32(0),
                OrderCode = rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1),
                PayOSOrderCode = rdr.IsDBNull(2) ? null : rdr.GetInt32(2),
                OrderDate = rdr.GetDateTime(3),
                CustomerId = rdr.IsDBNull(4) ? null : rdr.GetInt32(4),
                UserId = rdr.GetInt32(5),
                TotalAmount = rdr.GetDecimal(6),
                PaymentMethod = rdr.IsDBNull(7) ? null : rdr.GetString(7),
                Notes = rdr.IsDBNull(8) ? null : rdr.GetString(8),
                Status = rdr.IsDBNull(9) ? "Paid" : rdr.GetString(9),
                PaidAt = rdr.IsDBNull(10) ? null : rdr.GetDateTime(10),
                TransactionId = rdr.IsDBNull(11) ? null : rdr.GetString(11),
                PrintedAt = rdr.IsDBNull(12) ? null : rdr.GetDateTime(12)
            };
        }

        public void Delete(int id)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"DELETE FROM {TableName} WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Xử lý webhook PayOS qua Stored Procedure với Transaction, Idempotency, Audit log
        /// </summary>
        public ProcessWebhookResult ProcessPayOSWebhook(
            string webhookId,
            int payOSOrderCode,
            string code,
            int amount,
            string? reference,
            string? paymentLinkId,
            string? rawData,
            string? ipAddress,
            string? userAgent)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand("ProcessPayOSWebhook", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@WebhookId", webhookId);
            cmd.Parameters.AddWithValue("@PayOSOrderCode", payOSOrderCode);
            cmd.Parameters.AddWithValue("@Code", code);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@Reference", (object?)reference ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PaymentLinkId", (object?)paymentLinkId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RawData", (object?)rawData ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IPAddress", (object?)ipAddress ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UserAgent", (object?)userAgent ?? DBNull.Value);

            var returnValue = new SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.ReturnValue
            };
            cmd.Parameters.Add(returnValue);

            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return new ProcessWebhookResult
                {
                    Result = rdr.IsDBNull(0) ? "Error" : rdr.GetString(0),
                    Message = rdr.IsDBNull(1) ? "" : rdr.GetString(1),
                    OrderId = rdr.IsDBNull(2) ? null : rdr.GetInt32(2),
                    CurrentStatus = rdr.IsDBNull(3) ? null : rdr.GetString(3)
                };
            }

            return new ProcessWebhookResult
            {
                Result = "Error",
                Message = "Stored procedure không trả về kết quả"
            };
        }
    }
}