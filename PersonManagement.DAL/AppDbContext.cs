using Microsoft.EntityFrameworkCore;
using PersonManagement.DAL.Entities;
using System.Collections.Generic;

namespace PersonManagement.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<Person> Persons => Set<Person>();
    public virtual DbSet<Address> Addresses => Set<Address>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.FirstName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(p => p.LastName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.HasOne(p => p.Address)
                  .WithOne()
                  .HasForeignKey<Address>(a => a.Id)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.City)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(a => a.AddressLine)
                  .IsRequired()
                  .HasMaxLength(200);
        });
    }
}


