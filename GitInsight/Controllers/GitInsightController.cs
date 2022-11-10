
using GitInsight.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GitInsight.Entities;
using LibGit2Sharp;

namespace GitInsight.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitInsightController : ControllerBase
    {
        private GitInsightContext _context;

        public GitInsightController(GitInsightContext context)
        {
            _context = context;
        }

        private static Contribution ContributionFromContributionDTO(ContributionDTO contribution)
         => new Contribution
         {
             repoPath = contribution.RepoPath,
             author = contribution.Author,
             date = contribution.Date,
             commitsCount = contribution.CommitsCount
         };

        [HttpPost("{repoPath}")]
        public void Post(string repoPath)
        {
            _context.Database.OpenConnection();
            Console.WriteLine(_context.Database.CanConnect());
            addRepoCheckToDB(repoPath);

            //_context.Add(newRepoCheck);
            //_context.SaveChanges();
        }

    //--------------Helper methods to Post()-------------------
    private void addRepoCheckToDB(string repoPath){
        var repo = new Repository(repoPath);
        var checkedCommit = repo.Commits.ToList().First().Id.ToString();

        var conDTOs = AddContributionsDataToSet(repoPath, repo);

        var newRepoCheck = new RepoCheck
                    {
                        repoPath = repoPath,
                        lastCheckedCommit = checkedCommit,
                        Contributions = conDTOs.Select(c => 
                        ContributionFromContributionDTO(c)).ToHashSet()
                    };

        
        _context.RepoChecks.Add(newRepoCheck);
        _context.SaveChanges();
        
    }

    private HashSet<ContributionDTO> AddContributionsDataToSet(string repoPath, Repository repo){
        //add repo data to hashset
        var commitArray = repo.Commits.ToList();
        var contributionsList = new HashSet<ContributionDTO>();

        foreach (var c in commitArray){
            //get number of commits by auhtor on date
            int commitNr = getNrCommitsOnDateByAuthor(c.Author.When.Date, c.Author, repo);

            var newContri = new ContributionDTO(
                RepoPath: repoPath,
                Author: c.Author.ToString(), 
                Date: c.Author.When.Date,
                CommitsCount: commitNr);

            contributionsList.Add(newContri);
        }

        return contributionsList;
    }

    private int getNrCommitsOnDateByAuthor(DateTime date, Signature author, Repository repo){
        var commitsCount = repo.Commits
        .Select(e => new { e.Author, e.Author.When.Date })
        .Where(e => e.Author.ToString() == author.ToString()
        && e.Author.When.Date == date).Count();

        return commitsCount;
    }

    //-------------------------------------------------------

        /* [HttpGet]
          public async Task<ActionResult<RepoCheckDTO>> Get(string repoPath){
              var repoCheck = _context.RepoChecks.Find(repoPath);
              //var DTO = new RepoCheckDTO();
              return repoCheck != null ? new RepoCheckDTO(
                                          repoCheck.repoPath,
                                          repoCheck.lastCheckedCommit,
                                          Contributions: ContributionDTOsToList(repoCheck)) : null!;
          }*/

        [HttpGet("{repoPath}")] //the GET()
        public RepoCheckDTO GetRepoCheck(string repoPath)
        {
            //C:\Users\annem\Desktop\BDSA_PROJECT\TestGithubStorage\assignment-05
            var repoCheck = _context.RepoChecks.Find(repoPath);
            //var DTO = new RepoCheckDTO();
            return repoCheck != null ? new RepoCheckDTO(
                                        repoCheck.repoPath,
                                        repoCheck.lastCheckedCommit,
                                        Contributions: ContributionDTOsToList(repoCheck)) : null!;

        }
        //helper method to above method
        private HashSet<ContributionDTO> ContributionDTOsToList(RepoCheck repoCheck)
        {
            var contributions = repoCheck.Contributions
                        .Select(cont => new ContributionDTO(
                            cont.repoPath, cont.author, //cont.Id, 
                            cont.date, cont.commitsCount
                        )).ToHashSet();
            return contributions;
        }

        [HttpPut("{repoPath}")]

        public void Put(RepoCheckUpdateDTO repoCheck)
        { //string repoPath, string newestCheckedCommit
            var toUpdate = _context.RepoChecks.Find(repoCheck.repoPath);
            toUpdate!.lastCheckedCommit = repoCheck.lastCheckedCommit;

            var newCons = repoCheck.Contributions.Select(c =>
            ContributionFromContributionDTO(c)).ToList();
            toUpdate.Contributions = newCons;
            //toUpdate.Contributions.ToList()
            //.AddRange(newCons.Except(toUpdate.Contributions.ToList()));

            _context.SaveChanges();
        }

        /* THIS METHOD DOESNT WORK YET! DONT USE OR DELETE PLZ AND TY
        [HttpDelete("{repoPath}")]
        
        public void Delete(string repoPath){
            var repoCheck = _context.RepoChecks.Find(repoPath);
            _context.RepoChecks.Remove(repoCheck);
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
        }*/

    }
}