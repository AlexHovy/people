using Api.Models;
using Api.Dtos;
using Microsoft.EntityFrameworkCore;
using Api.Services.Interfaces;

namespace Api.Services
{
    public class PersonService
    {
        private readonly IRepository<Person> _repo;

        public PersonService(IRepository<Person> repo)
        {
            _repo = repo;
        }

        public async Task AddPersonAsync(Person person)
        {
            await _repo.AddAsync(person);
        }

        public async Task UpdatePersonAsync(PersonDto personDto)
        {
            var person = await _repo.Query.FirstOrDefaultAsync(p => p.Id == personDto.Id);
            if (person != null)
            {
                person.Name = personDto.Name;
                person.Surname = personDto.Surname;
                person.Gender = personDto.Gender;
                person.Email = personDto.Email;
                person.MobileNumber = personDto.MobileNumber;
                person.AddressCity = personDto.AddressCity;
                person.AddressCountry = personDto.AddressCountry;
                person.ProfilePicture = personDto.ProfilePicture;
                person.UpdatedDateTime = DateTime.Now;

                await _repo.UpdateAsync(person);
            }
        }

        public async Task DeletePersonAsync(Guid id)
        {
            var person = await _repo.Query.FirstOrDefaultAsync(p => p.Id == id);
            if (person != null)
            {
                await _repo.RemoveAsync(person);
            }
        }
    }
}
