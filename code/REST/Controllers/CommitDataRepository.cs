namespace REST.Controllers;

public class CommitDataRepository : ICommitDataRepository
{
    private readonly CommitTreeContext context;

    public CommitDataRepository(CommitTreeContext context)
    {
        _context = context;
    }

    public void Create(CommitDataCreateDTO CommitData)
    {

        var CommitDataToAdd = (
            from c in _context.CommitData
            where c.HashCode == CommitData.HashCode
            select c
        );
        if (CommitDataToAdd == null)
        {
            var entry = new CommitData(CommitData.HashCode, CommitData.RepositoryName, CommitData.authorName, CommitData.Date);
            _context.CommitData.Add(entry);
            _context.SaveChanges();
            Console.WriteLine(CommitData.Date + " has been created");
            // } else
            // {
            //     CommitDataToAdd.Count++;
            //     Console.WriteLine(CommitDataToAdd.Count);
            //     context.SaveChanges();
            // }
        }
    }

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

    // Method returns a dictionary of all all commits per author from a given repo. 
    // Key is author and value is a tuple of DateTimes and Count of the amount of commits on the given date
    //Fix casting
    // IReadOnlyCollection<Dictionary<string, List<Tuple<DateTime, int>>>> GetAllAuthorsCommitsFromRepository(string RepositoryName)
    // {

    //     var mapToReturn = new Dictionary<string, List<Tuple<DateTime, int>>>();
    //     var start = context.CommitData.Where(c => c.RepositoryName == RepositoryName);
    //     var ListOfNames = start.Select(c => c.AuthorName);


    //     foreach (var name in ListOfNames)
    //     {
    //         var listOfTuples = new List<Tuple<DateTime, int>>();

    //         foreach (var dt in start.GroupBy(c => c.Date).ToList())
    //         {
    //             var TupleOfDateAndCount = new Tuple<DateTime, int>(dt.FirstOrDefault()!.Date, dt.Count());
    //             listOfTuples.Add(TupleOfDateAndCount);
    //         }
    //         mapToReturn.Add(name, listOfTuples);
    //     }


    //     return (IReadOnlyCollection<Dictionary<string, List<Tuple<DateTime, int>>>>)mapToReturn;
    // }

    IReadOnlyCollection<Tuple<DateTime, int>> ICommitDataRepository.GetAllAuthorsCommitsFromRepository(string RepositoryName)
    {

        var start = _context.CommitData.Where(c => c.RepositoryName == RepositoryName);
        var dates = start.GroupBy(c => c.Date);

        Tuple<DateTime, int> tup;
        var listToReturn = new List<Tuple<DateTime, int>>();
        foreach (var date in dates)
        {
            int count = 0;
            foreach (var commitsAmount in start.GroupBy(c => c.Date).ToList())
            {
                count = commitsAmount.Count();
            }

            tup = new Tuple<DateTime, int>(date.FirstOrDefault()!.Date, count);
            listToReturn.Add(tup);
        }
        return (IReadOnlyCollection<Tuple<DateTime, int>>)listToReturn;
    }

    // IReadOnlyCollection<Tuple<DateTime, int>> ReadAllCommitsFromRepo(string RepositoryName)
    // {
    //     //fix casting

    //     var start = context.CommitData.Where(c => c.RepositoryName == RepositoryName);
    //     var dates = start.GroupBy(c => c.Date);

    //     Tuple<DateTime, int> tup;
    //     var listToReturn = new List<Tuple<DateTime, int>>();
    //     foreach (var date in dates)
    //     {
    //         int count = 0;
    //         foreach (var commitsAmount in start.GroupBy(c => c.Date).ToList())
    //         {
    //             count = commitsAmount.Count();
    //         }

    //         tup = new Tuple<DateTime, int>(date.FirstOrDefault()!.Date, count);
    //         listToReturn.Add(tup);
    //     }
    //     return (IReadOnlyCollection<Tuple<DateTime, int>>)listToReturn;
    // }

    IReadOnlyCollection<Dictionary<string, List<Tuple<DateTime, int>>>> ICommitDataRepository.ReadAllCommitsFromRepo(string RepositoryName)
    {

        var mapToReturn = new Dictionary<string, List<Tuple<DateTime, int>>>();
        var start = _context.CommitData.Where(c => c.RepositoryName == RepositoryName);
        var ListOfNames = start.Select(c => c.AuthorName);


        foreach (var name in ListOfNames)
        {
            var listOfTuples = new List<Tuple<DateTime, int>>();

            foreach (var dt in start.GroupBy(c => c.Date).ToList())
            {
                var TupleOfDateAndCount = new Tuple<DateTime, int>(dt.FirstOrDefault()!.Date, dt.Count());
                listOfTuples.Add(TupleOfDateAndCount);
            }
            mapToReturn.Add(name, listOfTuples);
        }


        return (IReadOnlyCollection<Dictionary<string, List<Tuple<DateTime, int>>>>)mapToReturn;
    }
}