using People.Models.Entities;
using People.Models.Dtos;

namespace People.Services.Interfaces;

public interface IPersonService
{
    Task<PersonDto> AddPersonAsync(Person person);
    Task<PersonDto> UpdatePersonAsync(PersonDto personDto);
    Task<bool> UpdatePersonHasProfilePictureAsync(Guid personId, bool hasProfilePicture);
    Task<PersonDto> DeletePersonAsync(Guid id);
}
