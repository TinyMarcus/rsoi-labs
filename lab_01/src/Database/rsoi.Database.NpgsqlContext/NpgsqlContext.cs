
using Microsoft.EntityFrameworkCore;
using rsoi.Database.Models;

namespace rsoi.Database.NpgsqlContext
{
    public class NpgSqlContext : DbContext
    {
        public DbSet<Person>? Persons { get; set; }

        public NpgSqlContext()
        {
            
        }

        public NpgSqlContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
        }
    }
}