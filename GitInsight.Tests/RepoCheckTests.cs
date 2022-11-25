namespace GitInsight.Tests;
using GitInsight.Core;
using LibGit2Sharp;
using Microsoft.Data.Sqlite;
using GitInsight.Entities;

public class RepoCheckTests{
    private readonly SqliteConnection _connection;
    private readonly GitInsightContext _context;
    private readonly GitInsightController _GitController;
    private readonly RepoCheckRepository _repository;
    private readonly string folderPath;

    public RepoCheckTests() {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        var builder = new DbContextOptionsBuilder<GitInsightContext>().UseSqlite(_connection);
        _context = new GitInsightContext(builder.Options);
        _context.Database.EnsureCreated();

        _context.SaveChanges();

        _GitController = new GitInsightController(_context);
        _repository = new RepoCheckRepository(_context);

        folderPath = @"C:\Users\annem\Desktop\BDSA_PROJECT\TestGithubStorage\VictoriousAnnro-Assignment0.git";
        //below doesnt work for some goddamn reason and i dont know why. YES i tried using the other backslash
        //"../TestGithubStorage/VictoriousAnnro-Assignment0.git";
    }

    public void Dispose() {
        _context.Dispose();
        _connection.Dispose();
    }

    [Fact]
    public void FirstAnalyzeCreatesEntry(){

        //Arrange
        _repository.CreateEntryInDB(folderPath);
        var repo = new Repository(folderPath);

        //Act
        var expectedCommitId = repo.Commits.First().Id.ToString();
        var conDTOs = _repository.AddContributionsDataToSet(repo);

        var expectedRepoCheckObj = new RepoCheckDTO(folderPath, expectedCommitId, conDTOs);

       var actualEntry = _repository.Read(folderPath);

        //Assert
        Assert.Equal(actualEntry.Contributions, expectedRepoCheckObj.Contributions);
        actualEntry.repoPath.Should().Be(expectedRepoCheckObj.repoPath);
        actualEntry.lastCheckedCommit.Should().Be(expectedRepoCheckObj.lastCheckedCommit);
    }

    [Fact]
    public void FirstCommitCountForCommitFreqShouldBe_2Sep2022_8Commits(){

        //Arrange
        _repository.CreateEntryInDB(folderPath);
        var repo = new Repository(folderPath);

        //Act
        var expected = new RepoCheckRepository.comFreqObj("02-09-2022 00:00:00", 8);
        var actual = _repository.getCommitFreq(folderPath).Last();

        //Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void FirstCommitCountForUserCommitFreqShouldBe_AnneMarie_2Sep2022_5Commits(){

        //Arrange
        _repository.CreateEntryInDB(folderPath);
        var repo = new Repository(folderPath);

        //Act
        var tempList = new List<RepoCheckRepository.dateCommits>{new RepoCheckRepository.dateCommits("02-09-2022 00:00:00",5)};
        var author = "Anne-Marie <annemarierommerdahl@gmail.com>";
        var expected = new RepoCheckRepository.userComFreqObj(author, tempList);

        var actual = _repository.getUserCommitFreq(folderPath).Last();

        //Assert
        actual.author.Should().Be(expected.author);
        Assert.Equal(actual.datesCommits, expected.datesCommits);
    }

    [Fact]
    public void LastCheckedCommit_ShouldBeMostRecent(){

        //Arrange
        _repository.CreateEntryInDB(folderPath);
        var repo = new Repository(folderPath);

        //Act
        var expectedCommitId = repo.Commits.First().Id.ToString();

       var actualEntry = _repository.Read(folderPath);

        //Assert
        actualEntry.lastCheckedCommit.Should().Be(expectedCommitId);
    }

    [Fact]
    public void UpdatedEntriesInDBTest(){ 

        //Arrange
        //make fake repocheck with real foldername path
        var repo = new Repository(folderPath);
        var fakeRepoCheck = new RepoCheck
                    {
                        repoPath = folderPath,
                        lastCheckedCommit = "fakeCommitIDHehee",
                        Contributions = new HashSet<Contribution>(),
                    };

        _context.RepoChecks.Add(fakeRepoCheck);

        //Act
        _repository.Update(folderPath);


        var expectedCommitId = repo.Commits.First().Id.ToString();
        var conDTOs = _repository.AddContributionsDataToSet(repo);
        var expected = new RepoCheckDTO(folderPath, expectedCommitId, conDTOs);

        var actual = _repository.Read(folderPath);

        //Assert
        Assert.Equal(actual.Contributions, expected.Contributions);
        actual.repoPath.Should().Be(expected.repoPath);
        actual.lastCheckedCommit.Should().Be(expected.lastCheckedCommit);
    }
}