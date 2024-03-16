namespace PresidentSite.Models;

public class Ballot : IBallot
{
    public static Ballot NotFound = new Ballot();
    
    public int Id {get; set;}
    public string LastName {get; set;}=null!;
    public int Votes {get; set;}
}
