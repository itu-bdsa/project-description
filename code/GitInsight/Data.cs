namespace GitInsight;
using static Mode;

public class Data
{
    // /Users/thekure23/Library/CloudStorage/OneDrive-ITU/courseDocuments/03sem/analysisDesignSoftArch/project/BDSA-project
    private Repository _repo;
    public Data(string path)
    {
        _repo = new Repository(path);
    }

    public void print(Mode mode)
    {
        if (mode == FREQUENCY)
        {
            Console.WriteLine("Frequency:");
            printFreq();
        }
        else if (mode == AUTHOR)
        {
            Console.WriteLine("Author: ");
            printAuthor();
        }
        else
        {
            Console.WriteLine("Please select a mode in order to print");
        }

    }


    public void printFreq(){
        foreach (var cl  in _repo.Commits.GroupBy(c => c.Committer.When.Date).ToList())
            {
                Console.WriteLine($"{cl.FirstOrDefault()!.Committer.When.Date.ToString("dd/MM/yyyy")}, {cl.Count()}");
            }
    }

    public void printAuthor(){
        foreach (var cl  in _repo.Commits.GroupBy(c => c.Committer.Name).ToList())
            {
                Console.WriteLine($"\n--- {cl.FirstOrDefault()!.Author.Name} ---");

                foreach (var dt  in _repo.Commits.GroupBy(c => c.Committer.When.Date).ToList())
                {
                    Console.WriteLine($"{dt.Count()}, {dt.FirstOrDefault()!.Committer.When.Date.ToString("dd/MM/yyyy")}");
                }
                
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