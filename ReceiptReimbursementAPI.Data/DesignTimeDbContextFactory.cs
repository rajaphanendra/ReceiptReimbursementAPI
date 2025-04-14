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
            // Load config manually since this doesn't run through Program.cs
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Path to where you're running EF command
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ReimbursementDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new ReimbursementDbContext(optionsBuilder.Options);
        }
    }
}