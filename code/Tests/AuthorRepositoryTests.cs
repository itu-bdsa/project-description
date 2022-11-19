namespace Tests;

public class AuthorRepositoryTests : IDisposable
{
    private readonly CommitTreeContext _context;
    private readonly AuthorDataRepository _authorRepository;

    public AuthorRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<CommitTreeContext>();
        builder.UseSqlite(connection);
        var context = new CommitTreeContext(builder.Options);
        context.Database.EnsureCreated();
        context.SaveChanges();

        _context = context;
        _authorRepository = new AuthorDataRepository(_context);
    }

#pragma warning disable

    [Fact]
    public void Create_Should_Create_Author_and_Add_to_Context()
    {
        // Arrange
        var testAuthorString = "Stephen King";

        // Act
        _authorRepository.Create(new AuthorDataCreateDTO("Stephen King"));

        // Assert
        Assert.Equal(_context.allAuthorData.FirstOrDefault().Name, testAuthorString);
    }

    [Fact]
    public void ReadAll_Should_Return_List_Size_3()
    {
        // Arrange
        var testAuthorString0 = "JK Rowling";
        var testAuthorString1 = "JRR Tolkien";
        var testAuthorString2 = "GRR Martin";
        var listOfTestAuthorStrings = new List<string>(){testAuthorString0, testAuthorString1, testAuthorString2};

        // Act
        _authorRepository.Create(new AuthorDataCreateDTO(testAuthorString0));
        _authorRepository.Create(new AuthorDataCreateDTO(testAuthorString1));
        _authorRepository.Create(new AuthorDataCreateDTO(testAuthorString2));

        var jens = _authorRepository.ReadAll().ToList();
        var hans = new List<string>();
        foreach (AuthorDataDTO a in jens) {
            hans.Add(a.Name);
        }

        // Assert
        Assert.Equal(hans, listOfTestAuthorStrings);
    }



    public void Dispose()
    {
        _context.Dispose();
    }
}
