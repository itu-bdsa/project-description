namespace GitInsigt;
using LibGit2Sharp;

public class GitInsight{

    public static void Main(string[] args){
        commitFrequencyMode();
    }
    
    public void commitAuthorMode(){
        throw new NotImplementedException();
    }

    public static void commitFrequencyMode(){
        var repoPath = @"C:\Users\Bruger\Desktop\BDSA Projekt\BDSA_PROJECT\GitInsight.Tests\Assignment5\assignment-05";
        var fileOffset = @"C:\Users\annem\Desktop\BDSA_PROJECT\GitInsight.Tests\assignment-05\GildedRose\obj\project.assets.json";
        var fileOffsetFwdSlash = fileOffset.Replace("\\", "/");
        using (var repo = new Repository(repoPath))
        {
            var loges = repo.Commits.ToList();
            Console.WriteLine(loges.Count);
            
            var dates = loges.GroupBy(x => x.Author.When.Date).Count();//.SelectMany(x=>x).ToList();
            Console.WriteLine(dates);
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
    
}