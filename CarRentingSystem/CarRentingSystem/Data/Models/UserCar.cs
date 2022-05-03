using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentingSystem.Data.Models
{
    public class UserCar
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }
        public Car Car { get; set; }

        public DateTime RentDate { get; set; }
    }
}
