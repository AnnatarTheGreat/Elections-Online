using Microsoft.AspNetCore.Mvc;
using PresidentSite.Models.Data;
using PresidentSite.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Text.Json;

namespace PresidentElectionsOnline.Controllers;

public class AuthorizationController : Controller
{
    private readonly ElectorCounterContext _electorCounterContext;
    public AuthorizationController(ElectorCounterContext electorCounterContext)
    {
        _electorCounterContext = electorCounterContext;
    }
    [HttpGet]
    public IActionResult Index()
    {
        
        if (TempData["Message"] != null)
        {
            ViewBag.Message = TempData["Message"];
        }
        return View();
    }


    [HttpPost]
    public IActionResult Index(Voter voter)
    {
        var context = _electorCounterContext;
        var existingVoterOrNot = context.Voters.FirstOrDefault(p => (p.Name == voter.Name)
                                                                && (p.Surname == voter.Surname));
        if (existingVoterOrNot != null)
        {
            CreateCookie(existingVoterOrNot);
            string message = $"{voter.Name}, welcome!";
            ViewBag.Message = message;
            return RedirectToAction("AuthorizationResult", "Authorization", new { message });
        }
        else
        {
            string errorMessage = "The data is incorrect or the user is not registered!";
            ViewBag.Message = errorMessage;
            return RedirectToAction("Error", "Authorization", new { errorMessage });
        }

    }

    [HttpGet]
    public IActionResult Error(string errorMessage)
    {
        ViewBag.Message = errorMessage;
        return View("~/Views/Authorization/Error.cshtml");
    }

    [HttpGet]
    public IActionResult AuthorizationResult(string message)
    {
        ViewBag.Message = message;
        return View("~/Views/Authorization/AuthorizationResult.cshtml");
        
    }

    public void CreateCookie(Voter voter)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, voter.Name),
            new Claim(ClaimTypes.Surname, voter.Surname),
            new Claim(ClaimTypes.Role, "user")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true
        };
        HttpContext.Session.SetString("CurrentVoter", JsonSerializer.Serialize(voter));
        HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }

}