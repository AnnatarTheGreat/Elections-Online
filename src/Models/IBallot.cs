namespace PresidentSite.Models;

public interface IBallot
{
    int Id {get; set;}
    string LastName {get; set;}
    int Votes {get; set;}
}
