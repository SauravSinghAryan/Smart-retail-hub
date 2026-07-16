using hardwareAPI.DTOs.Product;

namespace hardwareAPI.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(
            string? category,
            string? search,
            bool lowStock);

        Task<ProductDto> CreateAsync(CreateProductDto dto);

        Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto);

        Task<bool> DeleteAsync(int id);
    }
}