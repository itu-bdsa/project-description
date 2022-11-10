using System.Collections;
using LibGit2Sharp;
using Microsoft.Data.Sqlite;
using GitInsight.Entities;
using GitInsight.Core;
using GitInsight;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class ContributionRepositoryTests : IDisposable{
    private readonly SqliteConnection _connection;
    private readonly GitInsightContext _context;
    private readonly ContributionRepository _repository;
    private readonly GitInsightController _repoCheckRepository;

        public ContributionRepositoryTests(){
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        var builder = new DbContextOptionsBuilder<GitInsightContext>().UseSqlite(_connection);
        _context = new GitInsightContext(builder.Options);
        _context.Database.EnsureCreated();

        _context.SaveChanges();

        _repository = new ContributionRepository(_context);
        _repoCheckRepository = new GitInsightController(_context);
    }

     public void Dispose()
    {
        _context.Dispose();
        _connection.Dispose();
    }

    /*[Fact]
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
        var Con1 = new ContributionCreateDTO(RepoPath: "kekw", Author: "monke", Date: DateTime.Today, CommitsCount: 3);
        
        //Act
        _repository.Create(Con1);
        var expected = new ContributionDTO(RepoPath: "kekw", Author: "monke", Date: DateTime.Today, CommitsCount: 3);
        var actual = _repository.Read("kekw", "monke", DateTime.Today);

        //Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Update_should_change_commitsCount_to_3(){
        //Arrange
        var Con1 = new ContributionCreateDTO(RepoPath: "kekw", Author: "monke", Date: DateTime.Today, CommitsCount: 3);
        _repository.Create(Con1);

        //Act
        var updateDTO = new ContributionUpdateDTO(RepoPath: "kekw", Author: "monke", Date: DateTime.Today,
                                        NewCommitsCount: 2);

       _repository.Update(updateDTO);
        var expected = new ContributionDTO(RepoPath: "kekw", Author: "monke", Date: DateTime.Today, CommitsCount: 5);
        var actual = _repository.Read("kekw","monke", DateTime.Today);
        //Assert
        actual.Should().Be(expected);
    }

 

    /*[Fact]
    public void FirstAnalyzeShouldCreateEntryInDB()
    {
        //Arrange
        var repoPath = @"C:\Users\annem\Skrivebord\BDSA\BDSA_PROJECT\TestGithubStorage\assignment-05";
        var expectedList = new List<ContributionDTO>();
        int nrCommits = 0;

        //Act
        var commits = GitInsightClass.CommitFrequencyMode(repoPath, _repoCheckRepository); //laver entry

        var repo = new Repository(repoPath);
        var logs = repo.Commits.ToList();

        //want to count commits, group by author
        var query = repo.Commits
                .GroupBy(c => new { c.Author, c.Author.When })
                .Select(a => new { grouping = a.Key, count = a.Count()}) //key name & when
                .ToList();

        //expected commits
        foreach (var l in logs) {
            nrCommits = query.Where(a => a.grouping.Author.Equals(l.Author)
            && a.grouping.When.Equals(l.Author.When))
            .Select(a => a.count).First();

            var con = new ContributionDTO(RepoPath: repoPath,
                                 Author: l.Author.ToString(), 
                                 Date: l.Author.When.Date, 
                                 CommitsCount: nrCommits);
            expectedList.Add(con);
        }
        //actual
        var actual = _repository.ReadAllforRepo(repoPath);
        
        //repo.Commits.Select().GroupBy(c => c.Count() c.Author).Count();
        //repo.Commits.Select(t => t.Author t => t.Id).Distinct();
        //repo.Commits.GroupBy(c => c.Author).SelectMany(c => c.au);
        
        //Actual
        actual.Equals(expectedList);

    }*/


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