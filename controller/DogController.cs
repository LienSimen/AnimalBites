using System.Formats.Asn1;
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

}