using Api.Constants;

namespace Api.Dtos;

public class PersonDto : BaseDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
    public Guid CountryId { get; set; }
    public string? Country { get; set; }
    public Guid CityId { get; set; }
    public string? City { get; set; }
    public string ProfilePicture { get; set; }
}