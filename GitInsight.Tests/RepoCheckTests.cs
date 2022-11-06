namespace GitInsight.Tests;
//using System.Collections;
using GitInsight.Core;
using LibGit2Sharp;
using Microsoft.Data.Sqlite;
//using Microsoft.EntityFrameworkCore.Infrastructure;

// Placeholder
public class DbTests{
    private readonly SqliteConnection _connection;
    private readonly GitInsightContext _context;
    private readonly RepoCheckRepository _repository;
    private readonly string repoPath;

    public DbTests() {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        var builder = new DbContextOptionsBuilder<GitInsightContext>().UseSqlite(_connection);
        _context = new GitInsightContext(builder.Options);
        _context.Database.EnsureCreated();

        _context.SaveChanges();

        _repository = new RepoCheckRepository(_context);

        repoPath = @"C:\Users\annem\Desktop\BDSA_PROJECT\TestGithubStorage\assignment-05";
    }

    public void Dispose() {
        _context.Dispose();
        _connection.Dispose();
    }

    [Fact]
    public void FirstAnalyzeCreatesNewRepoChecksEntry(){
        //rewrite to fit w. new one to many relationship

        //Arrange
        var l = GitInsightClass.CommitFrequencyMode(repoPath, _repository);

        var repo = new Repository(repoPath);

        //Act
        var expectedCommitId = repo.Commits.First().Id.ToString();

        /*var commitCounts = repo.Commits.GroupBy(c => c.Author, c => c.Author.When)
                        .Select(t => t.Count());
        var expectedComCount = commitCounts.First(); //check om rigtigt
        //repo.Commits.Count();

        var expectedFirstContribution = new Contribution {
            Id = 0,
            repoPath = repoPath,
            author = repo.Commits.First().Author.ToString(),
            date = repo.Commits.First().Author.When.As<DateTime>(),
            commitsCount = expectedComCount,
            repCheck = _repository.Read(repoPath),

        };

        var expectedContributions = new HashSet<Contribution>(){};*/

        var actualEntry = _repository.Read(repoPath);

        //Assert
        //actual.Should().Be(expected); cant use without use of record type
        actualEntry.lastCheckedCommit.Should().Be(expectedCommitId);
        //Assert.Equal(expected: , actual: );

        //check attached contri. 
        //var actual = _context.Tasks.Find(task.Id).Tags.Select(t => t.Name);
    }

        [Fact]
    public void CheckCommitMostRecentShouldBeTrue(){

        //Arrange
        var l = GitInsightClass.CommitFrequencyMode(repoPath, _repository);

        //Act
        var actual = GitInsightClass.CommitIsNewest(repoPath, _repository);

        //Assert
        actual.Should().Be(true);
    }

        [Fact]
    public void CheckCommitMostRecentShouldBeFalse(){ 

        //Arrange
        var repo = new Repository(repoPath);
        //set first (Last i Commits collection) commit made as last checked commit
        var wrongCommitRepoCheck = new RepoCheckCreateDTO(repoPath, 
                                    repo.Commits.Last().Id.ToString(),
                                    new List<ContributionDTO>{});
        _repository.Create(wrongCommitRepoCheck);

        //Act
        var actual = GitInsightClass.CommitIsNewest(repoPath, _repository);

        //Assert
        Assert.False(actual);
    }

        [Fact]
    public void OutdatedCommitShouldBeUpdatedToNewest(){ 

        //Arrange
        var repo = new Repository(repoPath);
        var OutdatedRepoCheck = new RepoCheckCreateDTO(repoPath, 
                                    repo.Commits.Last().Id.ToString(),
                                    new List<ContributionDTO>{});
        _repository.Create(OutdatedRepoCheck);

        //Act
        GitInsightClass.UpdateCheckedCommitInDb(repoPath, _repository);
        var expected = repo.Commits.First().Id.ToString();
        var actual = _repository.Read(repoPath).lastCheckedCommit;

        //Assert
        actual.Should().Be(expected);
    }

}