using People.Models.Dtos;
using People.Services.Queries.Interfaces;
using People.Services.Services.Interfaces;
using People.Models.Entities;
using Microsoft.EntityFrameworkCore;
using People.Core.Extensions;
using People.Core.Constants;

namespace People.Services.Queries;

public class PersonQuery : IPersonQuery
{
    private readonly IRepository<Person> _repo;

    public PersonQuery(IRepository<Person> repo)
    {
        _repo = repo;
    }

    public async Task<PersonDto[]> Get()
    {
        var persons = await _repo.Query
                                    .Include(x => x.Country)
                                    .Include(x => x.City)
                                    .ToListAsync();
        return persons.Select(p => p.ToDto()).ToArray();
    }

    public async Task<PersonDto[]> Find(string query)
    {
        var persons = await _repo.Query
                                    .Include(x => x.Country)
                                    .Include(x => x.City)
                                    .Where(x =>
                                        x.Name.ToLower().Contains(query.ToLower())
                                        || x.Surname.ToLower().Contains(query.ToLower())
                                        || x.Email.ToLower().Contains(query.ToLower())
                                        || x.MobileNumber.ToLower().Contains(query.ToLower())
                                        || (x.Gender == Gender.Other && Gender.Other.GetDisplayName().ToLower().Contains(query.ToLower()))
                                        || (x.Gender == Gender.Male && Gender.Male.GetDisplayName().ToLower().Contains(query.ToLower()))
                                        || (x.Gender == Gender.Female && Gender.Female.GetDisplayName().ToLower().Contains(query.ToLower()))
                                    )
                                    .ToListAsync();
        return persons.Select(p => p.ToDto()).ToArray();
    }

    public async Task<PersonDto> GetById(Guid id)
    {
        var person = await _repo.Query
                                .Include(x => x.Country)
                                .Include(x => x.City)
                                .FirstOrDefaultAsync(x => x.Id == id);
        if (person == null) return null;

        return person.ToDto();
    }
}