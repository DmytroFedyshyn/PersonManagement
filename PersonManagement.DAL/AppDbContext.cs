using Microsoft.EntityFrameworkCore;
using PersonManagement.DAL.Entities;
using System.Collections.Generic;

namespace PersonManagement.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<Person> Persons => Set<Person>();
    public virtual DbSet<Address> Addresses => Set<Address>();
}
