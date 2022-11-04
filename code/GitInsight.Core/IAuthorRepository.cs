namespace GitInsight.Core;


public interface IAuthorRepository{

    Create (AuthorCreateDTO author);

    IReadOnlyCollection<AuthorDTO> ReadAll();

    Update(AuthorUpdateDTO author);


}