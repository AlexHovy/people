using People.Models.Dtos;
using People.Services.Queries;
using Microsoft.AspNetCore.Mvc;

namespace People.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CountryController : ControllerBase
{
    private readonly CountryQuery _countryQuery;

    public CountryController(CountryQuery countryQuery)
    {
        _countryQuery = countryQuery;
    }

    [HttpGet]
    public async Task<ActionResult<CountryDto[]>> Get()
    {
        try
        {
            var countries = await _countryQuery.Get();
            return Ok(countries);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CountryDto>> GetById(Guid id)
    {
        try
        {
            var country = await _countryQuery.GetById(id);
            return Ok(country);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
