namespace GitInsight.Entities;

using GitInsight.Core;
using GitInsight;

[ApiController]
// Route defines default route for actions
//[Route("api/Classroom/Student")]
public class FrequencyRepository : ControllerBase, IFrequencyRepository
{

    private readonly CommitTreeContext context;

    public FrequencyRepository(CommitTreeContext context)
    {
        this.context = context;
    }

    [HttpPut]
    public void Create (FrequencyCreateDTO frequency) {

        var entry = new Frequency(frequency.Date);
        context.Frequencies.Add(entry);
        context.SaveChanges();

        Console.WriteLine(frequency.Date + " has been created");
    }
    [HttpGet]
    public IReadOnlyCollection<FrequencyDTO> ReadAll(){
        return context.Frequencies.Select ( e => new FrequencyDTO(e.Id)).ToList().AsReadOnly();
    }

    [HttpPatch]
    public void Update(){
        // not implemented yet
        // context.Frequenices.delete
    }

}