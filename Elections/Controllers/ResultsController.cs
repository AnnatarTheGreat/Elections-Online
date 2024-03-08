using PresidentSite.Models;
using Microsoft.AspNetCore.Mvc;
using PresidentSite.Models.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;


namespace PresidentElectionsOnline.Controllers;

public class ResultsController : Controller
{
    private readonly ElectorCounterContext _electorCounterContext;
    public ResultsController(ElectorCounterContext electorCounterContext)
    {
        _electorCounterContext = electorCounterContext;
    }


    [Authorize]
    public IActionResult Index()
    {
        var currentVoterJson = HttpContext.Session.GetString("CurrentVoter");
        if (currentVoterJson == null)
        {
            string errorMessage = "Please log in to see results.";
            TempData["Message"] = errorMessage;
            return RedirectToAction("Index", "Authorization");
        }
        var currentVoter = JsonSerializer.Deserialize<Voter>(currentVoterJson);
        
        if (currentVoter.Ballot != null)
        {
        var context = _electorCounterContext;
        var ballots = context.Ballots.ToList();
        return View(ballots);
        }

        string message = "You have to vote before You see the results.";
        TempData["Message"] = message;
        return RedirectToAction("Index", "Vote");

    }

}