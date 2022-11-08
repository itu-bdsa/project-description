namespace GitInsight.Core;

public record AuthorDTO(int Id, string Name);

public record AuthorCreateDTO([StringLength(50)]string Name);
