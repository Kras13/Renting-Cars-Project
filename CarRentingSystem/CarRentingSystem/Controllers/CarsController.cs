using CarRentingSystem.Data;
using CarRentingSystem.Data.Models;
using CarRentingSystem.Models.Cars;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CarRentingSystem.Infrastructure;

namespace CarRentingSystem.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarRentingDbContext data;

        public CarsController(CarRentingDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!UserIsDealer())
            {
                return RedirectToAction("Create", "Dealers");
            }

            return View(new AddCarFormModel
            {
                Categories = this.GetCarCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddCarFormModel car)
        {
            var currentUserId = User.GetId();

            var dealerId = this.data
                .Dealers
                .Where(d => d.UserId == currentUserId)
                .Select(d => d.Id)
                .FirstOrDefault();

            if (dealerId == 0)
            {
                return RedirectToAction("Become", "Dealers");
            }

            if (!UserIsDealer())
            {
                return RedirectToAction("Create", "Dealers");
            }

            if (!this.data.Categories.Any(c => c.Id == car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = this.GetCarCategories();

                return View(car);
            }

            this.data.Cars
                .Add(new Car
                {
                    Description = car.Description,
                    Make = car.Make,
                    Model = car.Model,
                    Year = car.Year,
                    ImageUrl = car.ImageUrl,
                    CategoryId = car.CategoryId,
                    DealerId = dealerId
                });
            this.data.SaveChanges();

            TempData["Success"] = "Car added successfully!";

            return RedirectToAction(nameof(All));
        }


        public IActionResult All([FromQuery] AllCarsQueryModel query)
        {
            // TODO: implement better data sort

            var carsQuery = this.data.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(query.Brand))
            {
                carsQuery = carsQuery.Where(c => c.Make == query.Brand);
            }

            if (!string.IsNullOrEmpty(query.SearchCrit))
            {
                carsQuery = carsQuery.Where(c =>
                c.Make.ToLower().Contains(query.SearchCrit.ToLower()) ||
                c.Model.ToLower().Contains(query.SearchCrit.ToLower()) ||
                c.Description.ToLower().Contains(query.SearchCrit.ToLower()));
            }

            switch (query.Sorting)
            {
                case CarSorting.DateCreated:
                    carsQuery = carsQuery.OrderByDescending(c => c.Id);
                    break;
                case CarSorting.Year:
                    carsQuery = carsQuery.OrderByDescending(c => c.Year);
                    break;
                case CarSorting.BrandAndModel:
                    carsQuery = carsQuery.OrderByDescending(c => c.Make).ThenBy(c => c.Model);
                    break;
            }

            var totalCarsWithFilter = carsQuery.Count();

            var cars = carsQuery
                .Skip((query.CurrentPage - 1) * AllCarsQueryModel.CarsPerPage)
                .Take(AllCarsQueryModel.CarsPerPage)
                .Select(c => new CarListingViewModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl,
                    Category = c.Category.Name
                })
                .ToList();

            //var cars = carService.CarsByQuery(queryCurrentPage, CarsPerPage, );

            var carMakes = this.data
                .Cars
                .Select(c => c.Make)
                .Distinct()
                .ToList();

            query.TotalCars = totalCarsWithFilter;
            query.Brands = carMakes;
            query.Cars = cars;

            return View(query);
        }

        private bool UserIsDealer()
        {
            return this.data
                .Dealers
                .Any(d => d.UserId == User.GetId());
        }

        public IActionResult Details(int Id)
        {
            var selectedCar = data.Cars.FirstOrDefault(c => c.Id == Id);

            CarDetailsViewModel carView = new CarDetailsViewModel()
            {
                Make = selectedCar.Make,
                Model = selectedCar.Model,
                ImageUrl = selectedCar.ImageUrl,
                Year = selectedCar.Year,
                Description = selectedCar.Description
            };

            return View(carView);
        }

        private IEnumerable<CarCategoryViewModel> GetCarCategories()
        {
            return this.data.Categories
                .Select(c => new CarCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();
        }
    }
}
