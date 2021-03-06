using CarRentingSystem.Data;
using System.Collections.Generic;
using System.Linq;

namespace CarRentingSystem.Areas.Admin.Service
{
    public class AdminService : IAdminService
    {
        private readonly CarRentingDbContext data;

        public AdminService(CarRentingDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<ScheduleModel> Schedule(int records)
        {
            var userCars = this.data.UsersCars
                .OrderByDescending(c => c.UserId)
                .Select(c => new ScheduleModel()
                {
                    Make = c.Car.Make,
                    Model = c.Car.Model,
                    Year = c.Car.Year,
                    Information = c.User.FullName + " owned on " + c.RentDate.Date.ToString()
                }).Take(records);

            return userCars;
        }
    }
}
