using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using People.Models.Dtos;

namespace People.Models.Entities;

public class City
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    [Required]
    public Guid CountryId { get; set; }

    [ForeignKey("CountryId")]
    public virtual Country Country { get; set; }

    public CityDto ToDto()
    {
        return new CityDto
        {
            Id = Id,
            Name = Name,
            CountryId = Country.Id,
            County = Country?.Name
        };
    }
}
