using CarRentingSystem.Models.Cars;
using CarRentingSystem.Models.Categories;
using CarRentingSystem.Service.Car;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CarRentingSystem.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICarService carService;

        public CategoriesController(ICarService carService)
        {
            this.carService = carService;
        }

        public IActionResult All()
        {
            var categories = carService
                .GetCategories()
                .Select(c => new CategoriesViewModel { Id = c.Id, Name = c.Name, CategoryUrl = c.CategoryUrl });

            return View(categories);
        }


        public IActionResult Cars(int id)
        {
            var selectedCars = this.carService.GetCarsByCategoryId(id)
                .Select(c => new CarListingViewModel()
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Category = c.Category,
                    ImageUrl = c.ImageUrl,
                    Year = c.Year
                }).ToList();

            return View(selectedCars);
        }
    }
}
