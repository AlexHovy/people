using Api.Dtos;
using Api.Models;
using Api.Queries;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly PersonQuery _personQuery;
    private readonly PersonService _personService;

    public PersonController(
        PersonQuery personQuery,
        PersonService personService
    )
    {
        _personQuery = personQuery;
        _personService = personService;
    }

    [HttpGet]
    public async Task<ActionResult<PersonDto[]>> Get()
    {
        try
        {
            var people = await _personQuery.Get();
            return Ok(people);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<string>> Create(PersonDto personDto)
    {
        try
        {
            var person = new Person
            {
                Name = personDto.Name,
                Surname = personDto.Surname,
                Gender = personDto.Gender,
                Email = personDto.Email,
                MobileNumber = personDto.MobileNumber,
                AddressCity = personDto.AddressCity,
                AddressCountry = personDto.AddressCountry,
                ProfilePicture = personDto.ProfilePicture,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            };
            await _personService.AddPersonAsync(person);
            return Ok("Person created successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> Update(PersonDto personDto)
    {
        try
        {
            await _personService.UpdatePersonAsync(personDto);
            return Ok("Person updated successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _personService.DeletePersonAsync(id);
            return Ok("Person deleted successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
