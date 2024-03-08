namespace PresidentSite.Models;


public interface IVoter
{
    int Id { get; set; }
    string Name { get; set; }
    string Surname { get; set; }
    int Age { get; set; }
    public string? Ballot { get; set; }
}