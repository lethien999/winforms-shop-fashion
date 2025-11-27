using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            using var cmd = new SqlCommand($"SELECT Id, Username, PasswordHash, Role FROM {TableName}", conn);
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
            using var cmd = new SqlCommand($"SELECT Id, Username, PasswordHash, Role FROM {TableName} WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read()) return MapReaderToUser(rdr);
            return null;
        }

        public void Insert(User entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"INSERT INTO {TableName} (Username, PasswordHash, Role) VALUES (@Username, @PasswordHash, @Role); SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@Username", entity.Username);
            cmd.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
            cmd.Parameters.AddWithValue("@Role", entity.Role);
            var id = Convert.ToInt32(cmd.ExecuteScalar());
            entity.Id = id;
        }

        public void Update(User entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"UPDATE {TableName} SET Username = @Username, PasswordHash = @PasswordHash, Role = @Role WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Username", entity.Username);
            cmd.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
            cmd.Parameters.AddWithValue("@Role", entity.Role);
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

        private User MapReaderToUser(SqlDataReader rdr)
        {
            return new User
            {
                Id = rdr.GetInt32(0),
                Username = rdr.GetString(1),
                PasswordHash = rdr.GetString(2),
                Role = rdr.GetString(3)
            };
        }
    }
}