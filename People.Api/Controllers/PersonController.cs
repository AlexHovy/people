using People.Core.Constants;
using People.Models.Dtos;
using People.Core.Helpers;
using People.Models.Entities;
using People.Services.Queries;
using People.Services;
using People.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace People.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly PersonQuery _personQuery;
    private readonly IPersonService _personService;
    private readonly IEmailService _emailService;
    private readonly IFileService _fileService;
    private readonly SmtpSettingsDto smtpSettingsDto;

    public PersonController(
        ConfigService configService,
        PersonQuery personQuery,
        IPersonService personService,
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
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            };

            var newPersonDto = await _personService.AddPersonAsync(person);
            if (newPersonDto != null)
                await SendEmail("New Person", newPersonDto);

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

            var person = await _personQuery.GetById(personId);
            if (person == null) BadRequest("Person does not exist");

            string filePath = Path.Combine(DataFilePaths.ProfilePictures, fileDto.FileName);
            await _fileService.WriteBytesAsync(filePath, fileDto.FileBase64);

            await SendEmail("Updated Person Profile Picture", person);

            await _personService.UpdatePersonHasProfilePictureAsync(person.Id, true);

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
            if (updatedPersonDto != null)
                await SendEmail("Updated Person", updatedPersonDto);

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
            if (removedPersonDto != null)
                await SendEmail("Removed Person", removedPersonDto);

            string filePath = Path.Combine(DataFilePaths.ProfilePictures, $"{id}.png");
            await _fileService.DeleteAsync(filePath);

            return Ok(removedPersonDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private async Task SendEmail(string subject, PersonDto personDto)
    {
        var to = new List<string>();
        to.Add(smtpSettingsDto.FromAddress);

        string body = HtmlTableGeneratorHelper.GenerateHtmlTable<PersonDto>(personDto);
        
        await _emailService.SendAsync(to, subject, body);
    }
}
