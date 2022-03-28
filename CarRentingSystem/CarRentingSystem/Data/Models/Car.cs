namespace CarRentingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Data.DataConstraints;
    using static Data.DataConstraints.DataConstants.Car;

    public class Car
    {
        public int Id { get; set; }

        [Required]
        [StringLength(BrandMaxLength)]
        [MinLength(2)]
        public string Make { get; set; }

        [Required]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int Year { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int DealerId { get; set; }

        public Dealer Dealer { get; set; }
    }
}
