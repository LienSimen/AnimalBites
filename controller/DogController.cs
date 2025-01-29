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
            .Where(bite => !string.IsNullOrWhiteSpace(bite.Breed))
            .GroupBy(bite => bite.Breed)
            .OrderByDescending(group => group.Count())
            .FirstOrDefault();

        string result = mostAggressive?.Key ?? "No Data Available";
        _view.DisplayMostAggressive(result);
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

    public void LongestQuarantine()
    {
        var longestQuarantine = _bites
            .Where(bite => bite.Quarantine_Date.HasValue && bite.Release_Date.HasValue)
            .OrderByDescending(bite => bite.DaysInQuarantine)
            .FirstOrDefault();

        _view.DisplayLongestQuarantine(longestQuarantine?.DaysInQuarantine);
    }

    
}
