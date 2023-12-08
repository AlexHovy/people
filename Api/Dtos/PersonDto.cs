using Api.Constants;

namespace Api.Dtos;

public class PersonDto : BaseDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
    public string AddressCity { get; set; }
    public string AddressCountry { get; set; }
    public string ProfilePicture { get; set; }
}