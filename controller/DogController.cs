using AnimalBites.model;
using AnimalBites.view;

namespace AnimalBites.controller;

public class DogController(DataDump dataDump, RenderAggression view)
{
    private readonly List<BiteData> _bites = dataDump.Bites;
    private readonly RenderAggression _view = view;

    public void ShowMostAggressiveBreed()
    {
        var mostAggressive = _bites
            .GroupBy(bite => bite.Breed)
            .OrderByDescending(group => group.Count())
            .Select(group => (group.Key, group.Count()))
            .ToList();

        _view.DisplayMostAggressive(mostAggressive);
    }
    public void BitesBySpecies()
    {
        var bitesBySpecies = _bites
            .Where(b => b.Species.Length > 0)
            .GroupBy(bite => bite.Species)
            .OrderByDescending(group => group.Count())
            .Select(group => (group.Key, group.Count()))
            .ToList();

        _view.DisplayBitesBySpecies(bitesBySpecies);
    }

    public void ShowLeastAggressiveBreed()
    {
        var leastAggressive = _bites
            .GroupBy(bite => bite.Breed)
            .OrderBy(group => group.Count())
            .FirstOrDefault();

        string result = leastAggressive?.Key ?? "No Data Available";
        _view.DisplayLeastAggressive(result);
    }

    public void BitesByGender()
    {
        var bitesByGender = _bites
            .Where(bite => bite.Gender.Length > 0)
            .GroupBy(bite => bite.Gender)
            .OrderByDescending(group => group.Count())
            .Select(group => (Gender: group.Key, Count: group.Count()))
            .ToList();
        _view.DisplayBitesByGender(bitesByGender);
    }

    public void BitesByArea()
    {
        var bitesByAreas = _bites
        .Where(bite => bite.BiteArea.Length > 0)
        .GroupBy(bite => bite.BiteArea)
        .OrderByDescending(group => group.Count())
        .Select(group => (Area: group.Key, Count: group.Count()))
        .ToList();
        _view.DisplayBiteArea(bitesByAreas);
    }
    public void BitesByColor()
    {
        var bitesByColor = _bites
            .Where(bite => !string.IsNullOrWhiteSpace(bite.Color))
            .GroupBy(bite => bite.Color)
            .OrderByDescending(group => group.Count())
            .Select(group => (Color: group.Key, Count: group.Count()))
            .ToList();

        _view.DisplayColor(bitesByColor);
    }

    public void BitesByZip()
    {
        var bitesByZip = _bites
        .Where(bite => bite.VictimZip.Length > 0)
        .GroupBy(bite => bite.VictimZip)
        .OrderByDescending(group => group.Count())
        .Select(group => (Area: group.Key, Count: group.Count()))
        .ToList();
        _view.DisplayBiteZip(bitesByZip);
    }

    public void BitesByCity()
    {
        var bitesByCity = _bites
            .Where(b => b.City != "Unknown")
            .GroupBy(b => b.City)
            .Select(g => (g.Key, g.Count()))
            .OrderByDescending(g => g.Item2)
            .ToList();

        _view.DisplayBitesByCity(bitesByCity);
    }

    public void BitesByCounty()
    {
        var bitesByCounty = _bites
            .Where(b => b.County != "Unknown")
            .GroupBy(b => b.County)
            .Select(g => (g.Key, g.Count()))
            .OrderByDescending(g => g.Item2)
            .ToList();

        _view.DisplayBitesByCounty(bitesByCounty);
    }

    public void LongestQuarantine()
    {
        var longestQuarantine = _bites
            .Where(bite => bite.QuarantineDate.HasValue && bite.ReleaseDate.HasValue)
            .OrderByDescending(bite => bite.DaysInQuarantine)
            .FirstOrDefault();
        _view.DisplayLongestQuarantine(longestQuarantine?.DaysInQuarantine);
    }

    public void MostBitesByYear()
    {
        var bitesByYear = _bites
            .Where(bite => bite.BiteDate.HasValue)
            .GroupBy(bite => bite.BiteDate!.Value.Year)
            .OrderByDescending(group => group.Count())
            .Select(group => new { Year = group.Key, Count = group.Count() })
            .ToList<dynamic>();

        _view.DisplayMostBitesByYear(bitesByYear);
    }

    //mega cool
    public (IQueryable<BiteData>, string?, string?) QueryBuilder()
    {
        var queryStart = _bites.AsQueryable();
        
        Console.WriteLine("Type a Species or leave blank");
        string? speciesInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(speciesInput))
        {
            queryStart = queryStart.Where(b => string.Equals(b.Species, speciesInput, StringComparison.InvariantCultureIgnoreCase));
        }

