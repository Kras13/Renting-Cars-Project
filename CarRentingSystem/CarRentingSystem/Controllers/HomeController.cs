namespace CarRentingSystem.Controllers
{
    using CarRentingSystem.Data;
    using CarRentingSystem.Models;
    using CarRentingSystem.Models.Home;
    using CarRentingSystem.Service;
    using CarRentingSystem.Service.Car;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly ISummaryService summaryService;
        private readonly ICarService carService;

        public HomeController(ISummaryService summaryService, ICarService carService)
        {
            this.summaryService = summaryService;
            this.carService = carService;
        }

        public IActionResult Index()
        {

            var cars = this.carService.SelectedTopCars(3);

            var summary = this.summaryService.Total();

            return View(new IndexViewModel
            {
                TotalCars = summary.TotalCars,
                Cars = cars.Select(c => new CarIndexViewModel 
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl,
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.Year
                }).ToList(),
                TotalUsers = summary.TotalUsers
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return View();
        }
    }
}
