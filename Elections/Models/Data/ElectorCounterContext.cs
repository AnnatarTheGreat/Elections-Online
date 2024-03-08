using Microsoft.EntityFrameworkCore;
namespace PresidentSite.Models.Data;

public class ElectorCounterContext : DbContext
{
    public ElectorCounterContext(DbContextOptions<ElectorCounterContext> options)
        : base(options)
        {}
    public DbSet<Ballot> Ballots {get; set;}
    public DbSet<Voter> Voters {get; set;}
    
}
