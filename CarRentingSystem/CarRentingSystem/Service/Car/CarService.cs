using CarRentingSystem.Data;
using CarRentingSystem.Models.Cars;
using CarRentingSystem.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CarRentingSystem.Service.Car
{
    public class CarService : ICarService
    {
        private readonly CarRentingDbContext data;

        public CarService(CarRentingDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<CarIndexServiceModel> SelectedTopCars(int count)
        {
            return this.data
                .Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarIndexServiceModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl
                })
                .Take(count)
                .AsEnumerable();
        }

        public CarQueryServiceModel QueriedCars(
            string brand,
            string searchCrit,
            CarSorting sorting,
            int currentPage,
            int carsPerPage)
        {
            var carsQuery = this.data.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(brand))
            {
                carsQuery = carsQuery.Where(c => c.Make == brand);
            }

            if (!string.IsNullOrEmpty(searchCrit))
            {
                carsQuery = carsQuery.Where(c =>
                c.Make.ToLower().Contains(searchCrit.ToLower()) ||
                c.Model.ToLower().Contains(searchCrit.ToLower()) ||
                c.Description.ToLower().Contains(searchCrit.ToLower()));
            }

            switch (sorting)
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
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage)
                .Select(c => new CarListingViewModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl,
                    Category = c.Category.Name
                }).AsEnumerable();

            return new CarQueryServiceModel()
            {
                Brand = brand,
                Cars = cars.Select(c => new CarServiceModel()
                {
                    Id = c.Id,
                    Category = c.Category,
                    ImageUrl = c.ImageUrl,
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.Year
                }),
                CarsPerPage = carsPerPage,
                CurrentPage = currentPage,
                SearchCrit = searchCrit,
                Sorting = sorting,
                TotalCars = totalCarsWithFilter,
                Brands = this.data.Cars.Select(c => c.Make).Distinct()
            };
        }

        public void SaveCar(SaveCarServiceModel car)
        {
            this.data.Cars.Add(new Data.Models.Car()
            {
                Make = car.Make,
                Model = car.Model,
                CategoryId = car.CategoryId,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                DealerId = car.DealerId
            });
            this.data.SaveChanges();
        }

        public CarDetailsServiceModel CarDetailsById(int id)
        {
            var selectedCar = this.data.Cars.FirstOrDefault(c => c.Id == id);

            CarDetailsServiceModel result = null;

            if (selectedCar != null)
            {
                result = new CarDetailsServiceModel()
                {
                    Id = selectedCar.Id,
                    Make = selectedCar.Make,
                    Model = selectedCar.Model,
                    ImageUrl = selectedCar.ImageUrl,
                    Description = selectedCar.Description,
                    Year = selectedCar.Year
                };
            }

            return result;
        }

        public IEnumerable<CategoryServiceModel> GetCategories()
        {
            return this.data.Categories.Select(c => new CategoryServiceModel()
            {
                Id = c.Id,
                Name = c.Name,
                CategoryUrl = c.CategoryUrl
            });
        }

        public IEnumerable<CarServiceModel> GetDealerCars(string dealerId)
        {
            return this.data.Cars
                .Where(c => c.Dealer.UserId == dealerId)
                .Select(c => new CarServiceModel()
                {
                    Id = c.Id,
                    Category = c.Category.Name,
                    ImageUrl = c.ImageUrl,
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.Year
                });
        }

        public IEnumerable<CarServiceModel> GetCarsByCategoryId(int categoryId)
        {
            return this.data.Cars
                .Where(c => c.CategoryId == categoryId)
                .Select(c => new CarServiceModel()
                {
                    Id = c.Id,
                    Category = c.Category.Name,
                    Make = c.Make,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl,
                    Year = c.Year
                });
        }

        public bool Edit(int id, string brand, string model, string description, int year, int dealerId)
        {
            var selectedCar = this.data.Cars.FirstOrDefault(c => c.Id == id && c.DealerId == dealerId);

            if (selectedCar == null)
            {
                throw new InvalidOperationException("Can not edi the selected car!");
            }

            selectedCar.Make = brand;
            selectedCar.Model = model;
            selectedCar.Description = description;
            selectedCar.Year = year;

            this.data.SaveChanges();

            return true;
        }
    }
}
