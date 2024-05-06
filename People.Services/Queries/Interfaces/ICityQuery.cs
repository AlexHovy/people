using People.Models.Dtos;

namespace People.Services.Queries.Interfaces;

public interface ICityQuery
{
    Task<CityDto[]> Get();
    Task<CityDto> GetById(Guid id);
    Task<CityDto[]> GetByCountryId(Guid countryId);
}
