using Microsoft.AspNetCore.Mvc;
using PresidentSite.Models.Data;
using PresidentSite.Models;

namespace PresidentElectionsOnline.Controllers;

[Route("api/voters/{lastName}")]
public class GetSpecVotersController : Controller
{   
    private IRepository repository;
    
    public GetSpecVotersController(IRepository repository)
    {
        this.repository = repository;
    }   


    [HttpGet]
    public ActionResult<IEnumerable<Ballot>> Index(string lastName)
    {
        var voters = repository.GetVotersByBallots(lastName).ToList();
        return View(voters);
    }
}