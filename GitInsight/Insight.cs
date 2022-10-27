namespace GitInsight;

using LibGit2Sharp;

public class Insight{

public static void Main (string[] args){
    //in the terminal we want to check if agrs[0] is either something like -f or -a given this information to the user. 
    //if it input is -f run in frequency mode
    //if the input is -a run in author mode

        Console.Write("Enter a path to your repository located on your local device");
        var pathToRepo = Console.ReadLine();

        var repo = new Repository(pathToRepo);
   
	    Console.Write("-a for authormode -f for freqyencemode ");
		var input = Console.ReadLine();

		//Process input
		if(input == "-a"){
            getFrequenceAuthorMode(repo);
        } else if(input == "-f"){
            getFrequence(repo);
        } else {
            throw new ArgumentException("Enter valid mode");
        }


}

    //idea here: in frequence mode: a dictionary that maps an integer to a date
    //every time the date is "read" in the commitlog, the integer is incremented. 
    //doing this for all commits in the log
    //returning the dictionary
public static Dictionary<DateTimeOffset,int> getFrequence(Repository repo)
    {

        Dictionary<DateTimeOffset,int> dic = new Dictionary<DateTimeOffset,int>{};
        foreach(Commit c in repo.Commits){
            if(!dic.ContainsKey(c.Author.When)){
                dic.Add(c.Author.When,1);
            } else {
                //if we are here the data already exists and then we want to increment the numnber of commits on that day
            
            }

      
        }
        
        throw new NotImplementedException();
    }


    //same as above but in author mode. Maybe seperate dictionaries per person, i'm not sure
    //incrementing the count whenever the same date is "read"
    public static List<Dictionary<int,DateTimeOffset>> getFrequenceAuthorMode(Repository repo)
    {
        throw new NotImplementedException();
    }



    //we also want a printmethod to print to the terminal
}