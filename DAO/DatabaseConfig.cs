namespace WinFormsFashionShop.Data
{
    /// <summary>
    /// Database configuration. 
    /// IMPORTANT: Update this connection string with your actual SQL Server credentials.
    /// DO NOT commit real credentials to version control.
    /// See DatabaseConfig.example.cs for template.
    /// </summary>
    public static class DatabaseConfig
    {
        // SQL Server connection string with SQL authentication
        // TODO: Replace with your actual connection string before running the application
        // Example: "Data Source=YOUR_SERVER;Initial Catalog=WinFormsFashionShopDb;User ID=YOUR_USER;Password=YOUR_PASSWORD;Trust Server Certificate=True"
        public static string ConnectionString { get; set; } = "Data Source=THIEN;Initial Catalog=WinFormsFashionShopDb;Persist Security Info=True;User ID=thien;Password=1909;Trust Server Certificate=True";
    }
}