namespace Tests;

public class FrequencyDataRepositoryTests : IDisposable
{

    private readonly CommitTreeContext _context;
    private readonly FrequencyDataRepository _freqRepo;


    public FrequencyDataRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<CommitTreeContext>();
        builder.UseSqlite(connection);
        var context = new CommitTreeContext(builder.Options);
        context.Database.EnsureCreated();

        //24/08/2008, 3
        context.allFrequencyData.AddRange(
            new FrequencyData(System.DateTime.ParseExact("24/08/2008", "dd/MM/yyyy", null)),
            new FrequencyData(System.DateTime.ParseExact("20/10/2009", "dd/MM/yyyy", null)),
            new FrequencyData(System.DateTime.ParseExact("24/08/2010", "dd/MM/yyyy", null)),
            new FrequencyData(System.DateTime.ParseExact("04/03/2011", "dd/MM/yyyy", null))
        );
        context.SaveChanges();

        _context = context;
        _freqRepo = new FrequencyDataRepository(_context);

    }


    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public void Create_Frequency_Should_Create_Frequency_And_Add_To_DB()
    {
        _freqRepo.Create(new FrequencyDataCreateDTO(DateTime.Parse("01/01/2023"), 1));

        Assert.Equal(5, _context.allFrequencyData.Count());
    }

    [Fact]
    public void Create_Freq_Should_not_change_count_of_freq_since_freq_exists()
    {
        _freqRepo.Create(new FrequencyDataCreateDTO(DateTime.Parse("20/10/2009"), 1));
        Assert.Equal(4, _context.allFrequencyData.Count());
    }

    [Fact]
    public void Create_Freq_Should_change_freq_count_freq_exists()
    {
        var newFreq = new FrequencyDataCreateDTO(DateTime.Parse("20/10/2009"), 1);
        _freqRepo.Create(newFreq);

        Assert.Equal(2, _context.allFrequencyData.Find(newFreq.Date)!.Count);
    }

    [Fact]
    public void Delete_existing_commit()
    {
        _freqRepo.DeleteSpecificFreq(DateTime.Parse("20/10/2009"));

        Assert.Equal(3, _context.allFrequencyData.Count());
    }

    [Fact]
    public void Delete_all_existing_commits()
    {
        _freqRepo.DeleteAll();

        Assert.Equal(0, _context.allFrequencyData.Count());
    }

    [Fact (Skip = "Not currently working")]
    public void ReadAll_return_all_frequencies()
    {
        var listToCheck = _freqRepo.ReadAll();

        Assert.Equivalent(listToCheck, _context.allFrequencyData);
    }
}