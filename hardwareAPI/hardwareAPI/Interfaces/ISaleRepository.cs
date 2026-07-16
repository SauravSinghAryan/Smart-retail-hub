using hardwareAPI.Models;

namespace hardwareAPI.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllAsync();

        Task AddAsync(Sale sale);

        Task AddSaleItemAsync(SaleItem saleItem);

        Task<Product?> GetProductByIdAsync(int productId);

        Task SaveChangesAsync();
    }
}