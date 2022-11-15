namespace GitInsight.Entities;

using GitInsight.Core;
using GitInsight;

public class FrequencyRepository : IFrequencyRepository
{

    private readonly CommitTreeContext context;

    public FrequencyRepository(CommitTreeContext context)
    {
        this.context = context;
    }

    public void Create(FrequencyCreateDTO frequency)
    {

        var entry = new Frequency(1, frequency.Date);
        context.Frequencies.Add(entry);
        context.SaveChanges();

        Console.WriteLine(frequency.Date + " has been created");
    }

    public IReadOnlyCollection<FrequencyDTO> ReadAll()
    {
        return context.Frequencies.Select(e => new FrequencyDTO(e.Id)).ToList().AsReadOnly();
    }

    public void Update()
    {
        // not implemented yet
        // context.Frequenices.delete
    }

    public void Delete(int Id)
    {
        // not implemented yet
        // context.Frequenices.delete
    }

}