namespace WinFormsFashionShop.Data
{
    /// <summary>
    /// Database configuration template.
    /// Copy this file to DatabaseConfig.cs and update with your actual connection string.
    /// IMPORTANT: DO NOT commit DatabaseConfig.cs with real credentials to version control.
    /// </summary>
    public static class DatabaseConfig
    {
        // SQL Server connection string with SQL authentication
        // TODO: Replace with your actual connection string before running the application
        // Example: "Data Source=YOUR_SERVER;Initial Catalog=WinFormsFashionShopDb;User ID=YOUR_USER;Password=YOUR_PASSWORD;Trust Server Certificate=True"
        public static string ConnectionString { get; set; } = "Data Source=YOUR_SERVER;Initial Catalog=WinFormsFashionShopDb;Persist Security Info=True;User ID=YOUR_USER;Password=YOUR_PASSWORD;Trust Server Certificate=True";
    }
}

