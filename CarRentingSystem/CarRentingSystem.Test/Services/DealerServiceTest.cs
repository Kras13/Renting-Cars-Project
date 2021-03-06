using CarRentingSystem.Data.Models;
using CarRentingSystem.Service.Dealer;
using CarRentingSystem.Test.Moqs;
using Xunit;

namespace CarRentingSystem.Test.Services
{
    public class DealerServiceTest
    {
        [Fact]
        public void UserReturnTrueIfDealer()
        {
            const string userId = "TestUserId";

            var data = DatabaseMoq.Instance;
            data.Dealers.Add(new Dealer() { UserId = userId });
            data.SaveChanges();

            var dealerService = new DealerService(data);

            var result = dealerService.IsDealer(userId);

            Assert.True(result);
        }

        [Fact]
        public void ReturnDealerId()
        {
            const string userId = "TestUserId";

            var data = DatabaseMoq.Instance;
            data.Dealers.Add(new Dealer() { UserId = userId });
            data.SaveChanges();

            var dealerService = new DealerService(data);

            var dealerId = dealerService.GetId(userId);

            var result = dealerId > 0;

            Assert.True(result);
        }

        [Fact]
        public void ReturFalseWhenUserNotDealer()
        {
            const string userId = "TestUserId";

            var data = DatabaseMoq.Instance;
            data.Dealers.Add(new Dealer() { UserId = userId });
            data.SaveChanges();

            var dealerService = new DealerService(data);

            var result = dealerService.IsDealer("FakeUserId");

            Assert.True(!result);
        }
    }
}
