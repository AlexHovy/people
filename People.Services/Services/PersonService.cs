using People.Models.Entities;
using People.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using People.Services.Interfaces;

namespace People.Services
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> _repo;

        public PersonService(IRepository<Person> repo)
        {
            _repo = repo;
        }

        public async Task<PersonDto> AddPersonAsync(Person person)
        {
            person.Id = Guid.NewGuid();
            await _repo.AddAsync(person);
            return person.ToDto();
        }

        public async Task<PersonDto> UpdatePersonAsync(PersonDto personDto)
        {
            var person = await _repo.Query.FirstOrDefaultAsync(p => p.Id == personDto.Id);
            if (person != null)
            {
                person.Name = personDto.Name;
                person.Surname = personDto.Surname;
                person.Gender = personDto.Gender;
                person.Email = personDto.Email;
                person.MobileNumber = personDto.MobileNumber;
                person.CountryId = personDto.CountryId;
                person.CityId = personDto.CityId;
                person.UpdatedDateTime = DateTime.Now;

                await _repo.UpdateAsync(person);
                return person.ToDto();
            }
            return null;
        }

        public async Task<bool> UpdatePersonHasProfilePictureAsync(Guid personId, bool hasProfilePicture)
        {
            var person = await _repo.Query.FirstOrDefaultAsync(p => p.Id == personId);
            if (person != null)
            {
                person.HasProfilePicture = hasProfilePicture;
                person.UpdatedDateTime = DateTime.Now;

                await _repo.UpdateAsync(person);
                return true;
            }
            return false;
        }

        public async Task<PersonDto> DeletePersonAsync(Guid id)
        {
            var person = await _repo.Query.FirstOrDefaultAsync(p => p.Id == id);
            if (person != null)
            {
                await _repo.RemoveAsync(person);
                return person.ToDto();
            }
            return null;
        }
    }
}
