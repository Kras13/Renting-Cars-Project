using CarRentingSystem.Models.Cars;
using CarRentingSystem.Service.UserCar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CarRentingSystem.Controllers
{
    public class UsersCarsController : Controller
    {
        private readonly IUserCarService userCarService;

        public UsersCarsController(IUserCarService userCarService)
        {
            this.userCarService = userCarService;
        }

        [Authorize]
        public IActionResult Rent()
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
        [HttpPost]
        public IActionResult Rent(int id)
        {
            // add validation if car exists

            // if successfull

            ViewBag.SuccessfullyRentedCar = true;

            return RedirectToAction("Index", "Home");
        }
    }
}
