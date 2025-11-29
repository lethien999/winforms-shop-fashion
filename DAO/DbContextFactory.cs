using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WinFormsFashionShop.Data
{
    /// <summary>
    /// Factory for creating DbContext during design-time (for Migrations).
    /// This is used by EF Core tools when running migrations.
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(DatabaseConfig.ConnectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}

