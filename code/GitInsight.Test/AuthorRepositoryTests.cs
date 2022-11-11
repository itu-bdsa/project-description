
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
        // // Arrange
        var testAuthorString = "Stephen Queen";

        _authorRepository.Create(new AuthorCreateDTO("Stephen Queen"));

        Assert.Equal(_context.Authors.FirstOrDefault().Name, testAuthorString);

        // var (userResponse, userId) = _userRepository.Create(new UserCreateDTO("HansGruber", "hansgruber@gmail.com"));
        // // var (tagResponse, tagId) = _tagRepository.Create(new TagCreateDTO("testTag"));
        // // var (taskResponse, taskId) = _taskRepository.Create(new TaskCreateDTO ("testTask", 0, null, tasksListOfTags));

        // // Act
        // var (testResponse, testId) = _taskRepository.Create(new TaskCreateDTO("taskTitle", 0, null, new List<string>()));

        // // Assert
        // testResponse.Should().Be(Response.Created);
        // testId.Should().Be(1);


        // public void Create (AuthorCreateDTO author) {

        // var entry = new Author(author.Name);
        // context.Authors.Add(entry);
        // context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
