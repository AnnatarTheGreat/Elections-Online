using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PresidentSite.Models;

public class Voter : IdentityUser
{
    public static Voter NotFound = new Voter();

    public string? Ballot {get; set;}

}