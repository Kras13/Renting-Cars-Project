using CarRentingSystem.Controllers;
using CarRentingSystem.Models.Cars;
using CarRentingSystem.Service.Car;
using CarRentingSystem.Service.Dealer;
using CarRentingSystem.Test.Moqs;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace CarRentingSystem.Test.Controllers
{
    public class CarsControllerTest
    {
        [Fact]
        public void AddShouldThrowNullReference()
        {
            var data = DatabaseMoq.Instance;

            var carService = new CarService(data);
            var dealerService = new DealerService(data);

            var carsController = new CarsController(carService, dealerService);

            Assert.Throws<NullReferenceException>(carsController.Add);
        }

        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
        {
            var data = DatabaseMoq.Instance;

            var carService = new CarService(data);
            var dealerService = new DealerService(data);
            var queryModel = new AllCarsQueryModel();

            var carsController = new CarsController(carService, dealerService);

            var result = carsController.All(queryModel);

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            //    var model = viewResult.Model;

            //    var indexViewModel = Assert.IsType<IndexViewModel>(model);
        }

        //[Fact]
        //public void IndexShouldReturnViewWithCorrectModel()
        //{
        //    var data = DatabaseMoq.Instance;

        //    var carService = new CarService(data);
        //    var summaryService = new SummaryService(data);

        //    var homeController = new HomeController(summaryService, carService);

        //    var result = homeController.Index();

        //    Assert.NotNull(result);
        //    var viewResult = Assert.IsType<ViewResult>(result);

        //    var model = viewResult.Model;

        //    var indexViewModel = Assert.IsType<IndexViewModel>(model);
        //}
    }
}
