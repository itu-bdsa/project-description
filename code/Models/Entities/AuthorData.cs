namespace Models;

public class AuthorData {

    public AuthorData(string Name, DateTime Date)
    {
        this.Name = Name;
        this.Date = Date;
    }
    
    public AuthorData()
    {
    }

    public int Id { get; set; }

    [StringLength(50), Key]
    public string Name { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int Count { get; set; }

}