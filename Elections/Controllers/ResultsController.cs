using PresidentSite.Models;
using Microsoft.AspNetCore.Mvc;
using PresidentSite.Models.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;


namespace PresidentElectionsOnline.Controllers;

public class ResultsController : Controller
{
    private IRepository repository;
    
    public ResultsController(IRepository repository)
    {
        this.repository = repository;
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
        var ballots = repository.BallotsToList();
        return View(ballots);
        }

        string message = "You have to vote before You see the results.";
        TempData["Message"] = message;
        return RedirectToAction("Index", "Vote");

    }

}