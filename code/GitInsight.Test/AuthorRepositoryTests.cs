
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
        var testAuthorString = "Stephen Queen";

        // Act
        _authorRepository.Create(new AuthorCreateDTO("Stephen Queen"));

        // Assert
        Assert.Equal(_context.Authors.FirstOrDefault().Name, testAuthorString);
    }



    public void Dispose()
    {
        _context.Dispose();
    }
}
