using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rsoi.Database.Models;

namespace rsoi.Database.NpgsqlContext
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Id).IsUnique();
            
            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedNever();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Age).IsRequired();
            builder.Property(p => p.Address).IsRequired();
            builder.Property(p => p.Work).IsRequired();
        }
    }
}