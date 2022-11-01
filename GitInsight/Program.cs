using System.Linq.Expressions;
using System.Collections;
using LibGit2Sharp;
namespace GitInsigt;

public class GitInsight
{

    public static void Main(string[] args)
    {
        //user inputs commandline switch /fm or /am to pick a program
        if (args[0].Equals("/fm")){
            commitFrequencyMode();
        }
        else if (args[0].Equals("/am")){
            commitUserFrequencyMode();
        } else {
            //if the user didnt write anything or wrote something that wasnt an existing mode:
            // this will loop if the user continueues to do the same as previous, until the user writes an elegible mode
            var blocker = true;
            while(blocker == true){
            Console.WriteLine("Please chose from this list of modes:");
            Console.WriteLine("Frequency mode:   /fm");
            Console.WriteLine("Author mode:      /am");
            string usermode = Console.ReadLine();
            if (usermode.Equals("/fm")){
                commitFrequencyMode();
                blocker = false;
            } else if (usermode.Equals("/am")){
                commitUserFrequencyMode();
                blocker=false;
            }
            }

        }
    }

    public static void commitFrequencyMode()
    {
        var repoPath = @"C:\Users\Bruger\Desktop\BDSA Projekt\BDSA_PROJECT\GitInsight.Tests\Assignment5\assignment-05";
        var fileOffset = @"C:\Users\annem\Desktop\BDSA_PROJECT\GitInsight.Tests\assignment-05\GildedRose\obj\project.assets.json";
        var fileOffsetFwdSlash = fileOffset.Replace("\\", "/");
        using (var repo = new Repository(repoPath))
        {
            var commitArray = repo.Commits.ToList();
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
                    Console.WriteLine(currentDateCount + " " + currentDate.ToString());
                    currentDate = item.ToString();
                    currentDateCount = 1;
                }

            }
            Console.WriteLine(currentDateCount + " " + currentDate.ToString());
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

    public static void commitUserFrequencyMode()
    {
        var repoPath = @"C:\Users\Bruger\Desktop\BDSA Projekt\BDSA_PROJECT\GitInsight.Tests\Assignment5\assignment-05";
        var fileOffset = @"C:\Users\annem\Desktop\BDSA_PROJECT\GitInsight.Tests\assignment-05\GildedRose\obj\project.assets.json";
        var fileOffsetFwdSlash = fileOffset.Replace("\\", "/");
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


        }
    }
}