namespace REST.Controllers;


public interface ICommitDataRepository
{

    void Create(CommitDataCreateDTO CommitData);

    IReadOnlyDictionary<string, List<Tuple<DateTime, int>>> GetAllAuthorsCommitsFromRepository(string RepositoryName);

    IReadOnlyCollection<Tuple<DateTime, int>> ReadAllCommitsFromRepo(string RepositoryName);

}