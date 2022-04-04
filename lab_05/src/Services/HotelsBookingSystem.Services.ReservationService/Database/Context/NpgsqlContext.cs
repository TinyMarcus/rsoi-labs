#nullable enable
using HotelsBookingSystem.Services.ReservationService.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelsBookingSystem.Services.ReservationService.Database.Context
{
    public class NpgsqlContext : DbContext
    {
        public DbSet<Reservation>? Reservations { get; set; }
        public DbSet<Hotel>? Hotels { get; set; }

        public NpgsqlContext()
        {
            
        }

        public NpgsqlContext(DbContextOptions options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.HasDefaultSchema("reservations");
        }
    }
}