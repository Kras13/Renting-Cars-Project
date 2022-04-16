using CarRentingSystem.Controllers;
using CarRentingSystem.Models.Home;
using CarRentingSystem.Service;
using CarRentingSystem.Service.Car;
using CarRentingSystem.Test.Moqs;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CarRentingSystem.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void ErrorShouldReturnView()
        {
            var homeController = new HomeController(null, null);

            var result = homeController.Error();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            var data = DatabaseMoq.Instance;

            var carService = new CarService(data);
            var summaryService = new SummaryService(data);

            var homeController = new HomeController(summaryService, carService);

            var result = homeController.Index();

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);
        }
    }
}
