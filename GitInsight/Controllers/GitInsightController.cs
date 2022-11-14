using GitInsight.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibGit2Sharp;
using System.Collections;

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
             author = contribution.Author,
             date = contribution.Date,
             commitsCount = contribution.CommitsCount
         };

        [HttpPost("{repoPath}")]
        public void Post(string repoPath)
        {
            _context.Database.OpenConnection();
            Console.WriteLine(_context.Database.CanConnect());

            String s = repoPath.Substring(repoPath.IndexOf("github.com%2F") + 13);
            s.Trim();

            //Repo name generator so we can create multiple temp-folders
            string folderPath = "../TestGithubStorage/" + s.Replace("%2F", "-");
            Console.WriteLine(folderPath);

            //https://github.com/SpaceVikingEik/assignment-05.git
            //Repository.Clone("https://github.com/VictoriousAnnro/Assignment0.git", "../TestGithubStorage/tempGitRepo");

            //String manipulation bc / gets replaced with %2F so we have to change it back for the method to work
            repoPath = repoPath.Replace("%2F", "/");
            
            if(Directory.Exists(folderPath)){

                //I cant really fathom why this works, but it does update the folder to the newest version of main
                Repository repo = new Repository(folderPath);
                Commands.Pull(repo,new Signature(" d", "d ",new DateTimeOffset()),new PullOptions());

                //addRepoCheckToDB(folderPath); for testing

            } else {
                var path = Repository.Clone(repoPath, folderPath);
                addRepoCheckToDB(folderPath);
            }
        }

    //--------------Helper methods to Post()-------------------
    private void addRepoCheckToDB(string repoPath){
        var repo = new Repository(repoPath);
        var checkedCommit = repo.Commits.ToList().First().Id.ToString();

        var conDTOs = AddContributionsDataToSet(repo, repoPath);

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

    private HashSet<ContributionDTO> AddContributionsDataToSet(Repository repo, string repoPath){
        //add repo data to hashset
        var commitArray = repo.Commits.ToList();
        var contributionsList = new HashSet<ContributionDTO>();

        foreach (var c in commitArray){
            //get number of commits by auhtor on date
            int commitNr = getNrCommitsOnDateByAuthor(c.Author.When.Date, c.Author, repo);

            var newContri = new ContributionDTO(
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


        [HttpGet("{repoPath}")]
        public IActionResult GetAnalysis(string repoPath, string analyseMode){
            _context.Database.OpenConnection();

            String s = repoPath.Substring(repoPath.IndexOf("github.com%2F") + 13);
            s.Trim();

            string folderPath = "../TestGithubStorage/" + s.Replace("%2F", "-");
            //https://github.com/VictoriousAnnro/Assignment0.git

            if(analyseMode.Equals("FQMode")){
                return Ok(CommitFrequencyGet(folderPath));
            } else if (analyseMode.Equals("AuthMode")){
                return Ok(userCommitFreq(folderPath));
            } else return Ok(null);

        }

        private List<comFreqObj> CommitFrequencyGet(string folderPath){
            _context.Database.OpenConnection();

            var repoCheckItem = _context.RepoChecks.Find(folderPath); //check om commit newest, fix
            var items = _context.Contributions.Where(c => c.repoCheckObj.Equals(repoCheckItem));

            var date = items.Select(c => c.date.Date).Distinct().ToList();

            var intList = new List<int>();
            foreach(var d in date){
                var comCount = items.Where(k => k.date.Date.Equals(d))
                .Select(k => k.commitsCount).Sum();
                intList.Add(comCount);
            }

            var tempList = new List<comFreqObj>();
            for (var i = 0; i < intList.Count; i++){
                var tem = new comFreqObj(date[i].Date.ToString(), intList[i]);
                tempList.Add(tem);
            }

            return tempList;
        }

        public record comFreqObj(string date, int commits);

        public record userComFreqObj(string author, List<Tuple<string, int>> datesCommits);

        private List<userComFreqObj> userCommitFreq(string folderPath){
            _context.Database.OpenConnection();

            var repoCheckItem = _context.RepoChecks.Find(folderPath); //check om commit newest, fix
            var contributions = _context.Contributions.Where(c => c.repoCheckObj.Equals(repoCheckItem));

            var authors = contributions.Select(c => c.author).Distinct().ToList();
            var data = new List<userComFreqObj>();
            foreach(string auth in authors){
                var intList = new List<int>();
                var contrList = new List<Tuple<string, int>>();

                var dates = contributions.Where(k => k.author.Equals(auth))
                            .Select(c => c.date).Distinct().ToList();

                foreach(var d in dates){
                    var comCount = contributions.Where(k => k.date.Equals(d)
                    && k.author.Equals(auth))
                    .Select(k => k.commitsCount).Sum();
                    intList.Add(comCount);
                }
            
                for (var i = 0; i < intList.Count; i++){
                    var tempTuple = Tuple.Create(dates[i].Date.ToString(), intList[i]);
                    contrList.Add(tempTuple);
                }

                data.Add(new userComFreqObj(auth, contrList));
            }
            
            return data;
        }


        //Hvad bruges denne til? gem?
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

    }
}