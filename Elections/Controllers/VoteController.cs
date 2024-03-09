using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresidentSite.Models;
using PresidentSite.Models.Data;
namespace PresidentElectionsOnline.Controllers;

public class VoteController : Controller
{
    private IRepository repository;
    
    public VoteController(IRepository repository)
    {
        this.repository = repository;
    }   



    [Authorize]
    [HttpGet]
    public IActionResult Index()
    {
        var currentVoterJson = HttpContext.Session.GetString("CurrentVoter");
        if (currentVoterJson == null)
        {
            string errorMessage = "Please log in to vote.";
            TempData["Message"] = errorMessage;
            return RedirectToAction("Index", "Authorization");
        }
        var currentVoter = JsonSerializer.Deserialize<Voter>(currentVoterJson);

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
    public IActionResult Index(Ballot ballot)
    {
        var currentVoterJson = HttpContext.Session.GetString("CurrentVoter");
        var currentVoter = JsonSerializer.Deserialize<Voter>(currentVoterJson);

        var voter = repository.FindVoter().FirstOrDefault(p => p.Name == currentVoter.Name && p.Surname == currentVoter.Surname);
        var currentBallot = repository.FindBallotById(ballot);
        if (currentBallot != null)
        {
            voter.Ballot = currentBallot.LastName;
            currentBallot.Votes++;
            repository.Save();
            
            HttpContext.Session.SetString("CurrentVoter", JsonSerializer.Serialize(voter));
            string message = $"You have voted for {currentBallot.LastName}!\nCheck current results of elections!";
            ViewBag.Message = message;
            return RedirectToAction("VotingResults", "Vote", new { message });
        }
        else
        {
            string errorMessage = "Error";
            ViewBag.Message = errorMessage;
            return RedirectToAction("Error", "Vote", new { errorMessage });
        }
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