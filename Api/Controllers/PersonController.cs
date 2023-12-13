using Api.Dtos;
using Api.Models;
using Api.Queries;
using Api.Services;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly PersonQuery _personQuery;
    private readonly PersonService _personService;
    private readonly IEmailService _emailService;
    private readonly SmtpSettingsDto smtpSettingsDto;

    public PersonController(
        ConfigService configService,
        PersonQuery personQuery,
        PersonService personService,
        IEmailService emailService
    )
    {
        _personQuery = personQuery;
        _personService = personService;
        _emailService = emailService;

        smtpSettingsDto = configService.GetSmtpSettings();
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
                CountryId = personDto.CountryId,
                CityId = personDto.CityId,
                ProfilePicture = personDto.ProfilePicture,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            };
            
            var newPersonDto = await _personService.AddPersonAsync(person);
            if (newPersonDto != null)
            {
                var to = new List<string>();
                to.Add(smtpSettingsDto.FromAddress);
                await _emailService.SendAsync(to, "New Person", $"{newPersonDto.Name} {newPersonDto.Surname} ({newPersonDto.Email}) was added.");
            }

            return Ok(newPersonDto);
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
            var updatedPersonDto = await _personService.UpdatePersonAsync(personDto);
            return Ok(updatedPersonDto);
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
            var removedPersonDto = await _personService.DeletePersonAsync(id);
            return Ok(removedPersonDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
