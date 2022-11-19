namespace Models;

public class FrequencyData {

    public FrequencyData (DateTime Date) {
        this.Date = Date;
        this.Count = 1;
    }

    [Key]
    public DateTime Date { get; set; }

    public int Count { get; set; }

    public int Id { get; set; }
}