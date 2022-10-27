namespace GitInsight.Tests;

using LibGit2Sharp;
using GitInsight;

public class InsightTests: IDisposable{

    private Repository repo;

    public InsightTests(){
        //creates repository through libGit2Sharp
        var path = Repository.Init(".");

        //creates a repository object from the path above
        repo = new Repository(path);

        //Creates 3 commits to the same date
        Signature sig1 = new Signature("Person1", "person1@itu.dk", new DateTimeOffset(new DateTime(2022,10,25)));
        Signature sig2 = new Signature("Person2", "person2@itu.dk", new DateTimeOffset(new DateTime(2022,10,25)));
        Signature sig3 = new Signature("Person3", "person2@itu.dk", new DateTimeOffset(new DateTime(2022,10,25)));

        repo.Commit("Inital commit", sig1, sig1, new CommitOptions() {AllowEmptyCommit = true});
        repo.Commit("Inital commit", sig2, sig2, new CommitOptions() {AllowEmptyCommit = true});
        repo.Commit("Inital commit", sig3, sig3, new CommitOptions() {AllowEmptyCommit = true});
        
        Console.WriteLine(repo.Commits.First().Message);
    }

    public void Dispose(){
            //clean up remove all .get folder
            //deletes created repository from computer. ./.git 
            repo.Dispose();
            Directory.Delete("./.git",true);
         }

    [Fact]
    public void frequence_in_frequencemode_should_return_dictionary_number_of_commits_per_day()
     {
         // Arrange
        var expected = new Dictionary<DateTimeOffset,int>{};
        expected.Add(new DateTimeOffset(new DateTime(2022,10,25)),3);

        // Act
        var actual = Insight.getFrequence(repo); //call to our method here should return dictionary mapping an integer to a DateTimeOffSet

        // Assert
        Assert.Equal(expected, actual);  
     }

     
     //CAN THIS BE DONE PRETTIER?
    [Fact]
    public void frequency_in_authormode_commits_should_return_dictionary_commits_per_author_per_day()
        {
        //Arrange
        //adding more commmits to the repo with different dates
        Signature sig4 = new Signature("Person1", "person1@itu.dk", new DateTimeOffset(new DateTime(2022,11,04)));
        Signature sig5 = new Signature("Person2", "person1@itu.dk", new DateTimeOffset(new DateTime(2022,11,03)));
        Signature sig6 = new Signature("Person3", "person1@itu.dk", new DateTimeOffset(new DateTime(2022,11,02)));
       
        var person1Dic = new Dictionary<int,DateTimeOffset>{};
        person1Dic.Add(1,new DateTimeOffset(new DateTime(2022,10,25)));
        person1Dic.Add(1,new DateTimeOffset(new DateTime(2022,11,04)));

        var person2Dic = new Dictionary<int,DateTimeOffset>{};
        person2Dic.Add(1,new DateTimeOffset(new DateTime(2022,10,25)));
        person2Dic.Add(1,new DateTimeOffset(new DateTime(2022,11,03)));

        var person3Dic = new Dictionary<int,DateTimeOffset>{};
        person2Dic.Add(1,new DateTimeOffset(new DateTime(2022,10,25)));
        person2Dic.Add(1,new DateTimeOffset(new DateTime(2022,11,02)));
        

        var expectedListOfDictionaries = new List<Dictionary<int,DateTimeOffset>>{};
        expectedListOfDictionaries.Add(person1Dic);
        expectedListOfDictionaries.Add(person2Dic);
        expectedListOfDictionaries.Add(person3Dic);

        // Act
        var actual = Insight.getFrequenceAuthorMode(repo); //call to our method here should return dictionary mapping an integer to a DateTimeOffSet

        // Assert
        Assert.Equal(expectedListOfDictionaries, actual);  
        }

        [Fact]
        public void frequency_in_authormode_with_invalid_author()
        {
            // Given
        
            // When
        
            // Then
        }

         [Fact]
        public void printing_prints_seperate_lines_with_correct_information()
        {
            // Given
        
            // When
        
            // Then
        }
   
}