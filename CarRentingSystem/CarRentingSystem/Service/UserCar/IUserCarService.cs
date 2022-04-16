using CarRentingSystem.Service.Car;
using System;
using System.Collections.Generic;

namespace CarRentingSystem.Service.UserCar
{
    public interface IUserCarService
    {
        bool RentCar(string userId, int carId, DateTime date);

        bool CarFree(int carId);

        List<CarServiceModel> FindFreeCars();
    }
}