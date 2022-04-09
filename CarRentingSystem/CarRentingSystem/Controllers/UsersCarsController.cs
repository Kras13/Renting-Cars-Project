using CarRentingSystem.Models.Cars;
using CarRentingSystem.Service.Car;
using CarRentingSystem.Service.UserCar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CarRentingSystem.Controllers
{
    public class UsersCarsController : Controller
    {
        private readonly IUserCarService userCarService;
        private readonly ICarService carService;

        public UsersCarsController(IUserCarService userCarService, ICarService carService)
        {
            this.userCarService = userCarService;
            this.carService = carService;
        }

        [Authorize]
        public IActionResult Available()
        {
            var freeCars = userCarService.FindFreeCars().Select(c => new CarListingViewModel() 
            {
                Id = c.Id,
                Make = c.Make,
                Category = c.Category,
                ImageUrl = c.ImageUrl,
                Model = c.Model,
                Year = c.Year
            });

            return View(freeCars);
        }

        [Authorize]
        public IActionResult Rent(int id)
        {
            var selectedCar = carService.CarDetailsById(id);

            return View(selectedCar);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Finalize(int id)
        {
            int name = 5;

            return View();
        }
    }
}
