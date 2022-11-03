namespace GitInsight.Entities;

public class RepoCheck
{
    //public int Id { get; set; }

    [Required]
    public string repoPath { get; set; }

    [Required]
    public Commit lastCheckedCommit { get; set; }

    //one-to-many relationship w. Contributions
    public ICollection<Contribution> Contributions { get; set; } = new HashSet<Contribution>();
}