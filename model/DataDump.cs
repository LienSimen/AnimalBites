namespace AnimalBites.model;

public class DataDump
{
    public List<BiteData> Bites { get; set; } = [];

    public DataDump()
    {
        var rawData = File.ReadLines("Health_AnimalBites.csv");
        
        Bites = rawData.Skip(1).Select(dataString => new BiteData(dataString)).ToList();
    }
}
