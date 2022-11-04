namespace GitInsight.Core;

public record AuthorDTO(int Id, string Name);

public record AuthorCreateDTO([StringLength(50)]string Name);

public record AuthorUpdateDTO(int AuthorId, DateTime Date, int Count);