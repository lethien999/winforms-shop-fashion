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
            using var cmd = new SqlCommand($"SELECT Id, OrderCode, OrderDate, CustomerId, UserId, TotalAmount, Notes, Status FROM {TableName}", conn);
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
            using var cmd = new SqlCommand($"SELECT Id, OrderCode, OrderDate, CustomerId, UserId, TotalAmount, Notes, Status FROM {TableName} WHERE Id=@Id", conn);
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
            using var cmd = new SqlCommand($"INSERT INTO {TableName} (OrderCode, OrderDate, CustomerId, UserId, TotalAmount, Notes, Status) VALUES (@OrderCode, @OrderDate, @CustomerId, @UserId, @TotalAmount, @Notes, @Status); SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@OrderCode", entity.OrderCode);
            cmd.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
            cmd.Parameters.AddWithValue("@CustomerId", (object?)entity.CustomerId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UserId", entity.UserId);
            cmd.Parameters.AddWithValue("@TotalAmount", entity.TotalAmount);
            cmd.Parameters.AddWithValue("@Notes", (object?)entity.Notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", entity.Status);
            entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void Update(Order entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"UPDATE {TableName} SET OrderCode=@OrderCode, OrderDate=@OrderDate, CustomerId=@CustomerId, UserId=@UserId, TotalAmount=@TotalAmount, Notes=@Notes, Status=@Status WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@OrderCode", entity.OrderCode);
            cmd.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
            cmd.Parameters.AddWithValue("@CustomerId", (object?)entity.CustomerId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UserId", entity.UserId);
            cmd.Parameters.AddWithValue("@TotalAmount", entity.TotalAmount);
            cmd.Parameters.AddWithValue("@Notes", (object?)entity.Notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", entity.Status);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Order> GetByDateRange(DateTime from, DateTime to)
        {
            var list = new List<Order>();
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, OrderCode, OrderDate, CustomerId, UserId, TotalAmount, Notes, Status FROM {TableName} WHERE OrderDate >= @From AND OrderDate <= @To", conn);
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
            using var cmd = new SqlCommand($"SELECT Id, OrderCode, OrderDate, CustomerId, UserId, TotalAmount, Notes, Status FROM {TableName} WHERE CustomerId = @CustomerId", conn);
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
            using var cmd = new SqlCommand($"SELECT Id, OrderCode, OrderDate, CustomerId, UserId, TotalAmount, Notes, Status FROM {TableName} WHERE UserId = @UserId", conn);
            cmd.Parameters.AddWithValue("@UserId", userId);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(MapReaderToOrder(rdr));
            }
            return list;
        }

        private Order MapReaderToOrder(SqlDataReader rdr)
        {
            return new Order
            {
                Id = rdr.GetInt32(0),
                OrderCode = rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1),
                OrderDate = rdr.GetDateTime(2),
                CustomerId = rdr.IsDBNull(3) ? null : rdr.GetInt32(3),
                UserId = rdr.GetInt32(4),
                TotalAmount = rdr.GetDecimal(5),
                Notes = rdr.IsDBNull(6) ? null : rdr.GetString(6),
                Status = rdr.IsDBNull(7) ? "Paid" : rdr.GetString(7)
            };
        }

        public void Delete(int id)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"DELETE FROM {TableName} WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}