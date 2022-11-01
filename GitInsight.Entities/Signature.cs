namespace GitInsight.Entities;

public class Signature {

    public int Id {get;set;}

    [Required]
    public string Name {get; set;}
    [Required]
    public string Email {get; set;}
    [Required]
    public DateTimeOffset Date {get;set;}

}