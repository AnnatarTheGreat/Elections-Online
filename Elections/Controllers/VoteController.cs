using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PresidentSite.Models;
using SignalRResults.Hubs;
using Microsoft.AspNetCore.Identity;
namespace PresidentElectionsOnline.Controllers;


public class VoteController : Controller
{
    private IRepository repository;
    private UserManager<Voter> userManager;
    private IHubContext<ResultsHub> resultsHub;
    public VoteController(IRepository repository, IHubContext<ResultsHub> resultsHub, UserManager<Voter> userManager)
    {
        this.repository = repository;
        this.resultsHub = resultsHub;
        this.userManager = userManager;
    }   



    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var currentVoter = await userManager.GetUserAsync(User);
        if (currentVoter.Ballot == null)
        {
            var ballots = repository.BallotsToList();
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            return View(ballots);
        }
        else
        {
            string errorMessage = "You have already voted!";
            ViewBag.Message = errorMessage;
            return RedirectToAction("Error", "Vote", new { errorMessage });
        }
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Index(Ballot ballot)
    {
        var currentVoter = await userManager.GetUserAsync(User);

        //var voter = repository.FindVoter(p => p.Name == currentVoter.Name && p.Surname == currentVoter.Surname);
        var currentBallot = repository.FindBallot(p => p.Id == ballot.Id);
        if (currentBallot != null)
        {
            repository.Vote(currentVoter, currentBallot);
            repository.Save();
            await resultsHub.Clients.All.SendAsync("ShowResults", currentBallot.Id, currentBallot.Votes);
            //HttpContext.Session.SetString("CurrentVoter", JsonSerializer.Serialize(voter));
            string message = $"You have voted for {currentBallot.LastName}!\nCheck current results of elections!";
            ViewBag.Message = message;
            return RedirectToAction("VotingResults", "Vote", new { message });
        }
            string errorMessage = "Error";
            ViewBag.Message = errorMessage;
            return RedirectToAction("Error", "Vote", new { errorMessage });
    }

    public IActionResult VotingResults(string message)
    {
        ViewBag.Message = message;
        return View("~/Views/Vote/VotingResults.cshtml");
    }

    public IActionResult Error(string errorMessage)
    {
        ViewBag.Message = errorMessage;
        return View("~/Views/Vote/Error.cshtml");
    }

}