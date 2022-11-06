namespace GitInsight.Core;

public record ContributionDTO(int Id, string RepoPath, string Author, DateTime Date, int CommitsCount);

public record ContributionCreateDTO(string RepoPath, string Author, DateTime Date, int CommitsCount);

public record ContributionUpdateDTO(int Id, string RepoPath, string Author, DateTime Date, int NewCommitsCount);