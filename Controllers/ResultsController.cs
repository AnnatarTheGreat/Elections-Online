using PresidentSite.Models;
using Microsoft.AspNetCore.Mvc;
using PresidentSite.Models.Data;

namespace PresidentElectionsOnline.Controllers;

public class ResultsController : Controller
{
    private readonly IConfiguration configuration;

    public IActionResult Index()
    {
        using var context = new ElectorCounterContext(configuration);
        var ballots = context.Ballots.ToList();
        return View(ballots);
    }
}