using CarRentingSystem.Infrastructure;
using CarRentingSystem.Models.Cars;
using CarRentingSystem.Service.Car;
using CarRentingSystem.Service.Dealer;
using CarRentingSystem.Service.UserCar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CarRentingSystem.Controllers
{
    public class UsersCarsController : Controller
    {
        private readonly IUserCarService userCarService;
        private readonly ICarService carService;
        private readonly IDealerService dealerService;

        public UsersCarsController(IUserCarService userCarService, ICarService carService, IDealerService dealerService)
        {
            this.userCarService = userCarService;
            this.carService = carService;
            this.dealerService = dealerService;
        }

        [Authorize]
        public IActionResult Available()
        {
            var freeCars = userCarService.FindFreeCars();

            return View(freeCars);
        }

        [Authorize]
        public IActionResult Rent(int id)
        {
            if (User.IsAdmin() || dealerService.IsDealer(User.GetId()))
            {
                throw new InvalidOperationException("Dealers and admins can not rent cars");
            }

            var selectedCar = carService.CarDetailsById(id);

            return View(selectedCar);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Rent(int id, int days)
        {
            if (User.IsAdmin() || dealerService.IsDealer(User.GetId()))
            {
                throw new InvalidOperationException("Dealers and admins can not rent cars");
            }

            this.userCarService.RentCar(User.GetId(), id, DateTime.Now.Date);

            ViewBag.SuccessfullRent = true;

            return RedirectToAction("Rented", "Cars");
        }
    }
}
