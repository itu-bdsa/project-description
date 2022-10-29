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
        Signature sig4 = new Signature("Person3", "person2@itu.dk", new DateTimeOffset(new DateTime(2022,11,25)));

        repo.Commit("Inital commit", sig1, sig1, new CommitOptions() {AllowEmptyCommit = true});
        repo.Commit("Inital commit1", sig2, sig2, new CommitOptions() {AllowEmptyCommit = true});
        repo.Commit("Inital commit2", sig3, sig3, new CommitOptions() {AllowEmptyCommit = true});
        repo.Commit("Inital commit3", sig4, sig4, new CommitOptions() {AllowEmptyCommit = true});
        
        //Console.WriteLine(repo.Commits.First().Message);
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
        var expected = new Dictionary<string,int>{};
        expected.Add("25.11.2022",1);
        expected.Add("25.10.2022",3);

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
        Signature sig5 = new Signature("Person1", "person1@itu.dk", new DateTimeOffset(new DateTime(2022,11,04)));
        Signature sig6 = new Signature("Person2", "person1@itu.dk", new DateTimeOffset(new DateTime(2022,11,03)));
        Signature sig7 = new Signature("Person3", "person1@itu.dk", new DateTimeOffset(new DateTime(2022,11,02)));

        repo.Commit("Inital commit", sig5, sig5, new CommitOptions() {AllowEmptyCommit = true});
        repo.Commit("Inital commit1", sig6, sig6, new CommitOptions() {AllowEmptyCommit = true});
        repo.Commit("Inital commit2", sig7, sig7, new CommitOptions() {AllowEmptyCommit = true});
       
        Dictionary<string,int> person1Dic = new Dictionary<string,int>{};
        person1Dic.Add("25.10.2022",1);
        person1Dic.Add("04.11.2022",1);

        Dictionary<string,int> person2Dic = new Dictionary<string,int>{};
        person2Dic.Add("25.10.2022",1);
        person2Dic.Add("03.11.2022",1);

        Dictionary<string,int> person3Dic = new Dictionary<string,int>{};
        person3Dic.Add("25.11.2022",1);
        person3Dic.Add("25.10.2022",1);
        person3Dic.Add("02.11.2022",1);

        var expectedDictionaryOfDictionaries = new Dictionary<string,Dictionary<string,int>>{};
        expectedDictionaryOfDictionaries.Add(sig7.Name,person3Dic);
        expectedDictionaryOfDictionaries.Add(sig5.Name,person1Dic);
        expectedDictionaryOfDictionaries.Add(sig6.Name,person2Dic);

        // Act
        var actual = Insight.getFrequenceAuthorMode(repo); //call to our method here should return dictionary mapping an integer to a DateTimeOffSet

        // Assert
        Assert.Equal(expectedDictionaryOfDictionaries, actual);  
        }

         [Fact]
        public void printing_prints_seperate_lines_with_correct_information()
        {
            // Given
        
            // When
        
            // Then
        }
   
}