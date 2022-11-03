namespace GitInsight.Entities;

//check med migrations. Are we missing smth for entities to work?
public class Contribution
{
    //public int Id { get; set; }

    [Required]
    public string repoPath { get; set; }
    
    public string author;

    public DateTime date;

    public int commitsCount;
}