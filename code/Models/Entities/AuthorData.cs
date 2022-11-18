namespace Models;

public class AuthorData {

    // public AuthorData (string Name) {
    //     this.Name = Name;
    // }

    public int Id {get; set;}

    [StringLength(50)]
    public string? Name {get; set;}

    public DateTime Date {get; set;}

    public int Count {get; set;}

}