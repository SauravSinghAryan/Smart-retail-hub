using hardwareAPI.Data;
using hardwareAPI.Interfaces;
using hardwareAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace hardwareAPI.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _context;

        public SaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _context.Sales
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();
        }

        public async Task AddAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
        }

        public async Task AddSaleItemAsync(SaleItem saleItem)
        {
            await _context.SaleItems.AddAsync(saleItem);
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}