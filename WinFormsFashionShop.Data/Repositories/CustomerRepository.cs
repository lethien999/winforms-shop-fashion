using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Data.Repositories
{
    public class CustomerRepository : RepositoryBase, ICustomerRepository
    {
        private const string TableName = "Customers";

        public IEnumerable<Customer> GetAll()
        {
            var list = new List<Customer>();
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, FullName, Phone, Email, Address FROM {TableName}", conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) list.Add(MapReaderToCustomer(rdr));
            return list;
        }

        public Customer? GetById(int id)
        {
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"SELECT Id, FullName, Phone, Email, Address FROM {TableName} WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read()) return MapReaderToCustomer(rdr);
            return null;
        }

        public void Insert(Customer entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"INSERT INTO {TableName} (FullName, Phone, Email, Address) VALUES (@FullName, @Phone, @Email, @Address); SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@FullName", entity.FullName);
            cmd.Parameters.AddWithValue("@Phone", (object)entity.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object)entity.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Address", (object)entity.Address ?? DBNull.Value);
            entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void Update(Customer entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = CreateOpenConnection();
            using var cmd = new SqlCommand($"UPDATE {TableName} SET FullName=@FullName, Phone=@Phone, Email=@Email, Address=@Address WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@FullName", entity.FullName);
            cmd.Parameters.AddWithValue("@Phone", (object)entity.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object)entity.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Address", (object)entity.Address ?? DBNull.Value);
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

        private Customer MapReaderToCustomer(SqlDataReader rdr)
        {
            return new Customer
            {
                Id = rdr.GetInt32(0),
                FullName = rdr.GetString(1),
                Phone = rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2),
                Email = rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3),
                Address = rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4)
            };
        }
    }
}