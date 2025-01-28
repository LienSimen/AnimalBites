using System;

namespace AnimalBites.view;

public class RenderAggression
{
    public void DisplayMostAggressive(string breed)
    {
        Console.WriteLine($"The most aggressive is: {breed}");
    }

    public void DisplayLeastAggressive(string breed)
    {
        Console.WriteLine($"The least aggressive is: {breed}");
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
}