using Api.Dtos;
using Api.Services.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Queries;

public class CityQuery
{
    private readonly IRepository<City> _repo;

    public CityQuery(IRepository<City> repo)
    {
        _repo = repo;
    }

    public async Task<CityDto[]> Get()
    {
        var cities = await _repo.Query
                                .Include(x => x.Country)
                                .ToListAsync();
        return cities.Select(p => p.ToDto()).ToArray();
    }

    public async Task<CityDto> GetById(Guid id)
    {
        var city = await _repo.Query
                                .Include(x => x.Country)
                                .FirstOrDefaultAsync(x => x.Id == id);
        if (city == null) return null;

        return city.ToDto();
    }

    public async Task<CityDto[]> GetByCountryId(Guid countryId)
    {
        var cities = await _repo.Query
                            .Include(x => x.Country)
                            .Where(x => x.CountryId == countryId)
                            .ToListAsync();
        return cities.Select(p => p.ToDto()).ToArray();
    }
}