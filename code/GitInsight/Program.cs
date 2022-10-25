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
        while (true)
        {
            var input = Console.ReadLine();
            switch (input)
            {
                case "author":
                    _mode = AUTHOR;
                    break;
                case "frequency":
                    // setMode(frequency);
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
                    break;

                default:
                    Console.WriteLine("Command doesn't exist. Type 'help' for commandlist");
                    break;
            }

        }
    }

    public static void print()
    {
        if (_mode == FREQUENCY)
        {
            Console.WriteLine("HELLO WORLD");
        }
        else if (_mode == AUTHOR)
        {
            Console.WriteLine("HELLO ITU");
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