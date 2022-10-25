namespace GitInsight.Tests;

using LibGit2Sharp;
using GitInsight;

public class CommitTests: IDisposable{

    private Repository repo;

    public CommitTests(){
        //creates repository through libGit2Sharp
        var path = Repository.Init(".");

        //creates a repository object from the path above
        repo = new Repository(path);


        Signature person1 = new Signature("Person1", "person1@itu.dk", new DateTimeOffset(new DateTime(2022,10,25)));
        Signature person3 = new Signature("Person3", "person2@itu.dk", new DateTimeOffset(new DateTime(2022,10,25)));
        Signature person2 = new Signature("Person2", "person2@itu.dk", new DateTimeOffset(new DateTime(2022,10,25)));

        Signature person12 = new Signature("Person1", "person1@itu.dk", new DateTimeOffset(new DateTime(2022,11,05)));
        Signature person123 = new Signature("Person1", "person1@itu.dk", new DateTimeOffset(new DateTime(2022,11,05)));

        Signature person31 = new Signature("Person3", "person2@itu.dk", new DateTimeOffset(new DateTime(2022,11,02)));
        Signature person32 = new Signature("Person3", "person2@itu.dk", new DateTimeOffset(new DateTime(2022,11,03)));
       
        //creates a repository object from the path above
        repo.Commit("Inital commit", new Signature("Monica Hardt", "monha@itu.dk", new System.DateTimeOffset()),new Signature("Monica Hardt", "monha@itu.dk", new System.DateTimeOffset()), new CommitOptions() {AllowEmptyCommit = true});

        Console.WriteLine(repo.Commits.First().Message);
    }

    public void Dispose(){
            //clean up remove all .get folder
            //deletes created repository from computer. ./.git 
            repo.Dispose();
            Directory.Delete("./.git",true);
         }


    // [Fact]
    public void correct_frequence_in_frequence_mode()
     {
         // Arrange
        var expected = new Dictionary<int,DateTimeOffset>{};
        expected.Add(3,new DateTimeOffset(new DateTime(2022,10,25)));
        expected.Add(2,new DateTimeOffset(new DateTime(2022,11,05)));
        expected.Add(1,new DateTimeOffset(new DateTime(2022,11,02)));
        expected.Add(1,new DateTimeOffset(new DateTime(2022,11,03)));

        // Act
        var actual = Frequence.getFrequence(); //et kald the vores metode her

        // Assert
        Assert.Equal(expected, actual);  
     }

   
}