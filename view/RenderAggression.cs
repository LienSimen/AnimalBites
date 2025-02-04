using AnimalBites.model;

namespace AnimalBites.view;

public class RenderAggression
{
    public void DisplayMostAggressive(List<(string Breed, int Count)> breeds)
    {
        if (breeds.Any())
        {
            Console.WriteLine("Most aggressive breeds (from most to least):");
            foreach (var (breed, count) in breeds)
            {
                Console.WriteLine($"{breed}: {count} bites");
            }
        }
        else
        {
            Console.WriteLine("No data available.");
        }
    }

    public void DisplayLeastAggressive(string breed)
    {
        Console.WriteLine($"The least aggressive is: {breed}");
    }

    public void DisplayBiteArea(List<(string Area, int Count)> areas)
    {
        if (areas.Any())
        {
            Console.WriteLine("Where people get bit (from most to least):");
            foreach (var (area, count) in areas)
            {
                Console.WriteLine($"{area}: {count} bites");
            }
        }
        else
        {
            Console.WriteLine("No data available.");
        }
    }

    public void DisplayColor(List<(string Color, int Count)> colors)
    {
        if (colors.Any())
        {
            Console.WriteLine("Colors of animals that bite.");
            foreach (var (color, count) in colors)
            {
                Console.WriteLine($"{color}, {count} times.");
            }
        }
        else
        {
            Console.WriteLine("No data available.");
        }
    }

    public void DisplayBitesBySpecies(List<(string Area, int Count)> species)
    {
        if (species.Any())
        {
            Console.WriteLine("Bites by species:");
            foreach (var (specie, count) in species)
            {
                Console.WriteLine($"{specie}: {count} bites");
            }
        }
        else
        {
            Console.WriteLine("No data available.");
        }
    }
    public void DisplayBiteZip(List<(string Zip, int Count)> zips)
    {
        if (zips.Any())
        {
            Console.WriteLine("Bites by ZIP code (from most to least):");
            foreach (var (zip, count) in zips)
            {
                Console.WriteLine($"{zip}: {count} bites");
            }
        }
        else
        {
            Console.WriteLine("No data available.");
        }
    }

    public void DisplayBitesByCity(List<(string City, int Count)> cities)
    {
        if (cities.Any())
        {
            Console.WriteLine("Bites by city (from most to least):");
            foreach (var (city, count) in cities)
            {
                Console.WriteLine($"{city}: {count} bites");
            }
        }
        else
        {
            Console.WriteLine("No data available.");
        }
    }

    public void DisplayBitesByCounty(List<(string County, int Count)> counties)
    {
        if (counties.Any())
        {
            Console.WriteLine("Bites by county (from most to least):");
            foreach (var (county, count) in counties)
            {
                Console.WriteLine($"{county}: {count} bites");
            }
        }
        else
        {
            Console.WriteLine("No data available.");
        }
    }
    public void DisplayBitesByGender(List<(string Gender, int Count)> genders)
    {
        if (genders.Any())
        {
            Console.WriteLine("Most aggressive genders (from most to least):");
            foreach (var (gender, count) in genders)
            {
                Console.WriteLine($"{gender}: {count} bites");
            }
        }
        else
        {
            Console.WriteLine("No data available.");
        }
    }

    public void DisplayLongestQuarantine(int? days)
    {
        if (days.HasValue)
        {
            Console.WriteLine($"The longest quarantine lasted {days.Value} days");
        }
        else
        {
            Console.WriteLine("No data available for quarantine");
        }
    }


    public void DisplayMostBitesByYear(List<dynamic> years)
    {
        if (years.Count > 0)
        {
            Console.WriteLine("Bites per year (most to least):");
            foreach (var year in years)
            {
                Console.WriteLine($"{year.Year}: {year.Count} bites");
            }
        }
        else
        {
            Console.WriteLine("No data available for bite years.");
        }
    }

    public void DisplayQueryResults(List<BiteData> results)
    {
        if (!results.Any())
        {
            Console.WriteLine("No results found.");
            return;
        }

        Console.WriteLine("\n🔍 Query Results:");

        foreach (var bite in results)
        {
            string speciesInfo = bite.Species.ToLower() == "dog"
                ? $"Bite from a {bite.Breed}"
                : $"Bite from a {bite.Species}";

            string biteDate = bite.BiteDate.HasValue ? $"on {bite.BiteDate:d}" : "Date Unknown";
            string quarantined = bite.DaysInQuarantine > 0 ? $"Quarantined for {bite.DaysInQuarantine} days." : "";

            Console.WriteLine($"📍 City: {bite.City}, County: {bite.County}, State: {bite.State}");
            Console.WriteLine($"🐾 {speciesInfo}, {bite.Gender?.ToLower()} - Bit the {bite.BiteArea}, {biteDate} {quarantined}");
            Console.WriteLine("----------------------------------------------------");
        }
    }
}