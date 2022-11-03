//using Microsoft.EntityFrameworkCore;
namespace GitInsight;

//context factory here
/*Program.cs skal kalde den her, for at lave database.
den laver en instans af GitInsightContext og returnerer den.
*/

/*
Der mangler sådan en EntityFrameWorkCore.targets thingy.
check det det microsoft framework shiet*/

internal class GitInsightContextFactory : IDesignTimeDbContextFactory<GitInsightContext>
{
    public GitInsightContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        var connectionString = configuration.GetConnectionString("<name of string>");

        var optionsBuilder = new DbContextOptionsBuilder<GitInsightContext>();
        optionsBuilder.UseNpgsql(connectionString);

        var context = new GitInsightContext(optionsBuilder.Options);
        return context;
    }
}
