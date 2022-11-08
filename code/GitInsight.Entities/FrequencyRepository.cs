namespace GitInsight.Entities;

using GitInsight.Core;
using GitInsight;

public class FrequencyRepository : IAuthorRepository
{

    private readonly IKanbanContext context;

    public FrequencyRepository(I context)
    {
        this.context = context;
    }

    public FrequencyCreateDTO (FrequencyCreateDTO frequency) {

        var entry = new Frequency(frequency.Name);
        context.Frequencies.Add(entry);
        context.SaveChanges;

        Console.WriteLine(frequency.Name + " has been created");
    }

    public IReadOnlyCollection<AuthorDTO> ReadAll(){
        return context.Frequencies.select ( e => new FrequencyDTO(e.Id, e.Name )).ToList().AsReadOnly();
    }

    public UpdateDTO(){
        // not implemented yet
        // context.Authors.delete
    }

}