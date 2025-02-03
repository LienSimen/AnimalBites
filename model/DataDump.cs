namespace AnimalBites.model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class DataDump
{
    public List<BiteData> Bites { get; set; } = [];
    public Dictionary<string, GeoData> GeoLookup { get; private set; } = new();

    public DataDump()
    {
        LoadGeoData();
        var rawData = File.ReadLines("Health_AnimalBites.csv");
        Bites = rawData.Skip(1)
            .Select(dataString => new BiteData(dataString, GeoLookup))
            .ToList();
    }

    private void LoadGeoData()
    {
        var geoLines = File.ReadLines("geo-data.csv").Skip(1);
        foreach (var line in geoLines)
        {
            var values = line.Split(',');
            if (values.Length < 6) continue;

            var zipcode = values[3].Trim();
            if (!GeoLookup.ContainsKey(zipcode))
            {
                GeoLookup[zipcode] = new GeoData(values[1], values[2], values[4], values[5]);
            }
        }
    }
}
