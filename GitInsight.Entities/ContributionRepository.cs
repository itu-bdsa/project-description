namespace GitInsight.Entities;

public class ContributionRepository {

    private GitInsightContext _context;

    public ContributionRepository(GitInsightContext context)
    {
        _context = context;
    }


    public void Create(string repoPath, string author, DateTime date, int commitsCount){
       var search = _context.Contributions.Where(x=>x.repoPath.Equals(repoPath)).FirstOrDefault();
       
       var cn = new Contribution();

       /*if(search is null){
        _context.Tags.Add(tg);
       _context.SaveChanges();

       return (Created,tg.Id);
       }
       return (Conflict,tg.Id);*/
       
    }

    public void Read(string repoPath){} //read for different modes

    //seaches for contribution in Db
    public Contribution Find(int id){
        var con = from c in _context.Contributions
        where c.Id == id
        select c;

        return con.FirstOrDefault();
    }

    public void Update(string repoPath, string author, DateTime date, int newCommitsSum){}

}