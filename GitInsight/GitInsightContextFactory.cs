namespace GitInsight;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

/*Kommando til at lave migrations. Skal ikke bruges igen pt. da de er blevet lavet
dotnet ef migrations add Added_something --verbose -p .\GitInsight.Entities\ -s .\GitInsight
*/

//Denne klasse er kun relevant ift. før-api-projekt

public class GitInsightContextFactory : IDesignTimeDbContextFactory<GitInsightContext> //internal
{
    public GitInsightContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder().AddUserSecrets<GitInsightContextFactory>().Build();
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
