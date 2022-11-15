namespace GitInsight.Entities;

//context for database
//aka, hvordan skal databasen sættes op? Strukturen

public partial class GitInsightContext : DbContext 
{
    public GitInsightContext(DbContextOptions<GitInsightContext> options)
        : base(options)
    {}

    public virtual DbSet<RepoCheck> RepoChecks { get; set; } = null!;

    public virtual DbSet<Contribution> Contributions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        //write stuff heeeereeeee

        modelBuilder.Entity<RepoCheck>(entity =>
        {
            //repopath must be unique
            entity.HasIndex(e => e.repoPath).IsUnique();
        });

        modelBuilder.Entity<Contribution>(entity =>
        {
            
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public void Clear() {

        using var transaction = this.Database.BeginTransaction();
        RepoChecks.RemoveRange(RepoChecks);
        Contributions.RemoveRange(Contributions);
        SaveChanges();
        transaction.Commit();
    }
}