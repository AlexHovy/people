using Api.Models;
using Api.Helpers;

namespace Api.DbContexts;

public static class DbInitialiser
{
    public static void Initialize(PeopleContext context)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any()) return;

        var users = new User[]
        {
            new User { Username = "admin", PasswordHash = HashHelper.HashPassword("P@ssword123") }
        };
        context.Users.AddRange(users);

        context.SaveChanges();
    }
}
