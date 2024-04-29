using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using People.Models.Entities;
using People.Core.Constants;

namespace People.Data.DbContexts;

public class PeopleContext : DbContext
{
    private protected string _connectionString { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }

    public PeopleContext(IConfiguration config)
    {
        _connectionString = config.GetConnectionString(ConfigKeys.ConnectionStringPeople);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne(p => p.City)
            .WithMany()
            .HasForeignKey(p => p.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Person>()
            .HasOne(p => p.Country)
            .WithMany()
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
