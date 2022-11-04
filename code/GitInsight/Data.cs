namespace GitInsight;
using static Mode;

public class Data
{
    // /Users/thekure23/Library/CloudStorage/OneDrive-ITU/courseDocuments/03sem/analysisDesignSoftArch/project/BDSA-project
    private Repository? _repo;
   
    public Data()
    {
        var gitInsightFolder = Directory.GetCurrentDirectory();
        var codeFolder = Directory.GetParent(gitInsightFolder)!.ToString();
        var bdsaProjectFolder = Directory.GetParent(codeFolder);
        _repo = new Repository(bdsaProjectFolder + "/testRepository/assignment-04");           
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
        foreach (var cl in _repo!.Commits.GroupBy(c => c.Committer.When.Date).ToList())
            {
                Console.WriteLine($"{cl.FirstOrDefault()!.Committer.When.Date.ToString("dd/MM/yyyy")}, {cl.Count()}");
            }
    }

    public void printAuthor(){
        foreach (var cl in _repo!.Commits.GroupBy(c => c.Committer.Name).ToList())
            {
                Console.WriteLine($"\n--- {cl.FirstOrDefault()!.Author.Name} ---");

                foreach (var dt in _repo.Commits.GroupBy(c => c.Committer.When.Date).ToList())
                {
                    Console.WriteLine($"{dt.Count()}, {dt.FirstOrDefault()!.Committer.When.Date.ToString("dd/MM/yyyy")}");
                }
                
            }
    }

}