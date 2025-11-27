using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            using var cmd = new SqlCommand($"SELECT Id, CustomerId, OrderDate, Total FROM {TableName}", conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new Order
                {
                    Id = rdr.GetInt32(0),
                    CustomerId = rdr.GetInt32(1),
                    OrderDate = rdr.GetDateTime(2),
                    Total = rdr.GetDecimal(3)
                });
            }
            return list;
        }

        public Order? GetById(int id)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, CustomerId, OrderDate, Total FROM {TableName} WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return new Order
                {
                    Id = rdr.GetInt32(0),
                    CustomerId = rdr.GetInt32(1),
                    OrderDate = rdr.GetDateTime(2),
                    Total = rdr.GetDecimal(3)
                };
            }
            return null;
        }

        public void Insert(Order entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"INSERT INTO {TableName} (CustomerId, OrderDate, Total) VALUES (@CustomerId, @OrderDate, @Total); SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
            cmd.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
            cmd.Parameters.AddWithValue("@Total", entity.Total);
            entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void Update(Order entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"UPDATE {TableName} SET CustomerId=@CustomerId, OrderDate=@OrderDate, Total=@Total WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
            cmd.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
            cmd.Parameters.AddWithValue("@Total", entity.Total);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.ExecuteNonQuery();
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