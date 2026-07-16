using hardwareAPI.Models;

namespace hardwareAPI.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<Purchase>> GetAllAsync();

        Task AddAsync(Purchase purchase);

        Task AddPurchaseItemAsync(PurchaseItem purchaseItem);

        Task<Product?> GetProductByIdAsync(int productId);

        Task SaveChangesAsync();
    }
}