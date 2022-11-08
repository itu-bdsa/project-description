// namespace GitInsight;

// public class CommitTreeContextFactory : IDesignTimeDbContextFactory<CommitTreeContext>
// {

//     public CommitTreeContextFactory CreateDbContext(string[] args)
//     {
//         var configuration = new ConfigurationBuilder().AddUserSecrets<CommitTreeContext>().Build();
//         var connectionString = configuration.GetConnectionString("ConnectionString");

//         String connectionStringHardcode = "Server=localhost;Database=stoic_chandrasekhar;User Id=sa;Password=BDSAagain22;Trusted_Connection=False;Encrypt=False";

//         var optionsBuilder = new DbContextOptionsBuilder<CommitTreeContext>();
//         optionsBuilder.UseSqlServer(connectionStringHardcode);

//         return new CommitTreeContext(optionsBuilder.Options);
//     }

// }