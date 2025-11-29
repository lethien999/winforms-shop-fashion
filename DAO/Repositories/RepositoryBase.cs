using Microsoft.Data.SqlClient;

namespace WinFormsFashionShop.Data.Repositories
{
    /// <summary>
    /// Provides helper methods for repository implementations to create database connections and commands.
    /// </summary>
    public abstract class RepositoryBase
    {
        /// <summary>
        /// Creates and opens a SqlConnection using the configured connection string.
        /// Caller should use 'using' when using the returned connection.
        /// </summary>
        protected SqlConnection CreateOpenConnection()
        {
            var conn = new SqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
