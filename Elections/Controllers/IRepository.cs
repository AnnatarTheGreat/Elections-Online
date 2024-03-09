





using System.ComponentModel.DataAnnotations;
using PresidentSite.Models;

public interface IRepository
{

    void Save();

    Microsoft.EntityFrameworkCore.DbSet<Voter> FindVoter();

    void InsertVoter(Voter voter);

    List<Ballot> BallotsToList();

    Ballot? FindBallotById(Ballot ballot);

    IQueryable<Voter> GetVotersByBallots(string lastName);

    DisplayAttribute? DisplayAttribute(System.Reflection.PropertyInfo? propertyInfo);

    
}

