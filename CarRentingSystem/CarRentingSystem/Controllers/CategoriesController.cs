using CarRentingSystem.Data;
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


    }
}
