using Api.Dtos;
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

    public PersonController(PersonQuery personQuery)
    {
        _personQuery = personQuery;
    }

    [HttpGet]
    public ActionResult<PersonDto[]> Get()
    {
        var people = _personQuery.Get();
        return Ok(people);
    }

    [Authorize]
    [HttpPost("CreatePerson")]
    public ActionResult<string> CreatePerson()
    {
        return Ok();
    }
}
