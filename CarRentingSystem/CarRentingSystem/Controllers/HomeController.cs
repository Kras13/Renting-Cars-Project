namespace CarRentingSystem.Controllers
{
    using CarRentingSystem.Data;
    using CarRentingSystem.Models;
    using CarRentingSystem.Models.Home;
    using CarRentingSystem.Service;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly CarRentingDbContext data;
        private readonly ISummaryService summaryService;

        public HomeController(CarRentingDbContext data, ISummaryService summaryService)
        {
            this.data = data;
            this.summaryService = summaryService;
        }

        public IActionResult Index()
        {

            var cars = this.data
                .Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarIndexViewModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl
                })
                .Take(3)
                .ToList();

            var summary = this.summaryService.Total();

            return View(new IndexViewModel
            {
                TotalCars = summary.TotalCars,
                Cars = cars,
                TotalUsers = summary.TotalUsers
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
