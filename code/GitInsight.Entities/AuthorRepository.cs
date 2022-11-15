namespace GitInsight.Entities;

using GitInsight.Core;
using GitInsight;

/*
[HttpGet]
[HttpPost]
[HttpPut]
[HttpDelete]
[HttpHead]
[HttpPatch]
*/

[ApiController]
// Route defines default route for actions
//[Route("api/Classroom/Student")]
public class AuthorRepository : ControllerBase, IAuthorRepository
{

    private readonly CommitTreeContext context;

    public AuthorRepository(CommitTreeContext context)
    {
        this.context = context;
    }

    [HttpPut]
    public void Create (AuthorCreateDTO author) {

        var entry = new Author(author.Name);
        context.Authors.Add(entry);
        context.SaveChanges();

        Console.WriteLine(author.Name + " has been created");
    }

    [HttpGet]
    public IReadOnlyCollection<AuthorDTO> ReadAll(){
        return context.Authors.Select ( e => new AuthorDTO(e.Id, e.Name )).ToList().AsReadOnly();
    }

    [HttpPatch]
    public void Update(){
        // not implemented yet
        // context.Authors.delete
    }

}