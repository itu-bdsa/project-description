namespace GitInsight.Entities;

public interface ICommitTreeContext : IDisposable
{
    DbSet<Author> Authors {get;}
    DbSet<Frequency> Frequencies {get;}

}