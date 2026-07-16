using hardwareAPI.Data;
using hardwareAPI.Interfaces;
using hardwareAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace hardwareAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(
            string? category,
            string? search,
            bool lowStock)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(x => x.Category == category);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.Name.Contains(search) ||
                    x.Sku.Contains(search));
            }

            if (lowStock)
            {
                query = query.Where(x =>
                    x.Quantity <= x.MinStockLevel);
            }

            return await query
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            return product;
        }

        public async Task<Product?> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            return product;
        }

        public async Task AddPurchaseItemAsync(PurchaseItem purchaseItem)
        {
            await _context.PurchaseItems.AddAsync(purchaseItem);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return false;

            _context.Products.Remove(product);

            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}