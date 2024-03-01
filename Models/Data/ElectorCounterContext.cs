using Microsoft.EntityFrameworkCore;
namespace PresidentSite.Models.Data;

public class ElectorCounterContext : DbContext
{
    public DbSet<Ballot> Ballots {get; set;}
    public DbSet<Voter> Voters {get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        string? connectionString = configuration.GetConnectionString("DataBase");
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}
