using CarRentingSystem.Service.Car;
using CarRentingSystem.Test.Moqs;
using System.Linq;
using Xunit;

namespace CarRentingSystem.Test.Services
{
    public class CarServiceTest
    {
        [Fact]
        public void SelectedTopCarsReturnTest()
        {
            const int recordCount = 3;

            var data = DatabaseMoq.Instance;

            var carService = new CarService(data);

            var taken = carService.SelectedTopCars(recordCount);

            bool result = taken.Count() <= recordCount;

            Assert.True(result);
        }
    }
}
