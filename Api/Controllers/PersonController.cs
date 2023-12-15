using Api.Constants;
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
    private readonly IFileService _fileService;
    private readonly SmtpSettingsDto smtpSettingsDto;

    public PersonController(
        ConfigService configService,
        PersonQuery personQuery,
        PersonService personService,
        IEmailService emailService,
        IFileService fileService
    )
    {
        _personQuery = personQuery;
        _personService = personService;
        _emailService = emailService;
        _fileService = fileService;

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

    [HttpGet("GetProfilePicture/{personId}")]
    public async Task<ActionResult<string>> GetProfilePicture(Guid personId)
    {
        try
        {
            var person = await _personQuery.GetById(personId);
            if (person == null) BadRequest("Person does not exist");

            string fileName = $"{personId}.png";
            string filePath = Path.Combine(DataFilePaths.ProfilePictures, fileName);
            string base64 = await _fileService.ConvertToBase64Async(filePath);

            var fileDto = new FileDto
            {
                FileName = fileName,
                FileBase64 = base64
            };

            return Ok(fileDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Search/{query}")]
    public async Task<ActionResult<PersonDto[]>> Search(string query)
    {
        try
        {
            var people = await _personQuery.Find(query);
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
                HasProfilePicture = personDto.HasProfilePicture,
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
    [HttpPost("UploadProfilePicture/{personId}")]
    public async Task<ActionResult<string>> UploadProfilePicture(Guid personId, FileDto fileDto)
    {
        try
        {
            if (string.IsNullOrEmpty(fileDto.FileName) || string.IsNullOrEmpty(fileDto.FileBase64))
                return BadRequest("Invalid file data");

            string filePath = Path.Combine(DataFilePaths.ProfilePictures, fileDto.FileName);
            await _fileService.WriteBytesAsync(filePath, fileDto.FileBase64);

            await _personService.UpdatePersonHasProfilePictureAsync(personId, true);

            return Ok(fileDto);
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

            string filePath = Path.Combine(DataFilePaths.ProfilePictures, $"{id}.png");
            await _fileService.DeleteAsync(filePath);

            return Ok(removedPersonDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
