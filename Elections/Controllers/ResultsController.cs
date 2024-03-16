using PresidentSite.Models;
using Microsoft.AspNetCore.Mvc;
using PresidentSite.Models.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using SignalRResults.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace PresidentElectionsOnline.Controllers;

public class ResultsController : Controller
{
    private IRepository repository;
    private IHubContext<ResultsHub> hubContext;

    public ResultsController(IRepository repository, IHubContext<ResultsHub> hubContext)
    {
        this.repository = repository;
        this.hubContext = hubContext;
    }

    [Authorize]
    public async Task<IActionResult> Index()
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