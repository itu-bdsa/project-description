namespace GitInsight.Entities;

public class RepoCheckRepository {

    private GitInsightContext _context;

    public RepoCheckRepository(GitInsightContext context)
    {
        _context = context;
    }

    public void Create(string repoPath, string lastCheckedCommit){
        var newRepoCheck = new RepoCheck{ repoPath = repoPath, lastCheckedCommit = lastCheckedCommit};
        _context.Add(newRepoCheck);
        _context.SaveChanges();
    }

    public RepoCheck Read(string repoPath){
        var repoCheckObject = _context.RepoChecks.Find(repoPath);
        return repoCheckObject != null ? repoCheckObject : null!;
    }

    public void Update(string repoPath, string newestCheckedCommit){
        var toUpdate = _context.RepoChecks.Find(repoPath);
        toUpdate!.lastCheckedCommit = newestCheckedCommit;
        _context.SaveChanges();
    }

}