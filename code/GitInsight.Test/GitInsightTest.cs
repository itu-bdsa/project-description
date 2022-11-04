using LibGit2Sharp;

namespace GitInsight.Test;
public class GitInsightTest
{

    Data? _repo;


    private void setupTests()
    {
        _repo = new Data("https://github.com/rmccue/test-repository");

    }

    private void closeTest()
    {
        Console.WriteLine("skrrt?");

        _repo.shutDown();

        Console.WriteLine("skrrt");
    }

    [Fact]
    public void Should_load_a_repository_from_path()
    {
        // Given
        setupTests();

        // When

        _repo.print(Mode.AUTHOR);


        // Then
        Assert.NotNull(_repo);
        closeTest();
    }

    [Fact]
    // Purposefully left out for now
    public void Should_be_able_to_update_repo_to_a_new_repo()
    {
        // Given

        // When

        // Then
    }


    [Fact]
    public void User_should_be_able_to_switch_mode_to_Author_Mode()
    {
        // // Given
        // var firstExpected = _repo.Mode.NULL;
        // Assert.Equal(_repo.Mode, firstExpected);

        // // When
        // _repo.Mode = _repo.Mode.AUTHOR;

        // // Then
        // Assert.Equal(_repo.Mode, _repo.Mode);
    }

    [Fact]
    public void Should_print_right_text_when_in_author_mode()
    {
        // Given

        // When

        // Then
    }
    [Fact]
    public void Should_print_right_text_when_in_frequency_mode()
    {
        // Given

        // When

        // Then
    }
}