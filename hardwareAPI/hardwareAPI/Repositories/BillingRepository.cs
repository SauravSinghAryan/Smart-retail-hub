using hardwareAPI.Data;
using hardwareAPI.Interfaces;
using hardwareAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace hardwareAPI.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        private readonly ApplicationDbContext _context;

        public BillingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Sale?> GetSaleWithItemsAsync(int saleId)
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
                .FirstOrDefaultAsync(s => s.Id == saleId);
        }
    }
}