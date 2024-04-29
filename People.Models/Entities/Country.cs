using People.Models.Dtos;

namespace People.Models.Entities;

public class Country
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<City> Cities { get; set; }

    public CountryDto ToDto()
    {
        return new CountryDto
        {
            Id = Id,
            Name = Name
        };
    }
}
