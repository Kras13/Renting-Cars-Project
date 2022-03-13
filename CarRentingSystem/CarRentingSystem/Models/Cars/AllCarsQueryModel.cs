using System.Collections.Generic;

namespace CarRentingSystem.Models.Cars
{
    public class AllCarsQueryModel
    {
        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<string> SearchTerm { get; set; }

        public CarSorting Sorting { get; set; }

        public IEnumerable<CarListingViewModel> Cars { get; set; }
    }
}