        Console.WriteLine("Type a breed or leave blank");
        string? breedInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(breedInput))
        {
            queryStart = queryStart.Where(b => string.Equals(b.Breed, breedInput, StringComparison.InvariantCultureIgnoreCase));
        }

        Console.WriteLine("Type a gender or leave blank");
        string? genderInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(genderInput))
        {
            queryStart = queryStart.Where(b => string.Equals(b.Gender, genderInput, StringComparison.CurrentCultureIgnoreCase));
        }

        Console.WriteLine("Type a color or leave blank");
        string? colorInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(colorInput))
        {
            queryStart = queryStart.Where(b => string.Equals(b.Color, colorInput, StringComparison.CurrentCultureIgnoreCase));
        }

        Console.WriteLine("Quarantined? Y/n");
        string? quarantineInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(quarantineInput))
        {
            if (quarantineInput.ToLower() == "y")
            {
                queryStart = queryStart.Where(b => b.DaysInQuarantine > 0);
            }
        }


        return (queryStart, quarantineInput, breedInput);
    }
    public void AdvancedQuery()
    {
        var results = RunAdvancedQuery();

        if (!results.Any())
        {
            Console.WriteLine("No results found.");
            return;
        }

        _view.DisplayQueryResults(results);
    }

    public List<BiteData> RunAdvancedQuery()
    {
        var queryStart = _bites.AsQueryable();

        Console.WriteLine("\n🔍Advanced Query: Define filters or leave blank");

        Console.Write("State: ");
        string? stateInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(stateInput))
        {
            queryStart = queryStart.Where(b => b.State.Equals(stateInput, StringComparison.OrdinalIgnoreCase));
        }

        Console.Write("County: ");
        string? countyInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(countyInput))
        {
            queryStart = queryStart.Where(b => b.County.Equals(countyInput, StringComparison.OrdinalIgnoreCase));
        }

        Console.Write("City: ");
        string? cityInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(cityInput))
        {
            queryStart = queryStart.Where(b => b.City.Equals(cityInput, StringComparison.OrdinalIgnoreCase));
        }

        Console.Write("ZIP Code: ");
        string? zipInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(zipInput))
        {
            queryStart = queryStart.Where(b => b.VictimZip.Equals(zipInput));
        }

        Console.Write("Breed: ");
        string? breedInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(breedInput))
        {
            queryStart = queryStart.Where(b => b.Breed.Equals(breedInput, StringComparison.OrdinalIgnoreCase));
        }

        Console.Write("Year (e.g., 2011): ");
        string? yearInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(yearInput) && int.TryParse(yearInput, out int year))
        {
            queryStart = queryStart.Where(b => b.BiteDate.HasValue && b.BiteDate.Value.Year == year);
        }

        Console.Write("Sort by (Breed, City, County, Year, Zip, Species, BitesCount): ");
        string? sortInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(sortInput))
        {
            switch (sortInput.ToLower())
            {
                case "breed":
                    queryStart = queryStart.OrderBy(b => b.Breed);
                    break;
                case "city":
                    queryStart = queryStart.OrderBy(b => b.City);
                    break;
                case "county":
                    queryStart = queryStart.OrderBy(b => b.County);
                    break;
                case "year":
                    queryStart = queryStart.OrderBy(b => b.BiteDate.Value.Year);
                    break;
                case "zip":
                    queryStart = queryStart.OrderBy(b => b.VictimZip);
                    break;
                case "species":
                    queryStart = queryStart.OrderBy(b => b.Species);
                    break;
                case "bitescount":
                    queryStart = queryStart.GroupBy(b => b.Breed)
                                           .Select(g => g.First())
                                           .OrderByDescending(b => _bites.Count(bite => bite.Breed == b.Breed));
                    break;
            }
        }

        var results = queryStart.ToList();
        return results;
    }
    public void DisplayQueryResults(List<BiteData> results)
    {
        _view.DisplayQueryResults(results);
    }
    public void ExportQueryToCSV(List<BiteData> results)
    {
        if (!results.Any())
        {
            Console.WriteLine("No data found to export.");
            return;
        }

        string filePath = "query_results.csv";
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("BiteDate,Species,Breed,Gender,Color,BiteArea,VictimZip,City,County,State,QuarantineDays");
            foreach (var bite in results)
            {
                writer.WriteLine($"{bite.BiteDate},{bite.Species},{bite.Breed},{bite.Gender},{bite.Color},{bite.BiteArea},{bite.VictimZip},{bite.City},{bite.County},{bite.State},{bite.DaysInQuarantine}");
            }
        }

        Console.WriteLine($"✅ Query results exported to {filePath}");
    }
}