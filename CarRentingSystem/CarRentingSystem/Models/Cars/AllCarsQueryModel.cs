using System.Collections.Generic;
using System.ComponentModel;

namespace CarRentingSystem.Models.Cars
{
    public class AllCarsQueryModel
    {
        public IEnumerable<string> Brands { get; set; }

        [DisplayName("Search")]
        public IEnumerable<string> SearchCrit { get; set; }

        public CarSorting Sorting { get; set; }

        public IEnumerable<CarListingViewModel> Cars { get; set; }
    }
}
