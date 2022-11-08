namespace GitInsight.Core;


public interface IFrequencyRepository{

    void Create (FrequencyCreateDTO frequency);

    IReadOnlyCollection<FrequencyDTO> ReadAll();

    void Update();

}