using CarRentingSystem.Data;
using CarRentingSystem.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingSystem.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CarRentingDbContext data;

        public CategoriesController(CarRentingDbContext data)
        {
            this.data = data;
        }

        public IActionResult All()
        {
            var categories = data.Categories
                .Select(c => new CategoriesViewModel { Id = c.Id, Name = c.Name, CategoryUrl = c.CategoryUrl });

            return View(categories);
        }

    }
}
