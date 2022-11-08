namespace GitInsight.Entities;

using GitInsight.Core;
using GitInsight;

public class AuthorRepository : IAuthorRepository
{

    private readonly IKanbanContext context;

    public AuthorRepository(I context)
    {
        this.context = context;
    }

    public CreateAuthor (AuthorCreateDTO author) {

        var entry = new Author(author.Name);
        context.Authors.Add(entry);
        context.SaveChanges;

        Console.WriteLine(author.Name + " has been created");
    }

    public IReadOnlyCollection<AuthorDTO> ReadAll(){
        return context.Authors.select ( e => new AuthorDTO(e.Id, e.Name )).ToList().AsReadOnly();
    }

    public UpdateDTO(){
        // not implemented yet
        // context.Authors.delete
    }

}