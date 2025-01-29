using System;
using AnimalBites.controller;

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
