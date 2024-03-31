namespace PresidentSite.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ElectorCounterContext : IdentityDbContext<Voter>
{
    public ElectorCounterContext(DbContextOptions<ElectorCounterContext> options)
        : base(options)
        {}
    public DbSet<Ballot> Ballots {get; set;}
    public DbSet<Voter> Voters {get; set;}
    
}
