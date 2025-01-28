namespace AnimalBites.model;

public class BiteData
{
    public DateTime? BiteDate { get; set; }
    public string? Species { get; set; }
    public string? Breed { get; set; }
    public string? Gender { get; set; }
    public string? Color { get; set; }
    public string? BiteArea { get; set; }
    public string? Victim_zip { get; set; }
    public DateTime? Quarantine_Date { get; set; }
    public DateTime? Release_Date { get; set; }

    public int DaysInQuarantine
    {
        get
        {
            return (Release_Date.HasValue && Quarantine_Date.HasValue)
                ? (Release_Date.Value - Quarantine_Date.Value).Days
                : 0;
        }
    }

    public BiteData(string csv)
    {
        string[] values = csv.Split(',');

        // got null errors on date time 
        BiteDate = DateTime.TryParse(values[0].Trim(), out var biteDate) ? biteDate : null;
        Species = values.Length > 1 ? values[1].Trim() : null;
        Breed = values.Length > 2 ? values[2].Trim() : null;
        Gender = values.Length > 3 ? values[3].Trim() : null;
        Color = values.Length > 4 ? values[4].Trim() : null;
        BiteArea = values.Length > 9 ? values[9].Trim() : null;
        Victim_zip = values.Length > 7 ? values[7].Trim() : null;
        Quarantine_Date = DateTime.TryParse(values.Length > 10 ? values[10].Trim() : null, out var quarantineDate) ? quarantineDate : null;
        Release_Date = DateTime.TryParse(values.Length > 13 ? values[13].Trim() : null, out var releaseDate) ? releaseDate : null;
    }
}
