
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
            new Frequency(DateTime.Parse("24/08/2008")),
            new Frequency(DateTime.Parse("20/10/2009")),
            new Frequency(DateTime.Parse("24/08/2010")),
            new Frequency(DateTime.Parse("04/03/2011"))
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
        _freqRepo.Create(new Core.FrequencyCreateDTO(DateTime.Parse("01/01/2023"), 1));

        Assert.Equal(_context.Frequencies.Count(), 5);
    }

    [Fact]
    public void Create_Freq_Should_give_conflict_since_freq_exists()
    {
        _freqRepo.Create(new FrequencyCreateDTO(DateTime.Parse("20/10/2009"), 1));

        // response.Should().Be(Response.Conflict);

        // id.Should().Be(new TagDTO(1, "Cleaning").Id);
    }

    // [Fact]
    // public void Delete_existing_tag_not_in_use()
    // {
    //     var response = _tagRepository.Delete(1);
    //     response.Should().Be(Response.Deleted);
    //     _context.Tags.Find(1).Should().BeNull();
    // }

    // [Fact]
    // public void Delete_non_existing_tag_return_notFound()
    // {
    //     var response = _tagRepository.Delete(100);
    //     response.Should().Be(Response.NotFound);
    //     _context.Tags.Find(100).Should().BeNull();
    // }

    // [Fact]
    // public void Delete_tag_in_use_without_using_force_should_give_conflict()
    // {
    //     var task1 = new Task { Title = "Clean Office", Id = 1, State = State.Active };
    //     var task2 = new Task { Title = "Do Taxes", Id = 2, State = State.New };
    //     var list = new List<Task> { task1, task2 };
    //     _context.Tags.Find(1)!.Tasks = list;

    //     var response = _tagRepository.Delete(1);
    //     response.Should().Be(Response.Conflict);
    //     _context.Tags.Find(1).Should().NotBeNull();
    // }

    // [Fact]
    // public void Read_return_the_right_tag()
    // {
    //     var tagD = new TagDTO(1, "Cleaning");
    //     var result = _tagRepository.Read(1);
    //     result.Should().Be(tagD);
    // }

    // [Fact]
    // public void ReadAll_Should_return_all_the_tags()
    // {
    //     var t1 = new TagDTO(1, "Cleaning");
    //     var t2 = new TagDTO(2, "Urgent");
    //     var t3 = new TagDTO(3, "TBD");
    //     var listOfTags = new List<TagDTO> { t1, t2, t3 };
    //     var result = _tagRepository.ReadAll();

    //     result.Should().BeEquivalentTo(listOfTags);
    // }

    // [Fact]
    // public void Update_tag_should_give_updated()
    // {
    //     var response = _tagRepository.Update(new TagUpdateDTO(1, "Office work"));
    //     response.Should().Be(Response.Updated);

    //     var entity = _context.Tags.Find(1)!;
    //     entity.Name.Should().Be("Office work");
    // }
}