using CarRentingSystem.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Controllers.Api
{
    [ApiController]
    [Route("api/cars")]
    public class CarsApiController : ControllerBase
    {
        private readonly CarRentingDbContext data;

        public CarsApiController(CarRentingDbContext data)
        {
            this.data = data;
        }
    }
}
