
using Microsoft.Data.Sqlite;

namespace GitInsight.Test;

public class AuthorRepositoryTests : IDisposable
{
    private readonly CommitTreeContext _context;
    private readonly AuthorRepository _authorRepository;

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
        _authorRepository = new AuthorRepository(_context);
    }

#pragma warning disable

    [Fact]
    public void Create_Should_Create_Author_and_Add_to_Context()
    {
        // Arrange
        var testAuthorString = "Stephen King";

        // Act
        _authorRepository.Create(new AuthorCreateDTO("Stephen King"));

        // Assert
        Assert.Equal(_context.Authors.FirstOrDefault().Name, testAuthorString);
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
        _authorRepository.Create(new AuthorCreateDTO(testAuthorString0));
        _authorRepository.Create(new AuthorCreateDTO(testAuthorString1));
        _authorRepository.Create(new AuthorCreateDTO(testAuthorString2));

        var jens = _authorRepository.ReadAll().ToList();
        var hans = new List<string>();
        foreach (AuthorDTO a in jens) {
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
