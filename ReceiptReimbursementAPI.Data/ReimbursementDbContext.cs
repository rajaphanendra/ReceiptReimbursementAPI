using Microsoft.EntityFrameworkCore;
using ReceiptReimbursementAPI.Model;
using ReceiptReimbursementAPI.Model.Entities;

namespace ReceiptReimbursementAPI.Data
{
    public class ReimbursementDbContext : DbContext
    {
        public ReimbursementDbContext(DbContextOptions<ReimbursementDbContext> options)
            : base(options)
        {
        }

        public DbSet<ReceiptRequest> ReceiptRequests { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReceiptRequest>().Property(r => r.Amount).HasPrecision(18, 2);
            base.OnModelCreating(modelBuilder);
        }
    }
}
