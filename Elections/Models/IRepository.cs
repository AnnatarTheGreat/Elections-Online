using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using PresidentSite.Models;

public interface IRepository
{
    void Save();

    void InsertVoter(Voter voter);

    void Vote(Voter voter, Ballot ballot);

    List<Ballot> BallotsToList();

    Ballot? FindBallot(Func<Ballot, bool> predicate);

    Voter FindVoter(Func<Voter, bool> predicate);

    IEnumerable<Voter> GetVoters(Func<Voter, bool> predicate);

    DisplayAttribute? DisplayAttribute(System.Reflection.PropertyInfo? propertyInfo);

     IEnumerable<T> GetPropertyOfBallots<T>(Func<Ballot, T> predicate);
}

