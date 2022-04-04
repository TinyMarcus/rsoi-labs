#nullable enable
using HotelsBookingSystem.Services.LoyaltyService.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelsBookingSystem.Services.LoyaltyService.Database.Context
{
    public class NpgsqlContext : DbContext
    {
        public DbSet<Loyalty>? Loyalties { get; set; }

        public NpgsqlContext()
        {
            
        }

        public NpgsqlContext(DbContextOptions options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.HasDefaultSchema("loyalties");
        }
    }
}