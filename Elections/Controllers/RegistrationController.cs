using Microsoft.AspNetCore.Mvc;
using PresidentSite.Models;
using PresidentSite.Models.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace PresidentElectionsOnline.Controllers;

public class RegistrationController : Controller
{
    private IRepository repository;
    
    public RegistrationController(IRepository repository)
    {
        this.repository = repository;
    }   


    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public IActionResult Index(Voter voter)
    {
        if (!ModelState.IsValid)
        {
            string errorMessage = "";
            foreach (var item in ModelState)
            {
                if (item.Value.ValidationState == ModelValidationState.Invalid)
                {
                    var propertyInfo = typeof(Voter).GetProperty(item.Key);
                    var displayName = repository.DisplayAttribute(propertyInfo)?.Name ?? item.Key;
                    errorMessage += $"<br/>Error in {displayName} - ";
                    foreach (var error in item.Value.Errors)
                    {
                        errorMessage += $"{error.ErrorMessage}<br/>";
                    }
                }
            }
            return RedirectToAction("Error", "Registration", new { errorMessage });
        }

        var existingVoter = repository.FindVoter().FirstOrDefault(p => (p.Name == voter.Name)
                                            && (p.Surname == voter.Surname)
                                            && (p.Age == voter.Age));;
        if (existingVoter != null)
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
            repository.InsertVoter(newVoter);
            repository.Save();

            string message = $"{voter.Name} {voter.Surname}, You are now registered!";
            ViewBag.Message = message;
            return View("RegistrationResult");
        }
    }
    [HttpGet]
    public IActionResult Error(string errorMessage)
    {
        ViewBag.Message = errorMessage;
        return View("~/Views/Registration/Error.cshtml");
    }

}