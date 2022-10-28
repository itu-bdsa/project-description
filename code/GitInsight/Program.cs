namespace GitInsight;
using static Mode;


class Program
{
    private static Mode _mode;
    private static Boolean _isLoaded;

    static void Main(string[] args)
    {
        RunProg();
        
    }
    

    static void RunProg()
    {
        var isRunning = true;
        while (isRunning)
        {
            var input = Console.ReadLine();
            switch (input)
            {
                case "author":
                    _mode = AUTHOR;
                    break;
                case "frequency":
                    _mode = FREQUENCY;
                    break;
                case "print":
                    // check isLoaded?
                    print();
                    break;
                case "newpath":
                    // prompt for path
                    // loadData(path);
                    break;
                case "help":
                    Console.WriteLine("'newpath' to set new repository path.");
                    Console.WriteLine("'author' to switch to AUTHOR mode.");
                    Console.WriteLine("'frequency' to switch to FREQUENCY mode.");
                    Console.WriteLine("'print' to print repository details.");
                    Console.WriteLine("'exit' to exit program");
                    break;
                case "exit":
                        isRunning = false;
                    break;
                default:
                    Console.WriteLine("Command doesn't exist. Type 'help' for commandlist");
                    break;
            }
        }
    }
// \Users\thekure23\Library\CloudStorage\OneDrive-ITU\courseDocuments\03sem\analysisDesignSoftArch\project\BDSA-project
    public static void Data()
    {
        using (var repo = new Repository(@"D:\source\LibGit2Sharp"))
        {
            Commit commit = repo.Head.Tip;
            Console.WriteLine("Author: {0}", commit.Author.Name);
            Console.WriteLine("Message: {0}", commit.MessageShort);
        }
    }

    public static void print()
    {
        if (_mode == FREQUENCY)
        {
            Console.WriteLine("Frequency:");
        }
        else if (_mode == AUTHOR)
        {
            Console.WriteLine("Author: ");
            Data();
        }
        else
        {
            Console.WriteLine("Please select a mode in order to print");
        }

    }
}

/*
    In commit frequency mode, it should produce textual output 
    on stdout that lists the number of commits per day. 
    For example, the output might look like in the following:

      1 2017-12-08
      6 2017-12-26
     12 2018-01-01
     13 2018-01-02
     10 2018-01-14
      7 2018-01-17
      5 2018-01-18 

    In commit author mode, it should produce textual output on 
    stdout that lists the number of commits per day per author. 
    For example, the output might look like in the following:

    Marie Beaumin
      1 2017-12-08
      6 2017-12-26
     12 2018-01-01
     13 2018-01-02
     10 2018-01-14
      7 2018-01-17
      5 2018-01-18 

    Maxime Kauta
      5 2017-12-06
      3 2017-12-07
      1 2018-01-01
     10 2018-01-02
     21 2018-01-03
      1 2018-01-04
      5 2018-01-05
      
*/