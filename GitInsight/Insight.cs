namespace GitInsight;




public class Insight{

public static void main (string[] args){
    //in the terminal we want to check if agrs[0] is either something like -f or -a given this information to the user. 
    //if it input is -f run in frequency mode
    //if the input is -a run in author mode
    string input = Console.ReadLine()!;
    if(input.Equals("-a"))
    {
        foreach(Dictionary<int,DateTimeOffset> dic in getFrequenceAuthorMode())
        Console.WriteLine(dic);
    } else if (input.Equals("-f"))
    {
        foreach(var number in getFrequence().Keys)
        {
            Console.WriteLine(number + " " +  getFrequence()[number]);
        }
    }
    
}

    //idea here: in frequence mode: a dictionary that maps an integer to a date
    //every time the date is "read" in the commitlog, the integer is incremented. 
    //doing this for all commits in the log
    //returning the dictionary
public static Dictionary<int,DateTimeOffset> getFrequence()
    {
        throw new NotImplementedException();


    }


    //same as above but in author mode. Maybe seperate dictionaries per person, i'm not sure
    //incrementing the count whenever the same date is "read"
    public static List<Dictionary<int,DateTimeOffset>> getFrequenceAuthorMode()
    {
        throw new NotImplementedException();
    }



    //we also want a printmethod to print to the terminal
}