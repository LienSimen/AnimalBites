namespace AnimalBites.model;

public class BiteData
{
    public DateTime? BiteDate { get; set; } 
    public string Species { get; set; } = "Unknown Species";
    public string Breed { get; set; } = "Unknown Breed";
    public string Gender { get; set; } = "Unknown";
    public string Color { get; set; } = "Unknown";
    public string BiteArea { get; set; } = "Unknown";
    public string VictimZip { get; set; } = "Unknown";
    public DateTime? QuarantineDate { get; set; }
    public DateTime? ReleaseDate { get; set; } 

    public int DaysInQuarantine => (ReleaseDate.HasValue && QuarantineDate.HasValue)
        ? (ReleaseDate.Value - QuarantineDate.Value).Days
        : 0;

    public BiteData(string csv)
    {
        string[] values = csv.Split(',');

        BiteDate = DateTime.TryParse(values[0], out var biteDate) ? biteDate : null;
        Species = values[1] ?? "Unknown Species";
        Breed = values[2] ?? "Unknown Breed";
        Gender = values[3] ?? "Unknown";
        Color = values[4] ?? "Unknown";
        VictimZip = values[7] ?? "Unknown";
        BiteArea = values[9] ?? "Unknown";
        QuarantineDate = DateTime.TryParse(values[10], out var quarantineDate) ? quarantineDate : null;
        ReleaseDate = DateTime.TryParse(values[13], out var releaseDate) ? releaseDate : null;
    }
}
