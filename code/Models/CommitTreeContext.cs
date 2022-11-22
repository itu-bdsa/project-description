namespace Models;

public sealed class CommitTreeContext : DbContext, ICommitTreeContext
{
    public CommitTreeContext(DbContextOptions<CommitTreeContext> options)
        : base(options)
    {
    }


    public DbSet<CommitData> CommitData => Set<CommitData>();
    // public DbSet<AuthorData> allAuthorData => Set<AuthorData>();
    // public DbSet<FrequencyData> allFrequencyData => Set<FrequencyData>();
    

    //NOTE for later use, incase we need to specify type conversions susch as Enums.
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    // }



}