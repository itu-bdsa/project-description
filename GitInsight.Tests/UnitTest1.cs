namespace GitInsight.Tests;
using System.Diagnostics;
using LibGit2Sharp;
//using (var repo = new Repository(@"/Users/sushi/code/sushi/Xamarin.Forms.Renderer.Tests"))


//søndag d.25 setember - 2 commits
//Torsdag d.6 october - 6 commits
//Fredag d.7 october - 3 commits
//Lørdag d.8 october - 10 commits
//Søndag d.9 october - 1 commits
//Torsdag d.13 october - 3 commits

public class UnitTest1
{
    [Fact]
    public void GetListOfAllCommits()
    {
        //Arrange
        var repoPath = @"C:\Users\whoever\Documents\git\SomeRepoName";
        var fileOffset = @"YouTube\subscriptions\subscriptions.json";
        var fileOffsetFwdSlash = fileOffset.Replace("\\", "/");
        using (var repo = new Repository(repoPath))
        {
            var logs = repo.Commits.QueryBy(fileOffsetFwdSlash).ToList();
            foreach (var log in logs)
            {
                Debug.WriteLine(log.Commit.Author.When);
            }
        }
        //Act

        //Actual

    }
}