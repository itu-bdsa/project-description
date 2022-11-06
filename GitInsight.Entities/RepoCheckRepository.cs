namespace GitInsight.Entities;
using GitInsight.Core;

public class RepoCheckRepository {

    private GitInsightContext _context;

    public RepoCheckRepository(GitInsightContext context)
    {
        _context = context;
    }

    public void Create(RepoCheckCreateDTO repoCheck){ //string repoPath, string lastCheckedCommit
        var newRepoCheck = new RepoCheck{ repoPath = repoCheck.repoPath,
                             lastCheckedCommit = repoCheck.lastCheckedCommit
                             };

        _context.Add(newRepoCheck);
        _context.SaveChanges();
    }

    public RepoCheckDTO Read(string repoPath){
        var repoCheck = _context.RepoChecks.Find(repoPath);
        //var DTO = new RepoCheckDTO();
        return repoCheck != null ? new RepoCheckDTO(
                                    repoCheck.repoPath,
                                    repoCheck.lastCheckedCommit,
                                    Contributions: ContributionDTOsToList(repoCheck)) : null!;
    }

    //helper method to above method
    private ICollection<ContributionDTO> ContributionDTOsToList(RepoCheck repoCheck){
        var contributions = repoCheck.Contributions
                    .Select(cont => new ContributionDTO(
                        cont.Id, cont.repoPath, cont.author,
                        cont.date, cont.commitsCount
                    )).ToList();
        return contributions;
    }

    public void Update(RepoCheckUpdateDTO repoCheck){ //string repoPath, string newestCheckedCommit
        var toUpdate = _context.RepoChecks.Find(repoCheck.repoPath);
        toUpdate!.lastCheckedCommit = repoCheck.lastCheckedCommit;
        _context.SaveChanges();
    }

}