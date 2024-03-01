using Microsoft.AspNetCore.Mvc;
using PresidentSite.Models.Data;
using PresidentSite.Models;



namespace PresidentElectionsOnline.Controllers;

[Route("api/voters/{lastName}")]
public class GetSpecVoters : Controller
{   

   [HttpGet]
    public ActionResult<IEnumerable<Ballot>> Index(string lastName)
    {
        using var context = new ElectorCounterContext();
        var voters = context.Voters.Where(p => p.Ballot == lastName).ToList();
        return View(voters);
    }
}