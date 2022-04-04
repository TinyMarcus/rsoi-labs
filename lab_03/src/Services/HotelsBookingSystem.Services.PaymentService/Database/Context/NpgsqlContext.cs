#nullable enable
using HotelsBookingSystem.Services.PaymentService.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelsBookingSystem.Services.LoyaltyService.Database.Context
{
    public class NpgsqlContext : DbContext
    {
        public DbSet<Payment>? Payments { get; set; }

        public NpgsqlContext()
        {
            
        }

        public NpgsqlContext(DbContextOptions options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.HasDefaultSchema("payments");
        }
    }
}