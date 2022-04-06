using CarRentingSystem.Data;
using CarRentingSystem.Models.Cars;
using System.Collections.Generic;
using System.Linq;

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
                .Skip((currentPage - 1) * AllCarsQueryModel.CarsPerPage)
                .Take(AllCarsQueryModel.CarsPerPage)
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
                TotalCars = totalCarsWithFilter
            };
        }
    }
}
