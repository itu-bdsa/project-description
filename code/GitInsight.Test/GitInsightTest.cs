using System.Reflection;
using LibGit2Sharp;

namespace GitInsight.Test;
public class GitInsightTest
{

    Data? _repo;


    private void setupTests()
    {
        _repo = new Data("https://github.com/rmccue/test-repository");

    }
    private void setupTests(string url)
    {
        _repo = new Data(url);

    }

    private void closeTest()
    {
        _repo.shutDown();
    }

    [Fact]
    public void Should_load_a_repository_from_path()
    {
        // Given
        setupTests();

        // When

        // Then
        Assert.NotNull(_repo);
        closeTest();
    }

    [Fact]
    // Purposefully left out for now
    public void Should_be_able_to_update_repo_to_a_new_repo()
    {
        // Given
        setupTests();
        System.IO.DirectoryInfo di = new DirectoryInfo("./repoData/deleteMe");
        var diLength = di.GetFiles().Length;
        Assert.Equal(diLength, 2);

        // When
        setupTests("https://github.com/thekure/chittychat_skrrrt");
        diLength = di.GetFiles().Length;


        // Then
        Assert.Equal(diLength, 6);
        closeTest();
    }


    [Fact]
    public void User_should_be_able_to_switch_mode_to_Author_Mode()
    {
        // // Given
        setupTests();
        var firstExpected = Mode.NULL;
        Assert.Equal(_repo.getMode(), firstExpected);

        // // When
        _repo.setMode(Mode.AUTHOR);

        // // Then
        Assert.Equal(_repo.getMode(), Mode.AUTHOR);
        closeTest();
    }

    [Fact]
    public void Should_print_right_text_when_in_author_mode()
    {
        setupTests();
        // Given
        TextWriter currentOut = Console.Out;
        using var writer = new StringWriter();
        Console.SetOut(writer);

        // When
        _repo.print(Mode.AUTHOR);

        // Then
        var output = writer.GetStringBuilder().ToString().TrimEnd();
        var outputShouldBe = "Author: \n\n--- Ryan McCue ---\n3, 24/08/2008";
        //Author: 
        //
        //--- Ryan McCue ---
        //3, 24/08/2008
        Assert.Equal(output, outputShouldBe);
        closeTest();
        Console.SetOut(currentOut);
    }
    [Fact]
    public void Should_print_right_text_when_in_frequency_mode()
    {
        // Given
        setupTests();
        // Given
        TextWriter currentOut = Console.Out;
        using var writer = new StringWriter();
        Console.SetOut(writer);

        // When
        _repo.print(Mode.FREQUENCY);

        // Then
        var output = writer.GetStringBuilder().ToString().TrimEnd();
        var outputShouldBe = "Frequency:\n24/08/2008, 3";
        //Frequency:
        //24/08/2008, 3
        Assert.Equal(output, outputShouldBe);
        Console.SetOut(currentOut);
        closeTest();
    }
}