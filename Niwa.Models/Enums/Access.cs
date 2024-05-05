using System.ComponentModel.DataAnnotations;

namespace Niwa.Models.Enums;

public enum Access
{
    [Display(Name = "Private")] Private,
    [Display(Name = "Public")] Public,
    [Display(Name = "Link-Only")] LinkOnly
}