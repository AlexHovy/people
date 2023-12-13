using System.ComponentModel.DataAnnotations.Schema;
using Api.Constants;
using Api.Dtos;

namespace Api.Models;

public class Person : Base
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }

    public Guid CountryId { get; set; }

    [ForeignKey("CountryId")]
    public virtual Country Country { get; set; }

    public Guid CityId { get; set; }

    [ForeignKey("CityId")]
    public virtual City City { get; set; }
    
    public string ProfilePicture { get; set; }

    public PersonDto ToDto()
    {
        return new PersonDto
        {
            Id = Id,
            Name = Name,
            Surname = Surname,
            Gender = Gender,
            Email = Email,
            MobileNumber = MobileNumber,
            CountryId = CountryId,
            Country = Country?.Name,
            CityId = CityId,
            City = City?.Name,
            ProfilePicture = ProfilePicture,
            CreatedDateTime = CreatedDateTime,
            UpdatedDateTime = UpdatedDateTime
        };
    }
}
