using hardwareAPI.DTOs.Product;
using hardwareAPI.Interfaces;
using hardwareAPI.Models;

namespace hardwareAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(
            string? category,
            string? search,
            bool lowStock)
        {
            var products = await _repository.GetAllAsync(category, search, lowStock);

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Sku = p.Sku,
                Category = p.Category,
                Cost = p.Cost,
                Price = p.Price,
                Quantity = p.Quantity,
                MinStockLevel = p.MinStockLevel,
                Supplier = p.Supplier
            });
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Sku = dto.Sku,
                Category = dto.Category,
                Cost = dto.Cost,
                Price = dto.Price,
                Quantity = dto.Quantity,
                MinStockLevel = dto.MinStockLevel,
                Supplier = dto.Supplier
            };

            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Sku = product.Sku,
                Category = product.Category,
                Cost = product.Cost,
                Price = product.Price,
                Quantity = product.Quantity,
                MinStockLevel = product.MinStockLevel,
                Supplier = product.Supplier
            };
        }

        public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            product.Name = dto.Name;
            product.Sku = dto.Sku;
            product.Category = dto.Category;
            product.Cost = dto.Cost;
            product.Price = dto.Price;
            product.Quantity = dto.Quantity;
            product.MinStockLevel = dto.MinStockLevel;
            product.Supplier = dto.Supplier;

            await _repository.UpdateAsync(product);
            await _repository.SaveChangesAsync();

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Sku = product.Sku,
                Category = product.Category,
                Cost = product.Cost,
                Price = product.Price,
                Quantity = product.Quantity,
                MinStockLevel = product.MinStockLevel,
                Supplier = product.Supplier
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return false;

            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
