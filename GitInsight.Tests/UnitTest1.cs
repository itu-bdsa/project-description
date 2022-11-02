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
    public void TheTestGithubIsStillTheSame()
    {
        //Arrange
        var repoPath = @"C:\Users\eikbo\Skrivebord\BDSA\BDSA_PROJECT\TestGithubStorage\assignment-05";
        var counter = 0;

        //Act
        using (var repo = new Repository(repoPath))
        {
            var logs = repo.Commits.ToList();
            foreach (var log in logs)
            {
                counter++;
            }
        }
        //Actual

        Assert.Equal(31, counter);

    }


        [Fact]
    public void commitFrequencyModeWorks()
    {
        //Arrange
        var counter = 0;
        var dateArray = GitInsight.commitFrequencyMode();
        //Act
        
        foreach (var item in dateArray)
        {
            
        }

        //Actual

        Assert.Equal(31, counter);

    }

           [Fact]
    public void commitUserFrequencyModeWorks()
    {
        //Arrange
        var repoPath = @"C:\Users\eikbo\Skrivebord\BDSA\BDSA_PROJECT\TestGithubStorage\assignment-05";
        var counter = 0;



        //Act
        using (var repo = new Repository(repoPath))
        {
            var logs = repo.Commits.ToList();
            foreach (var log in logs)
            {
                counter++;
            }
        }
        //Actual

        Assert.Equal(31, counter);

    }
}