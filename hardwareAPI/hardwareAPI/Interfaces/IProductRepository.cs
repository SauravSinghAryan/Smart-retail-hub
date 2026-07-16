using hardwareAPI.Models;

namespace hardwareAPI.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(
            string? category,
            string? search,
            bool lowStock);

        Task<Product?> GetByIdAsync(int id);

        Task<Product> AddAsync(Product product);

        Task<Product?> UpdateAsync(Product product);

        Task<bool> DeleteAsync(int id);

        Task SaveChangesAsync();
    }
}
