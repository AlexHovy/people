using Api.DbContexts;
using Api.Dtos;

namespace Api.Queries;

public class PersonQuery
{
    private readonly PeopleContext _context;

    public PersonQuery(PeopleContext context)
    {
        _context = context;
    }

    public PersonDto[] Get()
    {
        return _context.Persons
            .Select(x => x.ToDto())
            .ToArray();
    }

    public PersonDto[] GetById(Guid id)
    {
        return _context.Persons
            .Where(x => x.Id == id)
            .Select(x => x.ToDto())
            .ToArray();
    }
}