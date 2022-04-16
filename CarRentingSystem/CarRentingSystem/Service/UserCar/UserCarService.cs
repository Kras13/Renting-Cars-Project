using CarRentingSystem.Data;
using CarRentingSystem.Service.Car;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentingSystem.Service.UserCar
{
    public class UserCarService : IUserCarService
    {
        private readonly CarRentingDbContext data;

        public UserCarService(CarRentingDbContext data)
        {
            this.data = data;
        }

        public bool CarFree(int carId)
        {
            var usersCars = this.data.UsersCars.Where(c => c.CarId == carId);

            foreach (var userCar in usersCars)
            {
                if (userCar.RentDate == DateTime.Now.Date)
                {
                    return false;
                }
            }

            return true;
        }

        public List<CarServiceModel> FindFreeCars()
        {
            List<CarServiceModel> result = new List<CarServiceModel>();

            foreach (var car in this.data.Cars.Include(c => c.Category))
            {
                if (CarFree(car.Id))
                {
                    result.Add(new CarServiceModel()
                    {
                        Id = car.Id,
                        Make = car.Make,
                        Category = car.Category.Name,
                        ImageUrl = car.ImageUrl,
                        Model = car.Model,
                        Year = car.Year
                    });
                }
            }

            return result;
        }

        public bool RentCar(string userId, int carId, DateTime rentDate)
        {
            if (!CarFree(carId))
            {
                throw new InvalidOperationException("Can not rent an used car!");
            }

            this.data.UsersCars.Add(new Data.Models.UserCar()
            {
                CarId = carId,
                UserId = userId,
                RentDate = rentDate
            });

            this.data.SaveChanges();

            return true;
        }
    }
}