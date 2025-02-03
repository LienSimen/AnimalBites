using System;
using System.ComponentModel;
using AnimalBites.controller;
using System.Security.Cryptography;

namespace AnimalBites.App;

public class BiteDataApp(DogController dogController)
{
    private readonly DogController _dogController = dogController;
    private bool isRunning = true;

    public void Run()
    {
        while(isRunning)
        {
            Console.WriteLine("Welcome the bite data analysis");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("press 1 to see most aggressive dog breed");
            Console.WriteLine("press 2 to see least aggressive dog breed");
            Console.WriteLine("Press 3 to see longest quarantine");
            Console.WriteLine("Press 4 to see most bites by year");
            Console.WriteLine("Press 5 to see bites by gender");
            Console.WriteLine("Press 6 to see bites by area");
            Console.WriteLine("Press 7 to see bites by zip");
            Console.WriteLine("Press 8 to see bites by County");
            Console.WriteLine("Press 9 to see bites by City");
            Console.WriteLine("Press 10 to see bites by species");
            Console.WriteLine("Press 11 to make your own query");
            Console.WriteLine("Press 12 for advanced query");
            Console.WriteLine("Press q to exit");

            var input = Console.ReadLine();
            switch(input)
            {
                case "1":
                    _dogController.ShowMostAggressiveBreed();
                    break;
                case "2":
                    _dogController.ShowLeastAggressiveBreed();
                    break;
                case "3":
                    _dogController.LongestQuarantine();
                    break;
                case "4":
                    _dogController.MostBitesByYear();
                    break;
                case "5":
                    _dogController.BitesByGender();
                    break;
                case "6":
                    _dogController.BitesByArea();
                    break;
                case "7":
                    _dogController.BitesByZip();
                    break;
                case "8":
                    _dogController.BitesByCounty();
                    break;
                case "9":
                    _dogController.BitesByCity();
                    break;
                case "10":
                    _dogController.BitesBySpecies();
                    break;
                case "11":
                    var (query, quarantineInput, breedInput) = _dogController.QueryBuilder();
                    Console.WriteLine("Here comes the result from your query");
                    Console.WriteLine($"We found {query.Count()} results");
                    
                    foreach (var bites in query)
                    {
                        //mega awesome
                        var isDog = bites.Species.ToLower() == "dog"
                            ? $"Bite from a {bites.Breed}"
                            : $"Bite from a {bites.Species}";

                        var bitesDate = bites.BiteDate.HasValue
                            ? $"on the {bites.BiteDate:d}."
                            : "";

                        var quarantined = quarantineInput?.ToLower() == "y" && bites.DaysInQuarantine > 0
                            ? $"And they were in quarantine for {bites.DaysInQuarantine} days."
                            : "";

                        Console.WriteLine($"{isDog} is a {bites.Gender?.ToLower()}, and they bit the {bites.BiteArea}, {bitesDate} {quarantined}");
                    }
                    break;
                case "12":
                    var results = _dogController.RunAdvancedQuery(); // ✅ Run query once

                    if (!results.Any())
                    {
                        Console.WriteLine("No results found.");
                        break;
                    }

                    _dogController.DisplayQueryResults(results); // ✅ Directly display results

                    Console.Write("\n💾 Do you want to save these results to a CSV file? (Y/N): ");
                    string? saveToCsv = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(saveToCsv) && saveToCsv.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        _dogController.ExportQueryToCSV(results); // ✅ Pass stored results
                    }
                    break;

                case "q":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Wrong info try again");
                    continue;
            }
        }
    }
}
