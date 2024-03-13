namespace PresidentSite.Models;

public class Ballot : IBallot
{
    public Ballot NotFound {get; set;}
    
    public int Id {get; set;}
    public string LastName {get; set;}=null!;
    public int Votes {get; set;}
}
