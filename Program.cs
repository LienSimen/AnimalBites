using AnimalBites.controller;
using AnimalBites.view;
using AnimalBites.model;
using AnimalBites.App;

namespace AnimalBites;

class Program
{
    static void Main(string[] args)
    {
        var bitedata = new DataDump();

        var biteDisplayer = new RenderAggression();

        var dogController = new DogController(bitedata, biteDisplayer);

        var app = new BiteDataApp(dogController);

        app.Run();
    }
}
