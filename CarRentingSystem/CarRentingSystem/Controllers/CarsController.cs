﻿using CarRentingSystem.Data;
using CarRentingSystem.Data.Models;
using CarRentingSystem.Models.Cars;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CarRentingSystem.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarRentingDbContext data;

        public CarsController(CarRentingDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add()
        {
            return View(new AddCarFormModel
            {
                Categories = this.GetCarCategories()
            });
        }

        public IActionResult All()
        {
            var cars = this.data
                .Cars
                .OrderByDescending(c => c.Id)
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

            return View(cars);
        }

        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
        {
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
                    CategoryId = car.CategoryId
                });
            this.data.SaveChanges();

            TempData["Success"] = "Car added successfully!";

            return RedirectToAction(nameof(All));
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
