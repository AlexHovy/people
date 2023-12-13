using Api.Dtos;
using Api.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CityController : ControllerBase
{
    private readonly CityQuery _cityQuery;

    public CityController(CityQuery cityQuery)
    {
        _cityQuery = cityQuery;
    }

    [HttpGet]
    public async Task<ActionResult<CityDto[]>> Get()
    {
        try
        {
            var cities = await _cityQuery.Get();
            return Ok(cities);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CityDto>> GetById(Guid id)
    {
        try
        {
            var city = await _cityQuery.GetById(id);
            return Ok(city);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ByCountryId/{countryId}")]
    public async Task<ActionResult<CityDto>> GetByCountryId(Guid countryId)
    {
        try
        {
            var cities = await _cityQuery.GetByCountryId(countryId);
            return Ok(cities);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
