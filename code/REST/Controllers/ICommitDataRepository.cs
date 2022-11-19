namespace REST.Controllers;


public interface ICommitDataRepository
{

    void Create(CommitDataCreateDTO CommitData);

    IReadOnlyCollection<Dictionary<string, List<Tuple<DateTime, int>>>> ReadAllCommitsFromRepo(string RepositoryName);
    
    IReadOnlyCollection<Tuple<DateTime, int>> GetAllAuthorsCommitsFromRepository(string RepositoryName);

}