using CarRentingSystem.Service.Car;
using System.Collections.Generic;

namespace CarRentingSystem.Service.UserCar
{
    public class UserCarsServiceModel
    {
        public int UserId { get; set; }

        public IEnumerable<CarServiceModel> Cars { get; set; }
    }
}
