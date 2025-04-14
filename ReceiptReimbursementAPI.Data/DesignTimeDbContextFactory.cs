using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ReceiptReimbursementAPI.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReimbursementDbContext>
    {
        public ReimbursementDbContext CreateDbContext(string[] args)
        {
            // Build configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ReimbursementDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new ReimbursementDbContext(optionsBuilder.Options);
        }
    }
}