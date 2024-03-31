using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PresidentSite.Models;

public class Voter : IdentityUser
{
    public static Voter NotFound = new Voter();

  /*  [Required]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must have more than 3 symbols!")]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name must contain only letters")]
    [Display(Name = "Name")]
    public override string UserName {get; set;} =null!;*/


 /*   [Required(ErrorMessage = "Surname must have more than 0 symbols!")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Surname must have more than 3 symbols!")]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Surname must contain only letters")]
    [Display(Name = "Surname")]
    public string Surname { get; set; } =null!;


    [Required(ErrorMessage = "Age must have more than 0 symbols!")]
    [Range(18, int.MaxValue, ErrorMessage ="You must me older than 17 to take part in elections!")]
    public int Age { get; set; }   
*/

    public string? Ballot {get; set;}

}