namespace Models;

public sealed class CommitTreeContext : DbContext , ICommitTreeContext
{
    public CommitTreeContext(DbContextOptions<CommitTreeContext> options) 
        : base(options)
    {
    }

    public DbSet<AuthorData> allAuthorData => Set<AuthorData>();
    public DbSet<FrequencyData> allFrequencyData => Set<FrequencyData>();

    public DbSet<TodoItem> TodoItems { get; set; } = null!;
    

//NOTE for later use, incase we need to specify type conversions susch as Enums.
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    // }
        
}