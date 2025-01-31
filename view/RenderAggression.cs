using System;
using System.IO.Compression;

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
    public void DisplayBiteZip(List<(string Area, int Count)> zips)
    {
        if (zips.Any())
        {
            Console.WriteLine("What zip area people get bit (from most to least):");
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

}