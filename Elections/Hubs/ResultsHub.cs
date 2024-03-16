using Microsoft.AspNetCore.SignalR;
using PresidentSite.Models;
using System.Collections.Generic;

namespace SignalRResults.Hubs;


public class ResultsHub : Hub
{
    public async Task UpdateResults(int id, int votes)
    {
        await Clients.All.SendAsync("ShowResults", id,votes);
    }

}

