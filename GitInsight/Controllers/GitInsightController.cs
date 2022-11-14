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
        private RepoCheckRepository _repository;

        public GitInsightController(GitInsightContext context)
        {
            _context = context;
            _repository =  new RepoCheckRepository(context);
            _context.Database.OpenConnection();
        }

        //new method to put Post and Get together
        [HttpGet("{repoPath}")]
        public IActionResult GetAnalysis(string repoPath, string analyseMode){
            String s = repoPath.Substring(repoPath.IndexOf("github.com%2F") + 13);
            s.Trim();

            //Repo name generator so we can create multiple temp-folders
            string folderPath = "../TestGithubStorage/" + s.Replace("%2F", "-");

            //String manipulation bc / gets replaced with %2F so we have to change it back for the method to work
            repoPath = repoPath.Replace("%2F", "/");

            if(Directory.Exists(folderPath)){

                //I cant really fathom why this works, but it does update the folder to the newest version of main
                Repository repo = new Repository(folderPath);
                Commands.Pull(repo,new Signature(" d", "d ",new DateTimeOffset()),new PullOptions());

                //addRepoCheckToDB(folderPath); //for testing

            } else {
                var path = Repository.Clone(repoPath, folderPath);
                addRepoCheckToDB(folderPath);
            }

            if(!_repository.CurrentCommitIdMostRecentCommit(folderPath)){
                //update entries in db
                _repository.Update(folderPath);
            }

            if(analyseMode.Equals("FQMode")){
                return Ok(CommitFrequencyGet(folderPath));
            } else if (analyseMode.Equals("AuthMode")){
                return Ok(userCommitFreq(folderPath));
            } else return Ok(null);

        }

        //--Helper method to GetAnalysis()-----
        private void addRepoCheckToDB(string repoPath){
            //create repocheck and add in _repository
            _repository.CreateEntryInDB(repoPath);
        }
        //-----------------------------------


        [HttpPost("{repoPath}")]
        public void Post(string repoPath)
        {
            //_context.Database.OpenConnection();

            /*String s = repoPath.Substring(repoPath.IndexOf("github.com%2F") + 13);
            s.Trim();

            //Repo name generator so we can create multiple temp-folders
            string folderPath = "../TestGithubStorage/" + s.Replace("%2F", "-");

            //https://github.com/SpaceVikingEik/assignment-05.git
            //Repository.Clone("https://github.com/VictoriousAnnro/Assignment0.git", "../TestGithubStorage/tempGitRepo");

            //String manipulation bc / gets replaced with %2F so we have to change it back for the method to work
            repoPath = repoPath.Replace("%2F", "/");
            
            if(Directory.Exists(folderPath)){

                //I cant really fathom why this works, but it does update the folder to the newest version of main
                Repository repo = new Repository(folderPath);
                Commands.Pull(repo,new Signature(" d", "d ",new DateTimeOffset()),new PullOptions());

                addRepoCheckToDB(folderPath); //for testing

            } else {
                var path = Repository.Clone(repoPath, folderPath);
                addRepoCheckToDB(folderPath); //check lige ift. folderpath og repopath, hvilken skal vi faktisk bruge?
            }*/
        }


        /*[HttpGet("{repoPath}")]
        public IActionResult GetAnalysis(string repoPath, string analyseMode){
            //_context.Database.OpenConnection();

            String s = repoPath.Substring(repoPath.IndexOf("github.com%2F") + 13);
            s.Trim();

            string folderPath = "../TestGithubStorage/" + s.Replace("%2F", "-");
            //https://github.com/VictoriousAnnro/Assignment0.git

            if(analyseMode.Equals("FQMode")){
                return Ok(CommitFrequencyGet(folderPath));
            } else if (analyseMode.Equals("AuthMode")){
                return Ok(userCommitFreq(folderPath));
            } else return Ok(null);

        }*/

        private List<RepoCheckRepository.comFreqObj> CommitFrequencyGet(string folderPath){
            return _repository.getCommitFreq(folderPath);
        }

        private List<RepoCheckRepository.userComFreqObj> userCommitFreq(string folderPath){
            return _repository.getUserCommitFreq(folderPath);
        }


        //Hvad bruges denne til? gem?
        [HttpPut("{repoPath}")]
        public void Put(RepoCheckUpdateDTO repoCheck)
        { //string repoPath, string newestCheckedCommit
            /*var toUpdate = _context.RepoChecks.Find(repoCheck.repoPath);
            toUpdate!.lastCheckedCommit = repoCheck.lastCheckedCommit;

            var newCons = repoCheck.Contributions.Select(c =>
            ContributionFromContributionDTO(c)).ToList();
            toUpdate.Contributions = newCons;
            //toUpdate.Contributions.ToList()
            //.AddRange(newCons.Except(toUpdate.Contributions.ToList()));

            _context.SaveChanges();*/
        }

    }
}