using CarRentingSystem.Models.Cars;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using CarRentingSystem.Infrastructure;
using CarRentingSystem.Service.Car;
using CarRentingSystem.Service.User;
using CarRentingSystem.Service.Dealer;
using System;

namespace CarRentingSystem.Controllers
{
    public class CarsController : Controller
    {
        private readonly IUserService userService;
        private readonly ICarService carService;
        private readonly IDealerService dealerService;

        public CarsController(IUserService userService, ICarService carService, IDealerService dealerService)
        {
            this.userService = userService;
            this.carService = carService;
            this.dealerService = dealerService;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!(User.IsAdmin() || dealerService.IsDealer(User.GetId())))
            {
                return RedirectToAction(nameof(DealersController.Create), "DealersController");
            }

            return View(new CarFormModel
            {
                Categories = this.carService.GetCategories().Select(c => new CarCategoryViewModel
                { Id = c.Id, Name = c.Name })
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(CarFormModel car)
        {
            if (!(User.IsAdmin() || dealerService.IsDealer(User.GetId())))
            {
                return RedirectToAction(nameof(DealersController.Create), "DealersController");
            }

            if (!this.carService.GetCategories().Any(c => c.Id == car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = this.carService.GetCategories().Select(c => new CarCategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                });

                return View(car);
            }

            this.carService.SaveCar(new SaveCarServiceModel()
            {
                Description = car.Description,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                ImageUrl = car.ImageUrl,
                CategoryId = car.CategoryId,
                DealerId = dealerService.GetId(User.GetId()).Value
            });

            TempData["Success"] = "Car added successfully!";

            return RedirectToAction(nameof(All));
        }


        public IActionResult All([FromQuery] AllCarsQueryModel query)
        {
            var resultView = this.carService
                .QueriedCars(query.Brand, query.SearchCrit, query.Sorting, query.CurrentPage, query.CarsPerPage);

            query.Cars = resultView.Cars;
            query.Brands = resultView.Brands;
            query.TotalCars = resultView.TotalCars;

            return View(query);
        }

        public IActionResult Details(int Id)
        {
            var selectedCar = this.carService.CarDetailsById(Id);

            CarDetailsViewModel carView = new CarDetailsViewModel()
            {
                Id = selectedCar.Id,
                Make = selectedCar.Make,
                Model = selectedCar.Model,
                ImageUrl = selectedCar.ImageUrl,
                Year = selectedCar.Year,
                Description = selectedCar.Description
            };

            return View(carView);
        }

        [Authorize]
        public IActionResult Mine()
        {
            if (!(User.IsAdmin() || dealerService.IsDealer(User.GetId())))
            {
                throw new InvalidOperationException("Cars/Mine...");
            }

            var currentUserCars = this.carService.GetDealerCars(User.GetId()).ToList();
            return View(currentUserCars);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            if (!(User.IsAdmin() || dealerService.IsDealer(User.GetId())))
            {
                return RedirectToAction(nameof(DealersController.Create), "DealersController");
            }

            var car = this.carService.CarDetailsById(id);

            var model = new CarFormModel()
            {
                Id = car.Id,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                CategoryId = car.CategoryId
            };

            model.Categories = this.carService.GetCategories().Select(c => new CarCategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name
            });

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, CarFormModel car)
        {
            bool isAdmin = User.IsAdmin();

            if (!(isAdmin || dealerService.IsDealer(User.GetId())))
            {
                return RedirectToAction(nameof(DealersController.Create), "DealersController");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = this.carService.GetCategories().Select(c => new CarCategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                });

                return View(car);
            }

            int dealerId = dealerService.GetId(User.GetId()).Value;

            this.carService.Edit(car.Id, car.Make, car.Model, car.Description, car.Year, dealerId, isAdmin);

            if (isAdmin)
            {
                return RedirectToAction(nameof(All));
            }

            return RedirectToAction(nameof(Mine));
        }
    }
}
