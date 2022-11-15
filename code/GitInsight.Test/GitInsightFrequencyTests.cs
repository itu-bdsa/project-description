
using Microsoft.Data.Sqlite;

namespace GitInsight.Test;

public class GitInsightFrequencyTests : IDisposable
{

    private readonly CommitTreeContext _context;
    private readonly FrequencyRepository _freqRepo;


    public GitInsightFrequencyTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<CommitTreeContext>();
        builder.UseSqlite(connection);
        var context = new CommitTreeContext(builder.Options);
        context.Database.EnsureCreated();

        //24/08/2008, 3
        context.Frequencies.AddRange(
            new Frequency(System.DateTime.ParseExact("24/08/2008", "dd/MM/yyyy", null)),
            new Frequency(System.DateTime.ParseExact("20/10/2009", "dd/MM/yyyy", null)),
            new Frequency(System.DateTime.ParseExact("24/08/2010", "dd/MM/yyyy", null)),
            new Frequency(System.DateTime.ParseExact("04/03/2011", "dd/MM/yyyy", null))
        );
        context.SaveChanges();

        _context = context;
        _freqRepo = new FrequencyRepository(context);

    }


    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public void Create_Frequency_Should_Create_Frequency_And_Add_To_DB()
    {
        _freqRepo.Create(new FrequencyCreateDTO(DateTime.Parse("01/01/2023"), 1));

        Assert.Equal(_context.Frequencies.Count(), 5);
    }

    [Fact]
    public void Create_Freq_Should_not_change_count_of_freq_since_freq_exists()
    {
        _freqRepo.Create(new FrequencyCreateDTO(DateTime.Parse("20/10/2009"), 1));
        Assert.Equal(_context.Frequencies.Count(), 4);
    }

    [Fact]
    public void Create_Freq_Should_change_freq_count_freq_exists()
    {
        var newFreq = new FrequencyCreateDTO(DateTime.Parse("20/10/2009"), 1);
        _freqRepo.Create(newFreq);

        Assert.Equal(_context.Frequencies.Find(newFreq.Date)!.Count, 2);
    }

    [Fact]
    public void Delete_existing_commit()
    {
        _freqRepo.DeleteSpecificFreq(DateTime.Parse("20/10/2009"));

        Assert.Equal(_context.Frequencies.Count(), 3);
    }

    [Fact]
    public void Delete_all_existing_commits()
    {
        _freqRepo.DeleteAll();

        Assert.Equal(_context.Frequencies.Count(), 0);
    }

    public void ReadAll_return_all_frequencies()
    {
        var listToCheck = _freqRepo.ReadAll();

        Assert.Equivalent(listToCheck, _context.Frequencies);
    }
}