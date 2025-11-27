using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            using var cmd = new SqlCommand($"SELECT Id, ProductId, QuantityOnHand FROM {TableName}", conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) list.Add(MapReaderToInventory(rdr));
            return list;
        }

        public Inventory? GetById(int id)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, ProductId, QuantityOnHand FROM {TableName} WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read()) return MapReaderToInventory(rdr);
            return null;
        }

        public void Insert(Inventory entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"INSERT INTO {TableName} (ProductId, QuantityOnHand) VALUES (@ProductId, @QuantityOnHand); SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@ProductId", entity.ProductId);
            cmd.Parameters.AddWithValue("@QuantityOnHand", entity.QuantityOnHand);
            entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void Update(Inventory entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"UPDATE {TableName} SET ProductId=@ProductId, QuantityOnHand=@QuantityOnHand WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@ProductId", entity.ProductId);
            cmd.Parameters.AddWithValue("@QuantityOnHand", entity.QuantityOnHand);
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
                QuantityOnHand = rdr.GetInt32(2)
            };
        }
    }
}