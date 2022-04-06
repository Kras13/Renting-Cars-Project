using CarRentingSystem.Models.Cars;
using CarRentingSystem.Service.Models;
using System.Collections.Generic;

namespace CarRentingSystem.Service.Car
{
    public class CarQueryServiceModel
    {
        public int CarsPerPage { get; set; }

        public string Brand { get; set; }

        public string SearchCrit { get; set; }

        public int CurrentPage { get; set; }

        public int TotalCars { get; set; }

        public CarSorting Sorting { get; set; }

        public IEnumerable<CarServiceModel> Cars { get; set; }
    }
}

/*
        public const int CarsPerPage = 3;

        public string Brand { get; set; }

        [DisplayName("Brands")]
        public IEnumerable<string> Brands { get; set; }

        [DisplayName("Search")]
        public string SearchCrit { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalCars { get; set; }

        [DisplayName("Sort by")]
        public CarSorting Sorting { get; set; }

        public IEnumerable<CarListingViewModel> Cars { get; set; }
 */
