using CarRentingSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Controllers
{
    public class DealersController : Controller
    {
        private readonly CarRentingDbContext data;

        public DealersController(CarRentingDbContext dbContext)
        {
            this.data = dbContext;
        }


        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
    }
}
