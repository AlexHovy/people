using People.Models.Dtos;

namespace People.Services.Queries.Interfaces;

public interface ICountryQuery
{
    Task<CountryDto[]> Get();
    Task<CountryDto> GetById(Guid id);
}
