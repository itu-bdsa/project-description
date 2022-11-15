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
        
        var frequencyToAdd = (
            from f in context.Frequencies
            where f.Date == frequency.Date
            select f
        ).FirstOrDefault();
        if (frequencyToAdd == null) {
            var entry = new Frequency(frequency.Date);
            context.Frequencies.Add(entry);
            context.SaveChanges();
            Console.WriteLine(frequency.Date + " has been created");
        } else
        {
            frequencyToAdd.Count++;
            Console.WriteLine(frequencyToAdd.Count);
            context.SaveChanges();
        }
    }

    public IReadOnlyCollection<FrequencyDTO> ReadAll()
    {
        return context.Frequencies.Select(e => new FrequencyDTO(e.Id)).ToList().AsReadOnly();
    }

    public void Update()
    {
        // Incoporated in Create()
    }

    public void DeleteSpecificFreq(DateTime Date)
    {
        var frequencyToRemove = (
            from f in context.Frequencies
            where f.Date == Date
            select f
        ).FirstOrDefault();
        if (frequencyToRemove != null) {
            context.Frequencies.Remove(frequencyToRemove);
            context.SaveChanges();
            Console.WriteLine(frequencyToRemove + " has been deleted");
        }
    }

    public void DeleteAll() {
        foreach (var freq in context.Frequencies)
        {
            context.Frequencies.Remove(freq);
            context.SaveChanges();
        }
    }

}