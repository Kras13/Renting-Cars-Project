using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarRentingSystem.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string CategoryUrl { get; set; }

        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
    }
}
