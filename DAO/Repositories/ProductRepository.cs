using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
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
            using var cmd = new SqlCommand($"SELECT Id, ProductCode, Name, CategoryId, UnitPrice, Unit, IsActive, CreatedAt, UpdatedAt FROM {TableName}", conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) list.Add(MapReaderToProduct(rdr));
            return list;
        }

        public Product? GetById(int id)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, ProductCode, Name, CategoryId, UnitPrice, Unit, IsActive, CreatedAt, UpdatedAt FROM {TableName} WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read()) return MapReaderToProduct(rdr);
            return null;
        }

        public void Insert(Product entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"INSERT INTO {TableName} (ProductCode, Name, CategoryId, UnitPrice, Unit, IsActive, CreatedAt) VALUES (@ProductCode, @Name, @CategoryId, @UnitPrice, @Unit, @IsActive, @CreatedAt); SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@ProductCode", entity.ProductCode);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
            cmd.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice);
            cmd.Parameters.AddWithValue("@Unit", entity.Unit);
            cmd.Parameters.AddWithValue("@IsActive", entity.IsActive);
            cmd.Parameters.AddWithValue("@CreatedAt", entity.CreatedAt);
            entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void Update(Product entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"UPDATE {TableName} SET ProductCode=@ProductCode, Name=@Name, CategoryId=@CategoryId, UnitPrice=@UnitPrice, Unit=@Unit, IsActive=@IsActive, UpdatedAt=@UpdatedAt WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@ProductCode", entity.ProductCode);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
            cmd.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice);
            cmd.Parameters.AddWithValue("@Unit", entity.Unit);
            cmd.Parameters.AddWithValue("@IsActive", entity.IsActive);
            cmd.Parameters.AddWithValue("@UpdatedAt", (object?)entity.UpdatedAt ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            // Check if product exists in OrderItems before deleting
            using var conn = CreateOpenConnection();
            using var checkCmd = new SqlCommand("SELECT COUNT(*) FROM OrderItems WHERE ProductId = @ProductId", conn);
            checkCmd.Parameters.AddWithValue("@ProductId", id);
            var count = Convert.ToInt32(checkCmd.ExecuteScalar());
            
            if (count > 0)
            {
                throw new InvalidOperationException($"Cannot delete product with Id {id} because it exists in {count} order item(s). Use IsActive = false instead.");
            }
            
            using var deleteCmd = new SqlCommand($"DELETE FROM {TableName} WHERE Id = @Id", conn);
            deleteCmd.Parameters.AddWithValue("@Id", id);
            deleteCmd.ExecuteNonQuery();
        }

        private Product MapReaderToProduct(SqlDataReader rdr)
        {
            return new Product
            {
                Id = rdr.GetInt32(0),
                ProductCode = rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1),
                Name = rdr.GetString(2),
                CategoryId = rdr.GetInt32(3),
                UnitPrice = rdr.GetDecimal(4),
                Unit = rdr.IsDBNull(5) ? "cái" : rdr.GetString(5),
                IsActive = !rdr.IsDBNull(6) && rdr.GetBoolean(6),
                CreatedAt = rdr.IsDBNull(7) ? DateTime.Now : rdr.GetDateTime(7),
                UpdatedAt = rdr.IsDBNull(8) ? null : rdr.GetDateTime(8)
            };
        }
    }
}