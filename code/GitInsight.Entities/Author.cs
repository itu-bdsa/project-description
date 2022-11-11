namespace GitInsight.Entities;

public class Author {

    public Author (string Name) {
        this.Name = Name;
    }

    public int Id {get;}

    [StringLength(50)]
    public string Name {get;}

    public DateTime Date {get;}

    public int Count {get; set;}

}