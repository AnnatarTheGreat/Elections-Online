using Microsoft.EntityFrameworkCore;
namespace PresidentSite.Models.Data;

public class ElectorCounterContext : DbContext
{
    private readonly IConfiguration configuration;
    public ElectorCounterContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    
    public DbSet<Ballot> Ballots {get; set;}
    public DbSet<Voter> Voters {get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        string? connectionString = configuration.GetConnectionString("DataBase");
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }


    


}
