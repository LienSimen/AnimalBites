namespace AnimalBites.model;
using System;

public class BiteData
{
    public DateTime? BiteDate { get; set; }
    public string Species { get; set; } = "Unknown Species";
    public string Breed { get; set; } = "Unknown Breed";
    public string Gender { get; set; } = "Unknown";
    public string Color { get; set; } = "Unknown";
    public string BiteArea { get; set; } = "Unknown";
    public string VictimZip { get; set; } = "Unknown";
    public string State { get; set; } = "Unknown";
    public string County { get; set; } = "Unknown";
    public string City { get; set; } = "Unknown";
    public DateTime? QuarantineDate { get; set; }
    public DateTime? ReleaseDate { get; set; }

    public int DaysInQuarantine => (ReleaseDate.HasValue && QuarantineDate.HasValue)
        ? (ReleaseDate.Value - QuarantineDate.Value).Days
        : 0;

    public BiteData(string csv, Dictionary<string, GeoData> geoLookup)
    {
        string[] values = csv.Split(',');

        BiteDate = DateTime.TryParse(values[0], out var biteDate) ? biteDate : null;
        Species = string.IsNullOrWhiteSpace(values[1]) ? "Unknown Species" : values[1];
        Breed = string.IsNullOrWhiteSpace(values[2]) ? "Unknown Breed" : values[2];
        Gender = string.IsNullOrWhiteSpace(values[3]) ? "Unknown" : values[3];
        Color = string.IsNullOrWhiteSpace(values[4]) ? "Unknown" : values[4];
        VictimZip = string.IsNullOrWhiteSpace(values[7]) ? "Unknown" : values[7];
        BiteArea = string.IsNullOrWhiteSpace(values[9]) ? "Unknown" : values[9];
        QuarantineDate = DateTime.TryParse(values[10], out var quarantineDate) ? quarantineDate : null;
        ReleaseDate = DateTime.TryParse(values[13], out var releaseDate) ? releaseDate : null;

        if (geoLookup.TryGetValue(VictimZip, out var geoData))
        {
            State = geoData.State;
            County = geoData.County;
            City = geoData.City;
        }
    }
}
