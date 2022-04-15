using CarRentingSystem.Areas.Admin.Service;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Areas.Admin.Controllers
{
    public class CarsController : AdminController
    {
        private readonly IAdminService adminService;

        public CarsController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Schedule()
        {
            var cars = this.adminService.Schedule(3);

            return View(cars);
        }
    }
}
