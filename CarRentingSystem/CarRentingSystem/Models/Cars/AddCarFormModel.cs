namespace CarRentingSystem.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.DataConstraints;
    public class AddCarFormModel
    {
        [Required]
        [MaxLength(DataConstants.CarBrandMaxLength)]
        [MinLength(2, ErrorMessage = "Please enter more than 2 symbols")]
        public string Make { get; set; }

        [Required]
        [MaxLength(DataConstants.CarModelMaxLength)]
        [MinLength(2,ErrorMessage = "Please enter more than 2 symbols")]
        public string Model { get; set; }

        [Required]
        [MaxLength(DataConstants.CarDescriptionMaxLength)]
        [MinLength(10,ErrorMessage ="Please enter more than 10 symbols")]
        public string Description { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [Range(1980,2018,ErrorMessage ="Year should be in range 1980-2018!")]
        public int Year { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<CarCategoryViewModel> Categories { get; set; }
    }
}