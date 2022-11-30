namespace GitInsight.Tests;
using System.Collections.Generic;
using GitInsight.Entities;
using Microsoft.Extensions.Configuration;

public class ForkTest{

IConfiguration Configuration { get; set; }
        public ForkTest()
        {
            // the type specified here is just so the secrets library can 
            // find the UserSecretId we added in the csproj file
            var builder = new ConfigurationBuilder().AddUserSecrets<ForkTest>();

            Configuration = builder.Build();
        }

    [Fact]
    public void forkFromRepo(){

        //Arrange
        var token = Configuration["Git:HubToken"];

        var expectedFork1 = new GitInsight.Entities.RepoFork.RepoForkObj("BDSA00","Nooja1012","https://github.com/Nooja1012/BDSA00");
        var expectedFork2 = new GitInsight.Entities.RepoFork.RepoForkObj("AMsForkU","VictoriousAnnro","https://github.com/VictoriousAnnro/AMsForkU");
        
        var expectedList = new List<GitInsight.Entities.RepoFork.RepoForkObj>(){expectedFork1, expectedFork2};
        
        //Act
        var y = RepoFork.getRepoForks("Divik-kid/BDSA00",token).GetAwaiter().GetResult();
        //Assert
        y.Should().BeEquivalentTo(expectedList);
    }

    
    [Fact]
    public void Invalid_repo_returns_empty_collection(){

        //Arrange
        var token = Configuration["Git:HubToken"];

        //Act
        var y = RepoFork.getRepoForks("THIS_IS_NOT_A_REPOSITORY", token).GetAwaiter().GetResult();
        //Assert
        y.Should().BeEmpty();
    }

    [Fact]
    public void Invalid_token_returns_empty_collection(){

        //Arrange

        //Act
        var y = RepoFork.getRepoForks("Divik-kid/BDSA00", "THIS_IS_NOT_A_TOKEN").GetAwaiter().GetResult();
        //Assert
        y.Should().BeEmpty();
    }

       [Fact]
    public void null_inputs_returns_null_collection(){

        //Arrange

        //Act
        var y = RepoFork.getRepoForks(null, null).GetAwaiter().GetResult();
        //Assert
        y.Should().BeEmpty();
    }

}