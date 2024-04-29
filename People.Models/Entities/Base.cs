using System.ComponentModel.DataAnnotations;

namespace People.Models.Entities;

public class Base
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}
