namespace GitInsight.Tests;

public class WebApiTests{

    private readonly SqliteConnection _connection;
    private readonly GitInsightContext _context;
    private readonly GitInsightController _GitController;
    private readonly string repoPath;

    public WebApiTests() {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        var builder = new DbContextOptionsBuilder<GitInsightContext>().UseSqlite(_connection);
        _context = new GitInsightContext(builder.Options);
        _context.Database.EnsureCreated();

        _context.SaveChanges();

        _GitController = new GitInsightController(_context);

        repoPath = @"https://github.com/VictoriousAnnro/Assignment0.git";
    }

    public void Dispose() {
        _context.Dispose();
        _connection.Dispose();
    }

    [Fact]
    public void comFreqTest()
    {
        //Arrange
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
    public void userComFreqTest()
    {
        //Arrange
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