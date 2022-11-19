// using System.Reflection;
// using LibGit2Sharp;

// namespace Tests;
// public class GitInsightTest
// {

//     Data? _repo;
//     Control _app;


//     private void SetupTests()
//     {
//         _repo = new Data("https://github.com/rmccue/test-repository");
//         _app = new Control();


//     }
//     private void SetupTests(string url)
//     {
//         _repo = new Data(url);

//     }

//     private void CloseTest()
//     {
//         _repo.Shutdown();
//     }

//     [Fact]
//     public void Should_load_a_repository_from_path()
//     {
//         // Given
//         SetupTests();

//         // When

//         // Then
//         Assert.NotNull(_repo);
//         CloseTest();
//     }

//     [Fact]
//     // Purposefully left out for now
//     public void Should_be_able_to_update_repo_to_a_new_repo()
//     {
//         // Given
//         SetupTests();
//         System.IO.DirectoryInfo di = new DirectoryInfo("./repoData/deleteMe");
//         var diLength = di.GetFiles().Length;
//         Assert.Equal(diLength, 2);

//         // When
//         SetupTests("https://github.com/thekure/chittychat_skrrrt");
//         diLength = di.GetFiles().Length;


//         // Then
//         Assert.Equal(diLength, 6);
//         CloseTest();
//     }


//     [Fact]
//     public void User_should_be_able_to_switch_mode_to_Author_Mode()
//     {
//         // // Given
//         SetupTests();
//         var firstExpected = Modes.NULL;
//         Assert.Equal(_app.GetMode(), firstExpected);

//         // // When
//         _app.SetMode(Modes.AUTHOR);

//         // // Then
//         Assert.Equal(_app.GetMode(), Modes.AUTHOR);
//         CloseTest();
//     }

//     [Fact]
//     public void Should_print_right_text_when_in_author_mode()
//     {
//         SetupTests();
//         // Given
//         TextWriter currentOut = Console.Out;
//         using var writer = new StringWriter();
//         Console.SetOut(writer);

//         // When
//         _repo.Print(Modes.AUTHOR);

//         // Then
//         var output = writer.GetStringBuilder().ToString().TrimEnd();
//         var outputShouldBe = "Author: \n\n--- Ryan McCue ---\n3, 24/08/2008";
//         //Author: 
//         //
//         //--- Ryan McCue ---
//         //3, 24/08/2008
//         Assert.Equal(output, outputShouldBe);
//         CloseTest();
//         Console.SetOut(currentOut);
//     }
//     [Fact]
//     public void Should_print_right_text_when_in_frequency_mode()
//     {
//         // Given
//         SetupTests();
//         // Given
//         TextWriter currentOut = Console.Out;
//         using var writer = new StringWriter();
//         Console.SetOut(writer);

//         // When
//         _repo.Print(Modes.FREQUENCY);

//         // Then
//         var output = writer.GetStringBuilder().ToString().TrimEnd();
//         var outputShouldBe = "Frequency:\n24/08/2008, 3";
//         //Frequency:
//         //24/08/2008, 3
//         Assert.Equal(output, outputShouldBe);
//         Console.SetOut(currentOut);
//         CloseTest();
//     }
// }