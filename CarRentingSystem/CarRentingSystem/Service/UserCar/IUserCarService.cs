using CarRentingSystem.Service.Car;
using System.Collections.Generic;

namespace CarRentingSystem.Service.UserCar
{
    public interface IUserCarService
    {
        bool RentCar(string userId, int carId, int days);

        bool CarFree(int carId);

        List<CarServiceModel> FindFreeCars();
    }
}