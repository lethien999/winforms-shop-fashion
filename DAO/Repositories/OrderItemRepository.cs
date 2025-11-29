using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Data.Repositories
{
    public class OrderItemRepository : RepositoryBase, IOrderItemRepository
    {
        private const string TableName = "OrderItems";

        public IEnumerable<OrderItem> GetAll()
        {
            var list = new List<OrderItem>();
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, OrderId, ProductId, Quantity, UnitPrice, LineTotal FROM {TableName}", conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) list.Add(MapReaderToOrderItem(rdr));
            return list;
        }

        public OrderItem? GetById(int id)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, OrderId, ProductId, Quantity, UnitPrice, LineTotal FROM {TableName} WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read()) return MapReaderToOrderItem(rdr);
            return null;
        }

        public void Insert(OrderItem entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            // Calculate LineTotal if not set
            if (entity.LineTotal == 0)
            {
                entity.LineTotal = entity.Quantity * entity.UnitPrice;
            }
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"INSERT INTO {TableName} (OrderId, ProductId, Quantity, UnitPrice, LineTotal) VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice, @LineTotal); SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@OrderId", entity.OrderId);
            cmd.Parameters.AddWithValue("@ProductId", entity.ProductId);
            cmd.Parameters.AddWithValue("@Quantity", entity.Quantity);
            cmd.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice);
            cmd.Parameters.AddWithValue("@LineTotal", entity.LineTotal);
            entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void Update(OrderItem entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            // Calculate LineTotal if not set
            if (entity.LineTotal == 0)
            {
                entity.LineTotal = entity.Quantity * entity.UnitPrice;
            }
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"UPDATE {TableName} SET OrderId=@OrderId, ProductId=@ProductId, Quantity=@Quantity, UnitPrice=@UnitPrice, LineTotal=@LineTotal WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@OrderId", entity.OrderId);
            cmd.Parameters.AddWithValue("@ProductId", entity.ProductId);
            cmd.Parameters.AddWithValue("@Quantity", entity.Quantity);
            cmd.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice);
            cmd.Parameters.AddWithValue("@LineTotal", entity.LineTotal);
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

        private OrderItem MapReaderToOrderItem(SqlDataReader rdr)
        {
            return new OrderItem
            {
                Id = rdr.GetInt32(0),
                OrderId = rdr.GetInt32(1),
                ProductId = rdr.GetInt32(2),
                Quantity = rdr.GetInt32(3),
                UnitPrice = rdr.GetDecimal(4),
                LineTotal = rdr.IsDBNull(5) ? rdr.GetInt32(3) * rdr.GetDecimal(4) : rdr.GetDecimal(5)
            };
        }
    }
}