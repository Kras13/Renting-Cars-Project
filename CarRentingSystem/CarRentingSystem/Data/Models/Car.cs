namespace CarRentingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Data.DataConstraints;
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [StringLength(DataConstants.CarBrandMaxLength)]
        [MinLength(2)]
        public string Make { get; set; }

        [Required]
        [MaxLength(DataConstants.CarModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(DataConstants.CarDescriptionMaxLength)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int Year { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
