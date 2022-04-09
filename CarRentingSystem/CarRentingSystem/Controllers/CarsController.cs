using CarRentingSystem.Models.Cars;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using CarRentingSystem.Infrastructure;
using CarRentingSystem.Service.Car;
using CarRentingSystem.Service.User;

namespace CarRentingSystem.Controllers
{
    public class CarsController : Controller
    {
        private readonly IUserService userService;
        private readonly ICarService carService;

        public CarsController(IUserService userService, ICarService carService)
        {
            this.userService = userService;
            this.carService = carService;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.userService.CurrentUser(User.GetId()).IsDealer)
            {
                return RedirectToAction("Create", "Dealers");
            }

            return View(new AddCarFormModel
            {
                Categories = this.carService.GetCategories().Select(c => new CarCategoryViewModel
                { Id = c.Id, Name = c.Name })
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddCarFormModel car)
        {
            var currentUser = userService.CurrentUser(User.GetId());

            if (!currentUser.IsDealer)
            {
                return RedirectToAction("Become", "Dealers");
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
                DealerId = currentUser.DealerId
            });

            TempData["Success"] = "Car added successfully!";

            return RedirectToAction(nameof(All));
        }


        public IActionResult All([FromQuery] AllCarsQueryModel query)
        {
            var resultView = this.carService
                .QueriedCars(query.Brand, query.SearchCrit, query.Sorting, query.CurrentPage, query.CarsPerPage);

            query.Cars = resultView.Cars.Select(c => new CarListingViewModel()
            {
                Id = c.Id,
                Category = c.Category,
                ImageUrl = c.ImageUrl,
                Make = c.Make,
                Model = c.Model,
                Year = c.Year
            });
            query.Brands = resultView.Brands;
            query.TotalCars = resultView.TotalCars;
            // query.CurrentPage = resultView.CurrentPage;
            //query.CarsPerPage = resultView.CarsPerPage;

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
            // check if currentUserIsDealer

            var currentUserCars = this.carService.GetDealerCars(User.GetId())
                .Select(c => new CarListingViewModel()
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.Year,
                    Category = c.Category,
                    ImageUrl = c.ImageUrl
                }).ToList();

            return View(currentUserCars);
        }
    }
}
