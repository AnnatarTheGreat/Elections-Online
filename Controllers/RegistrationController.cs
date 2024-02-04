using Microsoft.AspNetCore.Mvc;
using PresidentSite.Models;
using PresidentSite.Models.Data;

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;




namespace PresidentElectionsOnline.Controllers;

public class RegistrationController : Controller
{
    private IVoter voter;
    private readonly IConfiguration configuration;
    public RegistrationController(IVoter voter)
    {
        this.voter = voter;
        
    }

    [HttpGet]
    public IActionResult Index() => View();

   [HttpGet]
   
   public IActionResult Error (string errorMessage)
   {
    ViewBag.Message = errorMessage;
    return View("~/Views/Registration/Error.cshtml");
   }

    [HttpPost]
    public IActionResult Index (Voter voter)
    {
        voter.Ballot = null;
        if (!ModelState.IsValid) 
        {
            string errorMessage = "";
            foreach (var item in ModelState)
            {
                if (item.Value.ValidationState == ModelValidationState.Invalid)
                {
                    var propertyInfo =typeof(Voter).GetProperty(item.Key);
                    var displayAttribute = (DisplayAttribute)propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
                    var displayName = displayAttribute?.Name ?? item.Key;
                    errorMessage += $"<br/>Error in {displayName} - ";
                    foreach (var error in item.Value.Errors)
                    {
                        errorMessage += $"{error.ErrorMessage}<br/>";
                    }
                }
            }
            return RedirectToAction("Error", "Registration", new { errorMessage });
        
        }

        using var context = new ElectorCounterContext(configuration);
        var existingVoterOrNot = context.Voters.FirstOrDefault(p => (p.Name == voter.Name)
                                                                && (p.Surname == voter.Surname)
                                                                && (p.Age==voter.Age));
        if (existingVoterOrNot != null) 
        {
            string errorMessage = "This voter is already exists!";
            ViewBag.Message = errorMessage;
            return RedirectToAction("Error", "Registration", new { errorMessage });
        }
        else
        {
            var newVoter = new Voter
            {
                Name = voter.Name,
                Surname = voter.Surname,
                Age = voter.Age
            };
            context.Voters.Add(newVoter);
            context.SaveChanges();

            string message = $"{voter.Name} {voter.Surname}, You are now registered!";
            ViewBag.Message = message;
            return View ("RegistrationResult");
        }
    }


}