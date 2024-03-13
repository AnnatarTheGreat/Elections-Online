




using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using PresidentSite.Models;
using PresidentSite.Models.Data;

public class Repository : IRepository
{
    private ElectorCounterContext context;

    public Repository(ElectorCounterContext context)
    {
        this.context = context;
    }

    public void Save()
    {
        context.SaveChanges();
    }

    public Voter FindVoter(Func<Voter, bool> predicate)
    {
        return context.Voters.FirstOrDefault(predicate); 
    }

    public Ballot? FindBallot(Func<Ballot, bool> predicate)
    {
        return context.Ballots.FirstOrDefault(predicate);
    }

    public void InsertVoter(Voter voter)
    {
        context.Voters.Add(voter);
    }

    public List<Ballot> BallotsToList()
    {
        return context.Ballots.ToList();
    }

    public IEnumerable<Voter> GetVoters(Func<Voter, bool> predicate)
    {
        return context.Voters.Where(predicate);
    }

    public DisplayAttribute? DisplayAttribute(System.Reflection.PropertyInfo? propertyInfo)
    {
        return (DisplayAttribute)propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
    }

    
}