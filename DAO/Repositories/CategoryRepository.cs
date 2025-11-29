using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Data.Repositories
{
    public class CategoryRepository : RepositoryBase, ICategoryRepository
    {
        private const string TableName = "Categories";

        public IEnumerable<Category> GetAll()
        {
            var list = new List<Category>();
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, CategoryName, Description, IsActive FROM {TableName}", conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) list.Add(MapReaderToCategory(rdr));
            return list;
        }

        public Category? GetById(int id)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, CategoryName, Description, IsActive FROM {TableName} WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read()) return MapReaderToCategory(rdr);
            return null;
        }

        public void Insert(Category entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"INSERT INTO {TableName} (CategoryName, Description, IsActive) VALUES (@CategoryName, @Description, @IsActive); SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@CategoryName", entity.CategoryName);
            cmd.Parameters.AddWithValue("@Description", (object?)entity.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsActive", entity.IsActive);
            entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void Update(Category entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"UPDATE {TableName} SET CategoryName=@CategoryName, Description=@Description, IsActive=@IsActive WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@CategoryName", entity.CategoryName);
            cmd.Parameters.AddWithValue("@Description", (object?)entity.Description ?? DBNull.Value);
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

        private Category MapReaderToCategory(SqlDataReader rdr)
        {
            return new Category
            {
                Id = rdr.GetInt32(0),
                CategoryName = rdr.GetString(1),
                Description = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                IsActive = !rdr.IsDBNull(3) && rdr.GetBoolean(3)
            };
        }
    }
}