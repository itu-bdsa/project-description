namespace GitInsight.Core;


public interface IAuthorRepository{

    void Create (AuthorCreateDTO author);

    IReadOnlyCollection<AuthorDTO> ReadAll();

    void Update();

}