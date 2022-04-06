using CarRentingSystem.Data;
using System.Linq;

namespace CarRentingSystem.Service.User
{
    public class UserService : IUserService
    {
        private readonly CarRentingDbContext data;

        public UserService(CarRentingDbContext data)
        {
            this.data = data;
        }

        public UserServiceModel CurrentUser(string id)
        {
            var dealerId = this.data
                .Dealers
                .Where(d => d.UserId == id)
                .Select(d => d.Id)
                .FirstOrDefault();

            return new UserServiceModel()
            {
                Id = id,
                IsDealer = dealerId > 0 ? true : false,
                DealerId = dealerId
            };
        }
    }
}
