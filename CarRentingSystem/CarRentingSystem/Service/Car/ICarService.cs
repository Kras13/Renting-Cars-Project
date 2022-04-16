using CarRentingSystem.Models.Cars;
using System.Collections.Generic;

namespace CarRentingSystem.Service.Car
{
    public interface ICarService
    {
        CarQueryServiceModel QueriedCars(
            string brand,
            string searchCrit,
            CarSorting sorting,
            int currentPage,
            int carsPerPage);

        IEnumerable<CarIndexServiceModel> SelectedTopCars(int count);

        void SaveCar(SaveCarServiceModel car);

        CarDetailsServiceModel CarDetailsById(int id);

        IEnumerable<CategoryServiceModel> GetCategories();

        IEnumerable<CarServiceModel> GetDealerCars(string dealerId);

        IEnumerable<CarServiceModel> GetCarsByCategoryId(int categoryId);

        bool Edit(int id, string brand, string model, string description, int year, int dealerId);


    }
}
