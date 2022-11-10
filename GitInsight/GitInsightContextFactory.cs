namespace GitInsight;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

/*Program.cs skal kalde den her, for at lave database.
den laver en instans af GitInsightContext og returnerer den.

Kommando til at lave migrations. Skal ikke bruges igen pt. da de er blevet lavet
dotnet ef migrations add Added_something --verbose -p .\GitInsight.Entities\ -s .\GitInsight
*/

public class GitInsightContextFactory : IDesignTimeDbContextFactory<GitInsightContext> //internal
{
    public GitInsightContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder().AddUserSecrets<GitInsightClass>().Build();
        var connectionString = configuration.GetConnectionString("GitIn");
        /*$CONNECTION_STRING="Host=localhost;Database=GitIn;Username=<username>;Password=<password>");" 
        dotnet user-secrets set "ConnectionStrings:GitIn" "$CONNECTION_STRING"*/

        var optionsBuilder = new DbContextOptionsBuilder<GitInsightContext>();
        optionsBuilder.UseNpgsql(connectionString);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        var context = new GitInsightContext(optionsBuilder.Options);
        return context;
    }
}
