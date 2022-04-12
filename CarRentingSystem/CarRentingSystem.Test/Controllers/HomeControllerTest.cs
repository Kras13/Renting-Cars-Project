using CarRentingSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CarRentingSystem.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void ErrorShouldReturnView()
        {
            // Arange
            var homeController = new HomeController(null, null);

            // Act
            var result = homeController.Error();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
