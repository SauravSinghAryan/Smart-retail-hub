using hardwareAPI.Models;

namespace hardwareAPI.Interfaces
{
    public interface IBillingRepository
    {
        Task<Sale?> GetSaleWithItemsAsync(int saleId);
    }
}