using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static CarRentingSystem.Data.DataConstraints.DataConstants.Dealer;

namespace CarRentingSystem.Data.Models
{
    public class Dealer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
    }
}
