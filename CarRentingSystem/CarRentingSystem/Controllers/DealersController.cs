using CarRentingSystem.Data;
using CarRentingSystem.Data.Models;
using CarRentingSystem.Infrastructure;
using CarRentingSystem.Models.Dealers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        [HttpPost]
        [Authorize]
        public IActionResult Create(BecomeDealerFormModel candidateDealer)
        {
            var userId = this.User.GetId();

            var isUserDealer = this.data
                .Dealers
                .Any(d => d.UserId == userId);

            if (isUserDealer)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(candidateDealer);
            }

            var dealer = new Dealer()
            {
                Name = candidateDealer.Name,
                PhoneNumber = candidateDealer.PhoneNumber,
                UserId = userId
            };

            this.data.Dealers.Add(dealer);
            this.data.SaveChanges();



            return RedirectToAction("All", "Cars");
        }
    }
}
