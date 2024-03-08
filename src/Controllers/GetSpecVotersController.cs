using Microsoft.AspNetCore.Mvc;
using PresidentSite.Models.Data;
using PresidentSite.Models;

namespace PresidentElectionsOnline.Controllers;

[Route("api/voters/{lastName}")]
public class GetSpecVotersController : Controller
{   
    private readonly ElectorCounterContext _electorCounterContext;
    public GetSpecVotersController(ElectorCounterContext electorCounterContext)
    {
        _electorCounterContext = electorCounterContext;
    }
   [HttpGet]
    public ActionResult<IEnumerable<Ballot>> Index(string lastName)
    {
        var context = _electorCounterContext;
        var voters = context.Voters.Where(p => p.Ballot == lastName).ToList();
        return View(voters);
    }
}