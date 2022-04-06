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
    }
}
