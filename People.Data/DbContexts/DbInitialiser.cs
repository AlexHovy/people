using People.Models.Entities;
using People.Core.Helpers;

namespace People.Data.DbContexts;

public static class DbInitialiser
{
    public static void Initialise(PeopleContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Users.Any())
            AddUsers(context);

        if (!context.Countries.Any())
            AddCountriesAndCities(context);

        context.SaveChanges();
    }

    private static void AddUsers(PeopleContext context)
    {
        var users = new User[]
        {
            new User { Username = "admin", PasswordHash = HashHelper.HashPassword("P@ssword123") }
        };
        context.Users.AddRange(users);
    }

    private static void AddCountriesAndCities(PeopleContext context)
    {
        var countries = GetCountriesWithCities();
        context.Countries.AddRange(countries);

        foreach (var country in countries)
        {
            context.Cities.AddRange(country.Cities);
        }
    }

    private static List<Country> GetCountriesWithCities()
    {
        var southAfricaId = Guid.NewGuid();
        var brazilId = Guid.NewGuid();
        var indiaId = Guid.NewGuid();

        var countries = new List<Country>
        {
            new Country { Id = southAfricaId, Name = "South Africa", Cities = GetSouthAfricaCities(southAfricaId) },
            new Country { Id = brazilId, Name = "Brazil", Cities = GetBrazilCities(brazilId) },
            new Country { Id = indiaId, Name = "India", Cities = GetIndiaCities(indiaId) }
        };

        return countries;
    }

    private static List<City> GetSouthAfricaCities(Guid countryId)
    {
        return new List<City>
        {
            new City { Id = Guid.NewGuid(), Name = "Johannesburg", CountryId = countryId },
            new City { Id = Guid.NewGuid(), Name = "Cape Town", CountryId = countryId },
            new City { Id = Guid.NewGuid(), Name = "Durban", CountryId = countryId },
            new City { Id = Guid.NewGuid(), Name = "Pretoria", CountryId = countryId },
            new City { Id = Guid.NewGuid(), Name = "Port Elizabeth", CountryId = countryId }
        };
    }

    private static List<City> GetBrazilCities(Guid countryId)
    {
        return new List<City>
        {
            new City { Id = Guid.NewGuid(), Name = "São Paulo", CountryId = countryId },
            new City { Id = Guid.NewGuid(), Name = "Rio de Janeiro", CountryId = countryId },
            new City { Id = Guid.NewGuid(), Name = "Brasília", CountryId = countryId },
            new City { Id = Guid.NewGuid(), Name = "Salvador", CountryId = countryId },
            new City { Id = Guid.NewGuid(), Name = "Fortaleza", CountryId = countryId }
        };
    }

    private static List<City> GetIndiaCities(Guid countryId)
    {
        return new List<City>
        {
            new City { Id = Guid.NewGuid(), Name = "Mumbai", CountryId = countryId },
            new City { Id = Guid.NewGuid(), Name = "Delhi", CountryId = countryId },
            new City { Id = Guid.NewGuid(), Name = "Bangalore", CountryId = countryId },
            new City { Id = Guid.NewGuid(), Name = "Kolkata", CountryId = countryId },
            new City { Id = Guid.NewGuid(), Name = "Chennai", CountryId = countryId }
        };
    }
}
