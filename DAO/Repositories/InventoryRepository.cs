using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Data.Repositories
{
    public class InventoryRepository : RepositoryBase, IInventoryRepository
    {
        private const string TableName = "Inventory";

        public IEnumerable<Inventory> GetAll()
        {
            var list = new List<Inventory>();
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, ProductId, QuantityInStock, LastUpdated FROM {TableName}", conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) list.Add(MapReaderToInventory(rdr));
            return list;
        }

        public Inventory? GetById(int id)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, ProductId, QuantityInStock, LastUpdated FROM {TableName} WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read()) return MapReaderToInventory(rdr);
            return null;
        }

        public Inventory? GetByProductId(int productId)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, ProductId, QuantityInStock, LastUpdated FROM {TableName} WHERE ProductId=@ProductId", conn);
            cmd.Parameters.AddWithValue("@ProductId", productId);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read()) return MapReaderToInventory(rdr);
            return null;
        }

        public void Insert(Inventory entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"INSERT INTO {TableName} (ProductId, QuantityInStock, LastUpdated) VALUES (@ProductId, @QuantityInStock, @LastUpdated); SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@ProductId", entity.ProductId);
            cmd.Parameters.AddWithValue("@QuantityInStock", entity.QuantityInStock);
            cmd.Parameters.AddWithValue("@LastUpdated", entity.LastUpdated);
            entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void Update(Inventory entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"UPDATE {TableName} SET ProductId=@ProductId, QuantityInStock=@QuantityInStock, LastUpdated=@LastUpdated WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@ProductId", entity.ProductId);
            cmd.Parameters.AddWithValue("@QuantityInStock", entity.QuantityInStock);
            cmd.Parameters.AddWithValue("@LastUpdated", entity.LastUpdated);
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

        private Inventory MapReaderToInventory(SqlDataReader rdr)
        {
            return new Inventory
            {
                Id = rdr.GetInt32(0),
                ProductId = rdr.GetInt32(1),
                QuantityInStock = rdr.GetInt32(2),
                LastUpdated = rdr.IsDBNull(3) ? DateTime.Now : rdr.GetDateTime(3)
            };
        }
    }
}