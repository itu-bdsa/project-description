//using System.Linq.Expressions;
using System.Collections;
using LibGit2Sharp;
namespace GitInsight;

public class GitInsight
{
    //int hewwo;
    public static void Main(string[] args)
    {

        GitInsightContextFactory factory = new GitInsightContextFactory();
        GitInsightContext context = factory.CreateDbContext(args);
        //context.Database.EnsureDeleted(); //to delete database for tests
        var repoRep = new RepoCheckRepository(context);
        //var connyboi = new Contribution{repoPath = "uwu", author = "owo", date = new DateTime(2022, 02,02,10,10,10, DateTimeKind.Utc)}; 
        Console.WriteLine(context.Database.EnsureCreated());

        //context.Contributions.Add(connyboi);
        //context.SaveChanges();

        //user inputs commandline switch /fm or /am to pick a program
        if (args[0].Equals("/fm"))
        {
            CommitFrequencyMode(repoRep);
        }
        else if (args[0].Equals("/am"))
        {
            CommitUserFrequencyMode();
        }
        else
        {
            //if the user didnt write anything or wrote something that wasnt an existing mode:
            // this will loop if the user continueues to do the same as previous, until the user writes an elegible mode
            var blocker = true;
            while (blocker == true)
            {
                Console.WriteLine("Please chose from this list of modes:");
                Console.WriteLine("Frequency mode:   /fm");
                Console.WriteLine("Author mode:      /am");
                string usermode = Console.ReadLine()!;
                if (usermode.Equals("/fm"))
                {
                    CommitFrequencyMode(repoRep);
                    blocker = false;
                }
                else if (usermode.Equals("/am"))
                {
                    CommitUserFrequencyMode();
                    blocker = false;
                }
            }

        }
    }

    public static ArrayList CommitFrequencyMode(RepoCheckRepository repoCheckRep)
    {
        var repoPath = @"C:\Users\annem\Desktop\BDSA_PROJECT\TestGithubStorage\assignment-05";

        using (var repo = new Repository(repoPath)) //hvorfor bruger vi using?
        {
            var commitArray = repo.Commits.ToList();
            Console.WriteLine(repo.Commits.First().Author.When);
            //-------add to database----
            if(RepoExistsInDb(repoPath, repoCheckRep)){
                /*if(CommitIsNewest(repo, repoCheckRep)){
                    //read from db and print
                } else {
                    //analyze, update db and print
                }*/
                
            } else {
                //analyse and then create entry in RepoChecks table
                //repoCheckRep.Create(repoPath, commitArray.Last().Id.ToString());
                repoCheckRep.Create(repoPath, repo.Commits.First().Id.ToString());
                //move this later
            }
            //---------------------------

            ArrayList dateArray = new ArrayList();

            /*for (int i = 0; i < commitArray.Count; i++)
            {
                string[] tempDateArray = commitArray[i].Author.When.ToString().Split(" ");
                dateArray.Add(DateTime.Parse(tempDateArray[0]));
            }

            dateArray.Sort();
            foreach (var item in dateArray)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(dateArray.Count);
            var currentDate = dateArray[0];
            var currentDateCount = 0;
            foreach (DateTime item in dateArray)
            {

                if (item.CompareTo(currentDate) == 0)
                {
                    currentDateCount = currentDateCount + 1;
                }
                else
                {
                    Console.WriteLine(currentDateCount + " " + currentDate?.ToString());
                    currentDate = item;
                    currentDateCount = 1;
                }

            }
            Console.WriteLine(currentDateCount + " " + currentDate?.ToString());*/
            return dateArray;


            //var dates = loges.GroupBy(x => x.Author.When.Date).Count();//.SelectMany(x=>x).ToList();
            //Console.WriteLine(dates);
            /*foreach (var date in dates){
                //Console.WriteLine(date.Take(2));
                foreach (var stuff in date){
                    Console.WriteLine(stuff);
                } 
            }*/

            //foreach (var log in loges){
            //  Console.WriteLine(log + " " + log.Author.When.Date);
            //}
        }
    }

    public static bool RepoExistsInDb(string repoPath, RepoCheckRepository repoCheckRep){
        //bool - does repo exist in table in db pt?
        var repoObject = repoCheckRep.Read(repoPath);
        return (repoObject != null);
    }

    public static bool CommitIsNewest(string repoPath, RepoCheckRepository repoCheckRep){ //string repoPath
        //bool - is last checked commit the newest commit made?
        var repoObject = repoCheckRep.Read(repoPath);
        var rep = new Repository(repoPath);
        return (repoObject.lastCheckedCommit == rep.Commits.First().Id.ToString());  
    }

    public void AddRepoToDb(){
        //add repo to table in db. Slet?
    }

    public static void UpdateCheckedCommitInDb(string repoPath, RepoCheckRepository repoCheckRep){
        //update the latest checked commit in table in db
        var rep = new Repository(repoPath);
        repoCheckRep.Update(repoPath, rep.Commits.First().Id.ToString());
    }

    public static List<List<DateTime>> CommitUserFrequencyMode()
    {
        var repoPath = @"C:\Users\annem\Skrivebord\BDSA\BDSA_PROJECT\TestGithubStorage\assignment-05";
        using (var repo = new Repository(repoPath))
        {
            var commitArray = repo.Commits.ToList();
            //var dateAuthorArray = new List<List<String>>();
            var authorArray = FindAllUsersInRepo();
            var dateArray = new List<List<DateTime>>();

            foreach (var item in authorArray)
            {
                dateArray.Add(new List<DateTime>());
            }


            for (int i = 0; i < commitArray.Count; i++)
            {

                string[] tempAuthorArray = commitArray[i].Author.ToString().Split(" ");

                for (int m = 0; m < authorArray.Count; m++)
                {
                    if (authorArray[m].Equals(tempAuthorArray[0]))
                    {
                        string[] tempDateArray = commitArray[i].Author.When.ToString().Split(" ");
                        dateArray[m].Add(DateTime.Parse(tempDateArray[0]));
                    }

                }

            }

            foreach (var item in dateArray)
            {
                item.Sort();
            }
            var authorCounter = 0;
            foreach (var item in dateArray)
            {

                Console.WriteLine(authorArray[authorCounter]);
                var currentDate = item[0];
                var currentDateCount = 1;
                for (int i = 0; i < item.Count - 1; i++)
                {
                    if (item[i].CompareTo(currentDate) == 0)
                    {
                        currentDateCount = currentDateCount + 1;
                    }
                    else
                    {
                        Console.WriteLine(currentDateCount + " " + currentDate.ToString());
                        currentDate = item[i];
                        currentDateCount = 1;
                    }
                }
                Console.WriteLine(currentDateCount + " " + currentDate.ToString());
                Console.WriteLine("");
                authorCounter = authorCounter + 1;
            }

            return dateArray;


        }
    }

    public static List<String> FindAllUsersInRepo(){
    var repoPath = @"C:\Users\annem\Skrivebord\BDSA\BDSA_PROJECT\TestGithubStorage\assignment-05";
        using (var repo = new Repository(repoPath))
        {

            var commitArray = repo.Commits.ToList();
            var authorArray = new List<String>();

             for (int i = 0; i < commitArray.Count; i++)
            {
                string[] tempAuthorArray = commitArray[i].Author.ToString().Split(" ");
                if (!authorArray.Contains(tempAuthorArray[0]))
                {
                    authorArray.Add(tempAuthorArray[0]);
                }
            }

            return authorArray;
        }
    }
}