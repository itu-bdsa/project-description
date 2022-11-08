namespace GitInsight;
using static Mode;

public class Data
{
    private Repository _repo;
    public Data(string url)
    {
        //Checks if a current repository exists and deletes it
        if (System.IO.Directory.Exists("./repoData/deleteMe"))
        {
            Console.WriteLine("Removing current loaded repository...");
            Shutdown();
        }
        //Creates a new repository from an url inside new folder
        _repo = new Repository(Repository.Clone(url, "repoData/deleteMe"));
    }

    public void Print(Mode mode)
    {
        if (mode == FREQUENCY)
        {
            Console.WriteLine("Frequency:");
            PrintFreq();
        }
        else if (mode == AUTHOR)
        {
            Console.WriteLine("Author: ");
            PrintAuth();
        }
        else
        {
            Console.WriteLine("Please select a mode in order to print");
        }
    }


    public void PrintFreq()
    {
        foreach (var cl in _repo.Commits.GroupBy(c => c.Committer.When.Date).ToList())
        {
            Console.WriteLine($"{cl.FirstOrDefault()!.Committer.When.Date.ToString("dd/MM/yyyy")}, {cl.Count()}");
        }
    }

    public void PrintAuth()
    {

        foreach (var cl in _repo.Commits.GroupBy(c => c.Committer.Name).ToList())
        {
            Console.WriteLine($"\n--- {cl.FirstOrDefault()!.Author.Name} ---");

            foreach (var dt in cl.GroupBy(c => c.Committer.When.Date).ToList())
            {
                Console.WriteLine($"{dt.Count()}, {dt.FirstOrDefault()!.Committer.When.Date.ToString("dd/MM/yyyy")}");
            }

        }
    }

    public void Shutdown()
    {
        Console.WriteLine("Deleting current repo");

        System.IO.DirectoryInfo di = new DirectoryInfo("./repoData/");

        foreach (FileInfo file in di.EnumerateFiles())
        {
            file.Delete();
        }
        foreach (DirectoryInfo dir in di.EnumerateDirectories())
        {
            dir.Delete(true);
        }
        if (System.IO.Directory.Exists("./repoData/deleteMe"))
        {
            Console.WriteLine("Directory still exist!");
        }
        else
        {
            Console.WriteLine("Directory was deleted!");
        }
    }


}