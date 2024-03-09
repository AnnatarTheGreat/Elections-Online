




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

    public Microsoft.EntityFrameworkCore.DbSet<Voter> FindVoter()
    {
        return context.Voters;
    }

    public Ballot? FindBallotById(Ballot ballot)
    {
        return context.Ballots.FirstOrDefault(p => p.Id == ballot.Id);
    }

    public void InsertVoter(Voter voter)
    {
        context.Voters.Add(voter);
    }

    public List<Ballot> BallotsToList()
    {
        return context.Ballots.ToList();
    }

    public IQueryable<Voter> GetVotersByBallots(string lastName)
    {
        return context.Voters.Where(p => p.Ballot == lastName);
    }

    public DisplayAttribute? DisplayAttribute(System.Reflection.PropertyInfo? propertyInfo)
    {
        return (DisplayAttribute)propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
    }

    
}