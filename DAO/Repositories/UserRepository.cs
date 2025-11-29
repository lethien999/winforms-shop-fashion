using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Data.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private const string TableName = "Users";

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, Username, PasswordHash, FullName, Role, IsActive, CreatedAt, UpdatedAt FROM {TableName}", conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                users.Add(MapReaderToUser(rdr));
            }
            return users;
        }

        public User? GetById(int id)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, Username, PasswordHash, FullName, Role, IsActive, CreatedAt, UpdatedAt FROM {TableName} WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read()) return MapReaderToUser(rdr);
            return null;
        }

        /// <summary>
        /// Gets a user by username (case-insensitive search).
        /// </summary>
        public User? GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, Username, PasswordHash, FullName, Role, IsActive, CreatedAt, UpdatedAt FROM {TableName} WHERE LOWER(Username) = LOWER(@Username)", conn);
            cmd.Parameters.AddWithValue("@Username", username);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read()) return MapReaderToUser(rdr);
            return null;
        }

        public void Insert(User entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"INSERT INTO {TableName} (Username, PasswordHash, FullName, Role, IsActive, CreatedAt) VALUES (@Username, @PasswordHash, @FullName, @Role, @IsActive, @CreatedAt); SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@Username", entity.Username);
            cmd.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
            cmd.Parameters.AddWithValue("@FullName", entity.FullName);
            cmd.Parameters.AddWithValue("@Role", entity.Role);
            cmd.Parameters.AddWithValue("@IsActive", entity.IsActive);
            cmd.Parameters.AddWithValue("@CreatedAt", entity.CreatedAt);
            var id = Convert.ToInt32(cmd.ExecuteScalar());
            entity.Id = id;
        }

        public void Update(User entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"UPDATE {TableName} SET Username = @Username, PasswordHash = @PasswordHash, FullName = @FullName, Role = @Role, IsActive = @IsActive, UpdatedAt = @UpdatedAt WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Username", entity.Username);
            cmd.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
            cmd.Parameters.AddWithValue("@FullName", entity.FullName);
            cmd.Parameters.AddWithValue("@Role", entity.Role);
            cmd.Parameters.AddWithValue("@IsActive", entity.IsActive);
            cmd.Parameters.AddWithValue("@UpdatedAt", (object?)entity.UpdatedAt ?? DBNull.Value);
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

        public IEnumerable<User> GetByRole(string role)
        {
            var users = new List<User>();
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, Username, PasswordHash, FullName, Role, IsActive, CreatedAt, UpdatedAt FROM {TableName} WHERE Role = @Role", conn);
            cmd.Parameters.AddWithValue("@Role", role);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                users.Add(MapReaderToUser(rdr));
            }
            return users;
        }

        public IEnumerable<User> GetActiveUsers()
        {
            var users = new List<User>();
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, Username, PasswordHash, FullName, Role, IsActive, CreatedAt, UpdatedAt FROM {TableName} WHERE IsActive = 1", conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                users.Add(MapReaderToUser(rdr));
            }
            return users;
        }

        private User MapReaderToUser(SqlDataReader rdr)
        {
            return new User
            {
                Id = rdr.GetInt32(0),
                Username = rdr.GetString(1),
                PasswordHash = rdr.GetString(2),
                FullName = rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3),
                Role = rdr.GetString(4),
                IsActive = !rdr.IsDBNull(5) && rdr.GetBoolean(5),
                CreatedAt = rdr.IsDBNull(6) ? DateTime.Now : rdr.GetDateTime(6),
                UpdatedAt = rdr.IsDBNull(7) ? null : rdr.GetDateTime(7)
            };
        }
    }
}