using People.Models.Dtos;

namespace People.Services.Queries.Interfaces;

public interface IPersonQuery
{
    Task<PersonDto[]> Get();
    Task<PersonDto[]> Find(string query);
    Task<PersonDto> GetById(Guid id);
}
