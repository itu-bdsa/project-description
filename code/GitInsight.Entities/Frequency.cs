namespace GitInsight.Entities;

public class Frequency {

    public Frequency (DateTime Date) {
        this.Date = Date;
    }

    [key]
    public DateTime Date {get;}

    public int Count {get; set;}

}