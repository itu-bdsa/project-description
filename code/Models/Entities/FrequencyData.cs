namespace Models;

public class FrequencyData {

    public FrequencyData (DateTime Date) {
        this.Date = Date;
    }
        
    public DateTime Date {get; set;}

    public int Count {get; set;}

    [Key]
    public int Id {get; set;}

}