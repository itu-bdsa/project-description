namespace GitInsight;

public class CommitTreeContextFactory : IDesignTimeDbContextFactory<CommitTreeContext>
{

    public CommitTreeContext CreateDbContext(string[] args)
    {
        //var configuration = new ConfigurationBuilder().AddUserSecrets<CommitTreeContext>().Build();
        //var connectionString = configuration.GetConnectionString("ConnectionString");

        String connectionStringHardcode = "Server=localhost;Database=youthful_keller;User Id=sa;Password=Gruppen123;Trusted_Connection=False;Encrypt=False";
        var optionsBuilder = new DbContextOptionsBuilder<CommitTreeContext>();
        optionsBuilder.UseSqlServer(connectionStringHardcode, b => b.MigrationsAssembly("GitInsight"));

        return new CommitTreeContext(optionsBuilder.Options);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}