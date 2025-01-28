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
