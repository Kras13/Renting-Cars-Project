using CarRentingSystem.Service.Car;
using System.Collections.Generic;
using System.ComponentModel;

namespace CarRentingSystem.Models.Cars
{
    public class AllCarsQueryModel
    {
        public int CarsPerPage { get; set; } = 2;

        public string Brand { get; set; }

        [DisplayName("Brands")]
        public IEnumerable<string> Brands { get; set; }

        [DisplayName("Search")]
        public string SearchCrit { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalCars { get; set; }

        [DisplayName("Sort by")]
        public CarSorting Sorting { get; set; }

        public IEnumerable<CarServiceModel> Cars { get; set; }
    }
}
