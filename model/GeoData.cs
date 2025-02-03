namespace AnimalBites.model;

public class GeoData
{
    public string State { get; set; } = "Unknown";
    public string StateAbbr { get; set; } = "Unknown";
    public string County { get; set; } = "Unknown";
    public string City { get; set; } = "Unknown";

    public GeoData(string state, string stateAbbr, string county, string city)
    {
        State = state;
        StateAbbr = stateAbbr;
        County = county;
        City = city;
    }
}
