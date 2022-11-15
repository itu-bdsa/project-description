namespace GitInsight.Entities;

using GitInsight.Core;
using GitInsight;

public class AuthorRepository : IAuthorRepository
{

    private readonly CommitTreeContext context;

    public AuthorRepository(CommitTreeContext context)
    {
        this.context = context;
    }

    public void Create(AuthorCreateDTO author)
    {

        var entry = new Author(author.Name, DateTime.ParseExact("01/01/0001", "dd/mm/yyyy", null));
        context.Authors.Add(entry);
        context.SaveChanges();

        Console.WriteLine(author.Name + " has been created");
    }

    public IReadOnlyCollection<AuthorDTO> ReadAll()
    {
        var authors = context.Authors.Select(e => new AuthorDTO(e.Id, e.Name));

        return authors.ToList().AsReadOnly();

    }

    public void Update()
    {
        // not implemented yet
        // context.Authors.delete
    }

        public void DeleteSpecificAuth(DateTime Date)
    {
        var authorToRemove = (
            from f in context.Authors
            where f.Date == Date
            select f
        ).FirstOrDefault();
        if (authorToRemove != null) {
            context.Authors.Remove(authorToRemove);
            context.SaveChanges();
            Console.WriteLine(authorToRemove + " has been deleted");
        }
    }

    public void DeleteAll() {
        foreach (var freq in context.Authors)
        {
            context.Authors.Remove(freq);
            context.SaveChanges();
        }
    }

}