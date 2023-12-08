using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Base
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}
