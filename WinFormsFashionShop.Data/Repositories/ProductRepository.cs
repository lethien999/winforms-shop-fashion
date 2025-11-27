using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Data.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        private const string TableName = "Products";

        public IEnumerable<Product> GetAll()
        {
            var list = new List<Product>();
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, Name, CategoryId, Price, Sku, IsActive FROM {TableName}", conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) list.Add(MapReaderToProduct(rdr));
            return list;
        }

        public Product? GetById(int id)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, Name, CategoryId, Price, Sku, IsActive FROM {TableName} WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read()) return MapReaderToProduct(rdr);
            return null;
        }

        public void Insert(Product entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"INSERT INTO {TableName} (Name, CategoryId, Price, Sku, IsActive) VALUES (@Name, @CategoryId, @Price, @Sku, @IsActive); SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
            cmd.Parameters.AddWithValue("@Price", entity.Price);
            cmd.Parameters.AddWithValue("@Sku", entity.Sku);
            cmd.Parameters.AddWithValue("@IsActive", entity.IsActive);
            entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void Update(Product entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"UPDATE {TableName} SET Name=@Name, CategoryId=@CategoryId, Price=@Price, Sku=@Sku, IsActive=@IsActive WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
            cmd.Parameters.AddWithValue("@Price", entity.Price);
            cmd.Parameters.AddWithValue("@Sku", entity.Sku);
            cmd.Parameters.AddWithValue("@IsActive", entity.IsActive);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"DELETE FROM {TableName} WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        private Product MapReaderToProduct(SqlDataReader rdr)
        {
            return new Product
            {
                Id = rdr.GetInt32(0),
                Name = rdr.GetString(1),
                CategoryId = rdr.GetInt32(2),
                Price = rdr.GetDecimal(3),
                Sku = rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4),
                IsActive = !rdr.IsDBNull(5) && rdr.GetBoolean(5)
            };
        }
    }
}