namespace Models;

public record AuthorDataDTO(int Id, string Name);

public record AuthorDataCreateDTO([StringLength(50)]string Name);
