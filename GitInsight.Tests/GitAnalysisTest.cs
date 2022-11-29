namespace GitInsight.Tests;

public class GitAnalysisTest{
    Repository _repo;
    public GitAnalysisTest(){
        var l = Directory.GetParent(Environment.CurrentDirectory)!.ToString();
        string path = Path.Combine(l, @"TestGithubStorage/VictoriousAnnro-TestRep.git");
        if(!Directory.Exists(path)){
            //Console.WriteLine("DOESNT exist!");
            var clonePath = Repository.Clone("https://github.com/VictoriousAnnro/TestRep.git", path);
        }
        _repo = new Repository(path);
        Commands.Pull(_repo,new Signature(" d", "d ",new DateTimeOffset()),new PullOptions());
    }

    [Fact]
    public void ListOfChangesShouldBeProgram2_Tester1(){
        
        //Arrange
        var analyser = new GitFileAnalyzer();

        //Act
        var expected = new List<GitFileAnalyzer.FileAndNrChanges>();
        expected.Add(new GitFileAnalyzer.FileAndNrChanges("Program.cs", 2));
        expected.Add(new GitFileAnalyzer.FileAndNrChanges("Tester.cs", 1));
        
        var actual = analyser.getFilesAndNrChanges(_repo);

        //Assert
        actual.Equals(expected);
    }


    [Fact]
    public void MostFreqChangedFileProgramcsW2Changes(){
        
        //Arrange
        var analyser = new GitFileAnalyzer();
        var list = analyser.getFilesAndNrChanges(_repo);

        //Act
        var expected = new GitFileAnalyzer.FileAndNrChanges("Program.cs", 2);
        var actual = analyser.getMostFreqChangedFile(list);

        //Assert
        actual.Should().Be(expected);
    }
}