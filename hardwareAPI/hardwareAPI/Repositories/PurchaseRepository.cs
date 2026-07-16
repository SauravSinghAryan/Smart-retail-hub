using hardwareAPI.Data;
using hardwareAPI.Interfaces;
using hardwareAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace hardwareAPI.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _context;

        public PurchaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Purchase>> GetAllAsync()
        {
            return await _context.Purchases
                .OrderByDescending(p => p.PurchaseDate)
                .ToListAsync();
        }

        public async Task AddAsync(Purchase purchase)
        {
            await _context.Purchases.AddAsync(purchase);
        }

        // ⭐ Missing Method
        public async Task AddPurchaseItemAsync(PurchaseItem purchaseItem)
        {
            await _context.PurchaseItems.AddAsync(purchaseItem);
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