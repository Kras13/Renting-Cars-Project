using CarRentingSystem.Data;
using CarRentingSystem.Service.Models;
using System.Linq;

namespace CarRentingSystem.Service
{
    public class SummaryService : ISummaryService
    {
        private readonly CarRentingDbContext data;

        public SummaryService(CarRentingDbContext data)
        {
            this.data = data;
        }

        public SummaryServiceModel Total()
        {
            var totalCars = this.data.Cars.Count();
            var totalUsers = this.data.Users.Count();

            return new SummaryServiceModel() { TotalUsers = totalUsers, TotalCars = totalCars };

        }
    }
}
