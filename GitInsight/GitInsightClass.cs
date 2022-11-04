namespace GitInsight;
//using System.Linq.Expressions;
using System.Collections;
using LibGit2Sharp;
using CommandLine;


public class GitInsightClass
{

    public static void Main(string[] args)
    {
        GitInsightContextFactory factory = new GitInsightContextFactory();
        GitInsightContext context = factory.CreateDbContext(args);
        //context.Database.EnsureDeleted(); //to delete database for tests
        var repoRep = new RepoCheckRepository(context);
        Console.WriteLine(context.Database.EnsureCreated());

        //specify a path by writing "--Path=pathname/somewhere" when running the program
        var result = Parser.Default.ParseArguments<Options>(args);
        //user inputs commandline switches "--AuthMode true" or leave it blank to pick a program
        if (result.Value.AuthMode.GetValueOrDefault() == true){
            
            CommitUserFrequencyMode(result.Value.path!);
        }
        else if (result.Value.FQMode.GetValueOrDefault() == true){
            CommitFrequencyMode(result.Value.path!);
        } else {
            Console.WriteLine("please leave etither FQMode to default value, or make sure Author mode is true");
        }
    }

    public static ArrayList CommitFrequencyMode(string path)
    {
        //specify a path by writing "--Path=pathname/somewhere" when running the program
        //var repoPath = @"C:\Users\annem\Desktop\BDSA_PROJECT\TestGithubStorage\assignment-05";
        var repoPath = path;
        //var fileOffset = @"C:\Users\annem\Desktop\BDSA_PROJECT\GitInsight.Tests\assignment-05\GildedRose\obj\project.assets.json";
        //var fileOffsetFwdSlash = fileOffset.Replace("\\", "/");
        using (var repo = new Repository(repoPath))
        {
            var commitArray = repo.Commits.ToList();
            //Console.WriteLine(repo.Commits.First().Author.When);
            //-------add to database----
            /*if(RepoExistsInDb(repoPath, repoCheckRep)){
                /*if(CommitIsNewest(repo, repoCheckRep)){
                    //read from db and print
                } else {
                    //analyze, update db and print
                }*/
                
            //} else {
                //analyse and then create entry in RepoChecks table
                //repoCheckRep.Create(repoPath, commitArray.Last().Id.ToString());
                //repoCheckRep.Create(repoPath, repo.Commits.First().Id.ToString());
                //move this later
            //}
            //---------------------------

            ArrayList dateArray = new ArrayList();

            for (int i = 0; i < commitArray.Count; i++)
            {
                string[] tempDateArray = commitArray[i].Author.When.ToString().Split(" ");
                dateArray.Add(tempDateArray[0]);
            }

            dateArray.Sort();
            foreach (var item in dateArray)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(dateArray.Count);
            var currentDate = dateArray[0];
            var currentDateCount = 0;
            foreach (var item in dateArray)
            {

                if (item.Equals(currentDate))
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
            Console.WriteLine(currentDateCount + " " + currentDate?.ToString());
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

    public static List<List<String>> CommitUserFrequencyMode(string path)
    {
        //var repoPath = @"C:\Users\annem\Skrivebord\BDSA\BDSA_PROJECT\TestGithubStorage\assignment-05";
        //specify a path by writing "--Path=pathname/somewhere" when running the program
        var repoPath = path;
        using (var repo = new Repository(repoPath))
        {
            var commitArray = repo.Commits.ToList();
            var dateAuthorArray = new List<List<String>>();


            for (int i = 0; i < commitArray.Count; i++)
            {
                string[] tempAuthorArray = commitArray[i].Author.ToString().Split(" ");

                bool containsInList = false;

                foreach (var item in dateAuthorArray)
                {
                    if (item[0].Equals(tempAuthorArray[0]))
                    {
                        containsInList = true;
                    }
                }

                if (!containsInList)
                {
                    var AuthorList = new List<String>();
                    AuthorList.Add(tempAuthorArray[0]);
                    dateAuthorArray.Add(AuthorList);
                }
            }




            for (int i = 0; i < commitArray.Count; i++)
            {

                string[] tempAuthorArray = commitArray[i].Author.ToString().Split(" ");
                foreach (var item in dateAuthorArray)
                {
                    if (item[0].Equals(tempAuthorArray[0]))
                    {
                        var tempDateArray = commitArray[i].Author.When.Date.ToString().Split(" ");
                        item.Add(tempDateArray[0].ToString());
                    }
                }

            }

            foreach (var item in dateAuthorArray)
            {
                item.Sort();
            }

            foreach (var item in dateAuthorArray)
            {
                Console.WriteLine(item[item.Count-1]);
                var currentDate = item[0];
                var currentDateCount = 0;
                for(int i = 0; i < item.Count-1; i++){
                    if (item[i].Equals(currentDate))
                    {
                        currentDateCount = currentDateCount + 1;
                    }
                    else
                    {
                        Console.WriteLine(currentDateCount + " " + currentDate.ToString());
                        currentDate = item[i].ToString();
                        currentDateCount = 1;
                    }
                }
                Console.WriteLine(currentDateCount + " " + currentDate.ToString());
                Console.WriteLine("");
            }

            return dateAuthorArray;


        }
    }

    public static List<String> FindAllUsersInRepo(){
    var repoPath = @"C:\Users\annem\Skrivebord\BDSA\BDSA_PROJECT\TestGithubStorage\assignment-05";
        using (var repo = new Repository(repoPath))
        {}
        return new List<String>(); //ret!
    }

}

//Options class, used by the commandline parser to take user input and set mode and path
class Options{
    [Option(Default = (bool)false)]
    public bool? AuthMode { get; set; }

    [Option(Default = (bool)true)]
    public bool? FQMode { get; set; }

    [Option('t', "Path")]
    public String ?path {get; set;}

}

