namespace GitInsight.Entities;

public class Frequency {

    public Frequency (DateTime Date) {
        this.Date = Date;
    }


    public DateTime Date {get; set; }

    public int Count {get; set; }

    [Key]
    public int Id {get; set; }

}