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
        private GitFileAnalyzer _analyzer;

        public GitInsightController(GitInsightContext context)
        {
            _context = context;
            _repository =  new RepoCheckRepository(context);
            _analyzer = new GitFileAnalyzer();
            _context.Database.OpenConnection();
        }

        //new method to put Post and Get together
        [HttpGet("{repoPath}")]
        public IActionResult GetAnalysis(string repoPath, string analyseMode){
            
            //Repo name generator so we can create multiple temp-folders
            string folderPath = "../TestGithubStorage/" + repoPath.Replace("%2F", "-");

            //String manipulation bc / gets replaced with %2F so we have to change it back for the method to work
            repoPath = repoPath.Replace("%2F", "/");
            repoPath = "https://github.com/" + repoPath;

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
            } else return Ok(fileChangeList(folderPath));

        }

        //--Helper method to GetAnalysis()-----
        private void addRepoCheckToDB(string repoPath){
            //create repocheck and add in _repository
            _repository.CreateEntryInDB(repoPath);
        }
        //-----------------------------------

        private List<RepoCheckRepository.comFreqObj> CommitFrequencyGet(string folderPath){
            return _repository.getCommitFreq(folderPath);
        }

        private List<RepoCheckRepository.userComFreqObj> userCommitFreq(string folderPath){
            return _repository.getUserCommitFreq(folderPath);
        }

        private List<GitFileAnalyzer.FileAndNrChanges> fileChangeList(string folderPath){
            var repo = new Repository(folderPath);
            return _analyzer.getFilesAndNrChanges(repo);
        }

    }
}