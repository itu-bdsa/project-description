using System.Reflection;
using LibGit2Sharp;

namespace GitInsight.Test;
public class GitInsightTest
{

    Data? _repo;
    Control _app;


    private void SetupTests()
    {
        _repo = new Data("https://github.com/rmccue/test-repository");
        _app = new Control();


    }
    private void SetupTests(string url)
    {
        _repo = new Data(url);

    }

    private void CloseTest()
    {
        _repo.Shutdown();
    }

    [Fact(Skip="a")]
    public void Should_load_a_repository_from_path()
    {
        // Given
        SetupTests();

        // When

        // Then
        Assert.NotNull(_repo);
        CloseTest();
    }

    [Fact(Skip="b")]
    // Purposefully left out for now
    public void Should_be_able_to_update_repo_to_a_new_repo()
    {
        // Given
        SetupTests();
        System.IO.DirectoryInfo di = new DirectoryInfo("./repoData/deleteMe");
        var diLength = di.GetFiles().Length;
        Assert.Equal(diLength, 2);

        // When
        SetupTests("https://github.com/thekure/chittychat_skrrrt");
        diLength = di.GetFiles().Length;


        // Then
        Assert.Equal(diLength, 6);
        CloseTest();
    }


    [Fact(Skip="c")]
    public void User_should_be_able_to_switch_mode_to_Author_Mode()
    {
        // // Given
        SetupTests();
        var firstExpected = Mode.NULL;
        Assert.Equal(_app.GetMode(), firstExpected);

        // // When
        _app.SetMode(Mode.AUTHOR);

        // // Then
        Assert.Equal(_app.GetMode(), Mode.AUTHOR);
        CloseTest();
    }

    [Fact(Skip="d")]
    public void Should_print_right_text_when_in_author_mode()
    {
        SetupTests();
        // Given
        TextWriter currentOut = Console.Out;
        using var writer = new StringWriter();
        Console.SetOut(writer);

        // When
        _repo.Print(Mode.AUTHOR);

        // Then
        var output = writer.GetStringBuilder().ToString().TrimEnd();
        var outputShouldBe = "Author: \n\n--- Ryan McCue ---\n3, 24/08/2008";
        //Author: 
        //
        //--- Ryan McCue ---
        //3, 24/08/2008
        Assert.Equal(output, outputShouldBe);
        CloseTest();
        Console.SetOut(currentOut);
    }
    [Fact(Skip="e")]
    public void Should_print_right_text_when_in_frequency_mode()
    {
        // Given
        SetupTests();
        // Given
        TextWriter currentOut = Console.Out;
        using var writer = new StringWriter();
        Console.SetOut(writer);

        // When
        _repo.Print(Mode.FREQUENCY);

        // Then
        var output = writer.GetStringBuilder().ToString().TrimEnd();
        var outputShouldBe = "Frequency:\n24/08/2008, 3";
        //Frequency:
        //24/08/2008, 3
        Assert.Equal(output, outputShouldBe);
        Console.SetOut(currentOut);
        CloseTest();
    }

    [Fact]
    public void testToDelete(){
        var factory = new CommitTreeContextFactory();
        var context = factory.CreateDbContext(new string[1]);
        var authorRepo = new AuthorRepository(context);;

        authorRepo.Create(new AuthorCreateDTO("hej"));
        Assert.True(true);
    }
}