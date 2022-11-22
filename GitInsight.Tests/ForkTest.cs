namespace GitInsight.Tests;

using GitInsight.Entities;

public class ForkTest{

    [Fact]
    public void forkFromRepo(){

        //Arrange

        var expectedFork1 = new GitInsight.Entities.RepoFork.RepoForkObj("BDSA00","Nooja1012","https://github.com/Nooja1012/BDSA00");
        var expectedFork2 = new GitInsight.Entities.RepoFork.RepoForkObj("AMsForkU","VictoriousAnnro","https://github.com/VictoriousAnnro/AMsForkU");
        
        var expectedList = new List<GitInsight.Entities.RepoFork.RepoForkObj>(){expectedFork1, expectedFork2};
        
        //Act
        var y = RepoFork.getRepoForks("Divik-kid/BDSA00").GetAwaiter().GetResult();
        //Assert
        y.Should().BeEquivalentTo(expectedList);
    }
}