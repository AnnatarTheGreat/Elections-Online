namespace PresidentSite.Models;

public class Ballot : IBallot
{
    public int Id {get; set;}
    public string LastName {get; set;}=null!;
    public int Votes {get; set;}
    public Ballot(){}
}
