namespace GitInsight.Entities;

public interface ICommitTreeContext : IDisposable
{

    DbSet<CommitData> CommitData { get; }

}