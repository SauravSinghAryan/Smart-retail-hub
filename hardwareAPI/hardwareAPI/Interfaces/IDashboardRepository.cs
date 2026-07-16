using hardwareAPI.DTOs.Dashboard;

namespace hardwareAPI.Interfaces
{
    public interface IDashboardRepository
    {
        Task<int> GetTotalProductsAsync();

        Task<int> GetLowStockProductsAsync();

        Task<decimal> GetTotalSalesAsync();

        Task<decimal> GetTotalPurchasesAsync();

        Task<List<RecentSaleDto>> GetRecentSalesAsync();

        Task<List<RecentPurchaseDto>> GetRecentPurchasesAsync();

        Task<List<SalesTrendDto>> GetSalesTrendAsync();
    }
}