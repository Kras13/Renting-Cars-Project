namespace CarRentingSystem.Service.Car
{
    public class SaveCarServiceModel
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int Year { get; set; }

        public int CategoryId { get; set; }

        public int DealerId { get; set; }
    }
}
