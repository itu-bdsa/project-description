/*
using GitInsight.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GitInsight.Entities;
namespace GitInsight.Entities{
    [Route("api/[controller]")]
    [ApiController]
public class GitInsightController : ControllerBase {


    private readonly GitInsightContext _context;

    public GitInsightController(GitInsightContext context)
    {
        _context = context;
    }

    private static Contribution ContributionFromContributionDTO(ContributionDTO contribution)
     => new Contribution{
        repoPath = contribution.RepoPath,
        author = contribution.Author,
        date = contribution.Date,
        commitsCount = contribution.CommitsCount
     };

    [HttpPost("{repoPath}")]
    public void Post(RepoCheckCreateDTO repoCheck){ //string repoPath, string lastCheckedCommit
        var newRepoCheck = new RepoCheck{ repoPath = repoCheck.repoPath,
                             lastCheckedCommit = repoCheck.lastCheckedCommit,
                             Contributions = repoCheck.Contributions.Select(c => ContributionFromContributionDTO(c)).ToHashSet()};

        _context.Add(newRepoCheck);
        _context.SaveChanges();
    }



   [HttpGet]
    public async Task<ActionResult<RepoCheckDTO>> GetRepoCheck(string repoPath){
        var repoCheck = await _context.RepoChecks.FindAsync(repoPath);
        //var DTO = new RepoCheckDTO();
        return repoCheck != null ? new RepoCheckDTO(
                                    repoCheck.repoPath,
                                    repoCheck.lastCheckedCommit,
                                    Contributions: ContributionDTOsToList(repoCheck)) : null!;
    }

  [HttpGet("{repoPath}")]
        public async Task<ActionResult<RepoCheckDTO>> GetWebApiItem(string repoPath)
        {
            var repoCheck = await _context.RepoChecks.FindAsync(repoPath);

            if (repoCheck == null)
            {
                return NotFound();
            }

            return repoCheck != null ? new RepoCheckDTO(
                                    repoCheck.repoPath,
                                    repoCheck.lastCheckedCommit,
                                    Contributions: ContributionDTOsToList(repoCheck)) : null!;
        }
    //helper method to above method
    private HashSet<ContributionDTO> ContributionDTOsToList(RepoCheck repoCheck){
        var contributions = repoCheck.Contributions
                    .Select(cont => new ContributionDTO(
                        cont.repoPath, cont.author, //cont.Id, 
                        cont.date, cont.commitsCount
                    )).ToHashSet();
        return contributions;
    }

    [HttpPut("{repoPath}")]
    public void UpdateRepoCheck(RepoCheckUpdateDTO repoCheck){ //string repoPath, string newestCheckedCommit
        var toUpdate = _context.RepoChecks.Find(repoCheck.repoPath);
        toUpdate!.lastCheckedCommit = repoCheck.lastCheckedCommit;

        var newCons = repoCheck.Contributions.Select(c => 
        ContributionFromContributionDTO(c)).ToList();
        toUpdate.Contributions = newCons; 
        //toUpdate.Contributions.ToList()
        //.AddRange(newCons.Except(toUpdate.Contributions.ToList()));

        _context.SaveChanges();
    }

    //slet??
    /*private void UpdateContribution(ContributionUpdateDTO contribution){
        //find entry - opdater antal commits for entry
        var contributionToUpdate = _context.Contributions
                            .Where(c => contribution.Author == c.author 
                            && contribution.Date == c.date 
                            && contribution.RepoPath == c.repoPath).FirstOrDefault();
        
        contributionToUpdate.commitsCount +=  contribution.NewCommitsCount;

        _context.SaveChanges();
    }

}
}*/