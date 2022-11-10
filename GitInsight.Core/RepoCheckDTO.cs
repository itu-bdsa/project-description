namespace GitInsight.Core;

public record RepoCheckDTO(string repoPath, string lastCheckedCommit, ICollection<ContributionDTO> Contributions);

public record RepoCheckCreateDTO(string repoPath, string lastCheckedCommit, ICollection<ContributionDTO> Contributions);

public record RepoCheckUpdateDTO(string repoPath, string lastCheckedCommit);
