using System.ComponentModel.DataAnnotations;

namespace People.Core.Constants;

public enum Gender
{
    [Display(Name = "Other")]
    Other = 0,

    [Display(Name = "Male")]
    Male = 1,

    [Display(Name = "Female")]
    Female = 2
}