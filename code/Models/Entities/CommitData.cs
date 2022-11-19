namespace Models;

public class CommitData
{

    public CommitData(string HashCode, string RepositoryName, string AuthorName, DateTime Date)
    {
        this.HashCode = HashCode;
        this.RepositoryName = RepositoryName;
        this.AuthorName = AuthorName;
        this.Date = Date;
    }

    public int Id { get; set; }

    [Key]
    public string HashCode { get; set; }

    [Required]
    public string RepositoryName { get; set; }

    [StringLength(50)]
    public string AuthorName { get; set; }

    [Required]
    public DateTime Date { get; set; }

}