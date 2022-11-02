namespace GitInsight.Entities;

public class CommitSignatureRepoTests: IDisposable
{

    private GitInsightContext _context;
    private CommitSignatureRepository _repo;


    //whenever a test is run the constructor creates a CommitSignatureRepoTests object and a in-memory database"

    public CommitSignatureRepoTests(){
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<GitInsightContext>();
        builder.UseSqlite(connection);

        var context = new GitInsightContext(builder.Options);
        context.Database.EnsureCreated();
        _context = context;
        _repo = new CommitSignatureRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
        _repo.Dispose();
    }

    [Fact]
    public void Inserting_CommitSignature_into_database()
    {
       //Arrange
       var sig = new GitInsight.Entities.CommmitSignature{Name="Monica",Email="test@itu.dk",Date=new DateTimeOffset()};

       //Act
       _context.Signatures.Add(sig);
        _context.SaveChanges();

       //Assert
       Assert.True(_context.Signatures.Count() == 1);
    }

}


