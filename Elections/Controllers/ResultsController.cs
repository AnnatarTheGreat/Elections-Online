using PresidentSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SignalRResults.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;

namespace PresidentElectionsOnline.Controllers;

public class ResultsController : Controller
{
    private IRepository repository;
    private IHubContext<ResultsHub> hubContext;

    private UserManager<Voter> userManager;

    public ResultsController(IRepository repository, IHubContext<ResultsHub> hubContext, UserManager<Voter> userManager)
    {
        this.repository = repository;
        this.hubContext = hubContext;
        this.userManager = userManager;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var currentVoter = await userManager.GetUserAsync(User);

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