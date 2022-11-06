using System.Collections;
using LibGit2Sharp;
using Microsoft.Data.Sqlite;
using GitInsight.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class ContributionRepositoryTests : IDisposable{
    private readonly SqliteConnection _connection;
    private readonly GitInsightContext _context;
    private readonly ContributionRepository _repository;

        public ContributionRepositoryTests(){
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        var builder = new DbContextOptionsBuilder<GitInsightContext>().UseSqlite(_connection);
        _context = new GitInsightContext(builder.Options);
        _context.Database.EnsureCreated();

        _context.SaveChanges();

        _repository = new ContributionRepository(_context);
    }

     public void Dispose()
    {
        _context.Dispose();
        _connection.Dispose();
    }

    /*
    Tænker vi kan lave metoder i GitInsight til at lave shiet i contributions,
    og teste dem individuelt, før vi tester vores to modes som helhed,
    og så ændrer vores orig. tests til at passe nye kode*/

    [Fact]
    public void Database_Should_be_empty()
    {
        //Arrange
        
        //Act
        
        //Assert
        Assert.Equal(_context.Contributions.First(),null);
    }

    [Fact]
    public void insert_contribution_Should_exist()
    {
        //Arrange
        //var Con1 = new Contribution(){ Id = 1, repoPath = "kekw", author = "monke", date = DateTime.Today, commitsCount = 3};
        
        //Act
        _repository.Create(repoPath: "kekw", author: "monke", date: DateTime.Today, commitsCount: 3);

        //Assert
        //Assert.Equal(_context.Contributions.First(), Con1);
    }


/*
    [Fact]
    public void FirstAnalyzeShouldCreateEntryInDBRepositories()
    {
        //Arrange
        var repoPath = @"C:\Users\annem\Skrivebord\BDSA\BDSA_PROJECT\TestGithubStorage\assignment-05";

        //Act
        var commits = GitInsight.CommitFrequencyMode(); //laver entry

        var repo = new Repository(repoPath);
        var logs = repo.Commits.ToList();
        var expected = logs.Last().Id;
        //last? element in list is last commit - expected state
        
        //Actual
        //check om id for commits er samme
        _context.Repositories.Find(repoPath).StateCommit.Should().Be(expected);

    }

*/
    /*[Fact]
    public void IsRepositoryReanalyzedAndUpdated(){
            //Arrange
            var repo = new Repository();
            var Con1 = new Contribution(){ Id = 1, repoPath = "kekw", author = "idk", date = DateTime.Today, commitsCount = 3};
            var Con2 = new Contribution(){ Id = 1, repoPath = "kekw", author = "idk", date = DateTime.Today, commitsCount = 5};
            
            //Act 
           _context.Contributions.AddRange(Con1);
           _context.Contributions.AddRange(Con2);
           GitInsight.Entities.ContributionRepository.Update(_repository);
            
            //Actual
            Assert.Equal(_repository.Find(5),Con2);
     
    }*/

 

}