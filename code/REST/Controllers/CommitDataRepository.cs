namespace REST.Controllers;

[ApiController]
[Route("api/CommitDataRepository")]
public class CommitDataRepository : ControllerBase, ICommitDataRepository
{
    private readonly CommitTreeContext _context;

    public CommitDataRepository(CommitTreeContext context)
    {
        _context = context;
    }

    [HttpPut(Name = "CD_Create")]
    public void Create(CommitDataCreateDTO CommitData)
    {
        var listToCheck = _context.CommitData.Where(c => c.HashCode == CommitData.HashCode);
        if (listToCheck.Count() != 0)
        {
            Console.WriteLine("Commit already in DB");
        }
        else
        {
            var entry = new CommitData(CommitData.HashCode, CommitData.RepositoryName, CommitData.authorName, CommitData.Date);
            _context.CommitData.Add(entry);
            _context.SaveChanges();
            Console.WriteLine(CommitData.Date + " has been created");
        }
    }


    [HttpDelete(Name = "CD_DeleteAll")]
    public void DeleteAll(string RepositoryName)
    {
        foreach (var commit in _context.CommitData)
        {
            if (commit.RepositoryName == RepositoryName)
            {
                _context.CommitData.Remove(commit);
            }
        }
        _context.SaveChanges();
    }

    [HttpGet(Name = "CD_GetAllFC")]
    public IReadOnlyCollection<Tuple<DateTime, int>> ReadAllCommitsFromRepo(string RepositoryName)
    {

        Tuple<DateTime, int> tup;
        var listToReturn = new List<Tuple<DateTime, int>>();


        var start = _context.CommitData.Where(c => c.RepositoryName == RepositoryName);
        var dates = start.GroupBy(c => c.Date);

        foreach (var date in dates)
        {
            int count = 0;
            foreach (var d in date.GroupBy(c => c.Date))
            {
                count = d.Count();

            }

            tup = new Tuple<DateTime, int>(date.FirstOrDefault()!.Date, count);
            listToReturn.Add(tup);
        }
        return (IReadOnlyCollection<Tuple<DateTime, int>>)listToReturn;
    }


    // Method returns a dictionary of all all commits per author from a given repo. 
    // Key is author and value is a tuple of DateTimes and Count of the amount of commits on the given date
    [HttpGet(Name = "CD_GetAllAC")]
    public IReadOnlyDictionary<string, List<Tuple<DateTime, int>>> GetAllAuthorsCommitsFromRepository(string RepositoryName)
    {

        var mapToReturn = new Dictionary<string, List<Tuple<DateTime, int>>>();
        var start = _context.CommitData.Where(c => c.RepositoryName == RepositoryName);
        var ListOfNames = start.Select(c => c.AuthorName).Distinct();


        foreach (var name in ListOfNames)
        {
            var listOfTuples = new List<Tuple<DateTime, int>>();

            foreach (var dt in start.Where(c => c.AuthorName == name).GroupBy(c => c.Date).ToList())
            {
                var TupleOfDateAndCount = new Tuple<DateTime, int>(dt.FirstOrDefault()!.Date, dt.Count());
                listOfTuples.Add(TupleOfDateAndCount);
            }
            mapToReturn.Add(name, listOfTuples);
        }

        return mapToReturn;
    }

    [HttpGet(Name = "CD_ReadAll")]
    public CommitData[] ReadAll()
    {
        return _context.CommitData.ToArray();
    }

}