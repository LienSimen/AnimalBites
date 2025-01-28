using AnimalBites.model;
using AnimalBites.view;

namespace AnimalBites.controller;

public class DogController
{
    private readonly DataDump _dataDump;
    private readonly RenderAggression _view;

    public DogController(DataDump dataDump, RenderAggression view)
    {
        _dataDump = dataDump; // Inject DataDump instance
        _view = view;         // Inject RenderAggression instance
    }

    public void ShowMostAggressiveBreed()
    {
        var mostAggressive = _dataDump.Bites
            .Where(bite => !string.IsNullOrWhiteSpace(bite.Breed))
            .GroupBy(bite => bite.Breed)
            .OrderByDescending(group => group.Count())
            .FirstOrDefault();

        string result = mostAggressive?.Key ?? "No Data Available";
        _view.DisplayMostAggressive(result);
    }

    public void ShowLeastAggressiveBreed()
    {
        var leastAggressive = _dataDump.Bites
            .GroupBy(bite => bite.Breed)
            .OrderBy(group => group.Count())
            .FirstOrDefault();

        string result = leastAggressive?.Key ?? "No Data Available";
        _view.DisplayLeastAggressive(result);
    }

    public void LongestQuarantine()
    {
        var longestQuarantine = _dataDump.Bites
            .Where(bite => bite.Quarantine_Date.HasValue && bite.Release_Date.HasValue) // Ensure both dates exist
            .OrderByDescending(bite => bite.DaysInQuarantine)
            .FirstOrDefault();

        if (longestQuarantine != null)
        {
            _view.DisplayLongestQuarantine(longestQuarantine.DaysInQuarantine);
        }
        else
        {
            _view.DisplayLongestQuarantine(null);
        }
    }
}
