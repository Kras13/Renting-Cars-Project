namespace CarRentingSystem.Service.Dealer
{
    public interface IDealerService
    {
        bool IsDealer(string userId);

        int? GetId(string userId);
    }
}
