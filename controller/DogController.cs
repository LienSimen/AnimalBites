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

}
