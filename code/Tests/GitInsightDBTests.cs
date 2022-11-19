// namespace GitInsight.Test;

// public class GitInsightDBTests : IDisposable
// {

//     private readonly CommitTreeContextFactory _context;
//     private readonly AuthorRepository _authorRepo;
//     private readonly FrequencyRepository _freqRepo;


//     public GitInsightDBTests()
//     {
//         var connection = new SQLiteConnection("Filename=:memory:");
//         connection.Open();
//         var builder = new DbContextOptionsBuilder<CommitTreeContext>();
//         builder.UseSqlite(connection);
//         var context = new CommitTreeContext(builder.Options);
//         context.Database.EnsureCreated();

//         context.Tags.AddRange(
//             new Tag { Name = "Cleaning", Id = 1 },
//             new Tag { Name = "Urgent", Id = 2 },
//             new Tag { Name = "TBD", Id = 3 });
//         context.SaveChanges();

//         _context = context;
//         _tagRepository = new TagRepository(context);


//     }


//     public void Dispose()
//     {
//         _context.Dispose();
//     }

//     [Fact]
//     public void shouldGetAProperName()
//     {
//         // Given


//         // When

//         // Then

//     }
// }