using Microsoft.AspNetCore.Mvc;
using PresidentSite.Models.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PresidentSite.Models;
using System.Security.Claims;


using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

using System.Text.Json;


namespace PresidentElectionsOnline.Controllers;

public class Authorization : Controller
{
    private IVoter voter;
    private readonly IConfiguration configuration;

    public Authorization(IVoter voter)
    {
        this.voter = voter;
    }

    [HttpGet]
    public IActionResult Index() 
    {
    
     return   View();
    }
    [HttpGet]
   
   public IActionResult Error (string errorMessage)
   {
    ViewBag.Message = errorMessage;
    return View("~/Views/Authorization/Error.cshtml");
   }

   [HttpGet]
   
   public IActionResult AuthorizationResult (string message)
   {
    ViewBag.Message = message;
    return View("~/Views/Authorization/AuthorizationResult.cshtml");
   }

    [HttpPost]
   public IActionResult Index(Voter voter)
    {
        using var context = new ElectorCounterContext(configuration);
        var existingVoterOrNot = context.Voters.FirstOrDefault(p => (p.Name == voter.Name)
                                                                && (p.Surname == voter.Surname));
        if (existingVoterOrNot != null) 
        {
            CreateCookie(existingVoterOrNot);
            string message = $"{voter.Name}, добро пожаловать!";
            ViewBag.Message = message;
            return RedirectToAction("AuthorizationResult", "Authorization", new {message});
        }
        else 
        {
            string errorMessage ="Неверно указаны данные или пользователь не зарегистрирован!";
            ViewBag.Message = errorMessage;
            return RedirectToAction("Error", "Authorization", new { errorMessage }); 
        }


    }

    public void CreateCookie (Voter voter)
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




    /*public void CreatingToken(Voter voter)
    {
        var claims = new List<Claim> 
        {
            new Claim(ClaimTypes.Name, voter.Name), 
            new Claim(ClaimTypes.Surname, voter.Surname),
            new Claim(ClaimTypes.Role, "user")
        };
        var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build;  
        var configurationRoot = configuration();
        var secretKey = configurationRoot["JwtSettings:SecretKey"];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var token = new JwtSecurityToken(
            issuer: "president_elections",
            audience: "voter",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        HttpContext.Response.Cookies.Append("accessToken", tokenString, new CookieOptions
        {
            HttpOnly = true
        } );

    }*/
}