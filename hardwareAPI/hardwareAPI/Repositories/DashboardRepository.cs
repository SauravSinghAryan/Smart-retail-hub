using hardwareAPI.Data;
using hardwareAPI.DTOs.Dashboard;
using hardwareAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hardwareAPI.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalProductsAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<int> GetLowStockProductsAsync()
        {
            return await _context.Products
                .CountAsync(p => p.Quantity <= 5);
        }

        public async Task<decimal> GetTotalSalesAsync()
        {
            return await _context.Sales
                .SumAsync(s => (decimal?)s.TotalAmount) ?? 0;
        }

        public async Task<decimal> GetTotalPurchasesAsync()
        {
            return await _context.Purchases
                .SumAsync(p => (decimal?)p.TotalAmount) ?? 0;
        }

        public async Task<List<RecentSaleDto>> GetRecentSalesAsync()
        {
            return await _context.Sales
                .OrderByDescending(s => s.SaleDate)
                .Take(5)
                .Select(s => new RecentSaleDto
                {
                    InvoiceNumber = s.SaleNumber,
                    CustomerName = s.CustomerName,
                    TotalAmount = s.TotalAmount,
                    SaleDate = s.SaleDate
                })
                .ToListAsync();
        }

        public async Task<List<RecentPurchaseDto>> GetRecentPurchasesAsync()
        {
            return await _context.Purchases
                .OrderByDescending(p => p.PurchaseDate)
                .Take(5)
                .Select(p => new RecentPurchaseDto
                {
                    PurchaseNumber = p.PurchaseNumber,
                    SupplierName = p.SupplierName,
                    TotalAmount = p.TotalAmount,
                    PurchaseDate = p.PurchaseDate
                })
                .ToListAsync();
        }

        public async Task<List<SalesTrendDto>> GetSalesTrendAsync()
        {
            var fromDate = DateTime.UtcNow.Date.AddDays(-6);

            return await _context.Sales
                .Where(s => s.SaleDate >= fromDate)
                .GroupBy(s => s.SaleDate.Date)
                .Select(g => new SalesTrendDto
                {
                    Date = g.Key,
                    Amount = g.Sum(x => x.TotalAmount)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }
    }
}
