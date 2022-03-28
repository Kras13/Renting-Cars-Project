namespace CarRentingSystem.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstraints.DataConstants.Car;
    public class AddCarFormModel
    {
        [Required]
        [MaxLength(BrandMaxLength)]
        [MinLength(2, ErrorMessage = "Please enter more than 2 symbols")]
        public string Make { get; set; }

        [Required]
        [MaxLength(ModelMaxLength)]
        [MinLength(2, ErrorMessage = "Please enter more than 2 symbols")]
        public string Model { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        [MinLength(10, ErrorMessage = "Please enter more than 10 symbols")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [Range(1980, 2018, ErrorMessage = "Year should be in range 1980-2018!")]
        public int Year { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<CarCategoryViewModel> Categories { get; set; }
    }
}