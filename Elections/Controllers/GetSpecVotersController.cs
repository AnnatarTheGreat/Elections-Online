using Microsoft.AspNetCore.Mvc;
using PresidentSite.Models;

namespace PresidentElectionsOnline.Controllers;

[Route("api/voters/{lastName}")]
public class GetSpecVotersController : ControllerBase
{   
    private IRepository repository;
    
    public GetSpecVotersController(IRepository repository)
    {
        this.repository = repository;
    }   


    [HttpGet]
    public ActionResult<IEnumerable<Ballot>> Index(string lastName)
    {
        var voters = repository.GetVoters(p => p.Ballot == lastName);
        return Ok(voters);
    }
}