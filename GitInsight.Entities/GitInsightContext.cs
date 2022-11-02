namespace GitInsight.Entities;

public partial class GitInsightContext : DbContext
{

    public GitInsightContext(){}
    public GitInsightContext(DbContextOptions<GitInsightContext> options)
        : base(options)
    { }

    public virtual DbSet<CommmitSignature> Signatures { get; set; } = null!;

    //every time this class is instantiated we need to check if already configured
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){

        if(!optionsBuilder.IsConfigured){
        var configuration = new ConfigurationBuilder().AddUserSecrets<GitInsightContext>().
        Build();
        var connectionString = configuration.GetConnectionString("gitInsightDB");
        optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommmitSignature>().Property(e => e.Date).HasConversion<string>();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public void Clear()
    {
     
    }
}