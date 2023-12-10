namespace Api.Dtos;

public class CityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CountryId { get; set; }
    public string County { get; set; }
}