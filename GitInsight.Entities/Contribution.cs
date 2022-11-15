namespace GitInsight.Entities;

public class Contribution
{
    public int Id { get; set; }

    public string author { get; set; }

    public DateTime date { get; set; }

    public int commitsCount { get; set; }

    public RepoCheck repoCheckObj { get; set; }
}
