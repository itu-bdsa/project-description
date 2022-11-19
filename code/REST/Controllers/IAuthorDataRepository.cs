namespace REST.Controllers;


public interface IAuthorDataRepository{

    void Create (AuthorDataCreateDTO author);

    IReadOnlyCollection<AuthorDataDTO> ReadAll();

    void Update();

}