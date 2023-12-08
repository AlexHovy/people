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
    public string AddressCity { get; set; }
    public string AddressCountry { get; set; }
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
            AddressCity = AddressCity,
            AddressCountry = AddressCountry,
            ProfilePicture = ProfilePicture,
            CreatedDateTime = CreatedDateTime,
            UpdatedDateTime = UpdatedDateTime
        };
    }
}
