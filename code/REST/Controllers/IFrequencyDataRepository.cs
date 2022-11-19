namespace REST.Controllers;


public interface IFrequencyDataRepository{

    void Create (FrequencyDataCreateDTO frequency);

    IReadOnlyCollection<FrequencyDataDTO> ReadAll();

    void Update();

}