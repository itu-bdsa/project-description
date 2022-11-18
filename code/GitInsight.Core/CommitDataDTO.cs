namespace GitInsight.Core;

public record CommitDataDTO(string HashCode);
// public record CommitDataDTO(string RepositoryName, [StringLength(50)] string authorName, DateTime Date);

public record CommitDataCreateDTO(string HashCode, string RepositoryName, [StringLength(50)] string authorName, DateTime Date);
