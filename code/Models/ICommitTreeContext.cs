namespace Models;

public interface ICommitTreeContext : IDisposable
{
    DbSet<AuthorData> allAuthorData {get;}
    DbSet<FrequencyData> allFrequencyData {get;}

}