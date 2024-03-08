using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;


namespace PresidentElectionsOnline.Controllers;


public class LogOutController : Controller
{
    public IActionResult Index()
    {
        var currentVoterJson = HttpContext.Session.GetString("CurrentVoter");
        if (currentVoterJson != null)
        {
            HttpContext.Session.Remove("CurrentVoter");
        }
        return RedirectToAction("Index", "Home");
    }
}









