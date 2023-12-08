using Microsoft.EntityFrameworkCore;
using Api.Models;
using Api.Services;

namespace Api.DbContexts;

public class PeopleContext : DbContext
{
    private protected string _connectionString { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Person> Persons { get; set; }

    public PeopleContext(ConfigService configService)
    {
        _connectionString = configService.GetConnectionString();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}
