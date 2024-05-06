using People.Models.Dtos;
using People.Services.Queries.Interfaces;
using People.Services.Services.Interfaces;
using People.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace People.Services.Queries;

public class CountryQuery : ICountryQuery
{
    private readonly IRepository<Country> _repo;

    public CountryQuery(IRepository<Country> repo)
    {
        _repo = repo;
    }

    public async Task<CountryDto[]> Get()
    {
        var countries = await _repo.GetAllAsync();
        return countries.Select(p => p.ToDto()).ToArray();
    }

    public async Task<CountryDto> GetById(Guid id)
    {
        var country = await _repo.Query.FirstOrDefaultAsync(x => x.Id == id);
        if (country == null) return null;

        return country.ToDto();
    }
}