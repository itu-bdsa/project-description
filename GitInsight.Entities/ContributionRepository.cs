namespace GitInsight.Entities;

public class ContributionRepository {

    private GitInsightContext _context;

    public ContributionRepository(GitInsightContext context)
    {
        _context = context;
    }

    private static ContributionDTO ContributionDTOFromContribution(Contribution contribution)
     => new ContributionDTO(
        RepoPath: contribution.repoPath,
        Author: contribution.author,
        Date: contribution.date,
        CommitsCount: contribution.commitsCount
    );


    public void Create(ContributionCreateDTO contribution){
        var newContribution = new Contribution {
                            repoPath = contribution.RepoPath,
                            author = contribution.Author,
                            date = contribution.Date,
                            commitsCount = contribution.CommitsCount,
        };
        _context.Contributions.Add(newContribution);
        _context.SaveChanges();

       /*var search = _context.Contributions.Where(x=>x.repoPath.Equals(repoPath)).FirstOrDefault();
        return (Created,tg.Id);
       }
       return (Conflict,tg.Id);*/
       
    }

    public ContributionDTO Read(string repoPath, string author, DateTime date){
        var contribution = _context.Contributions.Find(repoPath, author, date);


        return contribution != null ? ContributionDTOFromContribution(contribution)
        : null!;
        /*new ContributionDTO(
                                    //contribution.Id,
                                    contribution.repoPath,
                                    contribution.author,
                                    contribution.date,
                                    contribution.commitsCount
        ) : null!;*/
    } //read for different modes

    public IReadOnlyCollection<ContributionDTO> ReadAllforRepo(string repoPath){

        var contributions = _context.Contributions
                            .Where(c => c.repoPath == repoPath)
                            .ToList()
                            .AsReadOnly();
        
        var ContributionDTOs = _context.Contributions
                .Select(contribution => ContributionDTOFromContribution(contribution));
                
        return ContributionDTOs.ToList().AsReadOnly();
    }

    //seaches for contribution in Db
    /*public Contribution Find(int id){
        var con = from c in _context.Contributions
        where c.Id == id
        select c;

        return con.FirstOrDefault();
    }*/

    public void Update(string repoPath, string author, DateTime date, int newCommitsSum){
        
    }

}