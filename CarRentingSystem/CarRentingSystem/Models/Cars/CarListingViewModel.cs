using CarRentingSystem.Data.DataConstraints;
using System.ComponentModel.DataAnnotations;

namespace CarRentingSystem.Models.Cars
{
    public class CarListingViewModel
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public int Year { get; set; }

        public string Category { get; set; }
    }
}
