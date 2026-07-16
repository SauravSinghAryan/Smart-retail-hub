using hardwareAPI.DTOs.Dashboard;
using hardwareAPI.Interfaces;

namespace hardwareAPI.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var dashboard = new DashboardStatsDto
            {
                TotalProducts = await _dashboardRepository.GetTotalProductsAsync(),

                LowStockProducts = await _dashboardRepository.GetLowStockProductsAsync(),

                TotalSales = await _dashboardRepository.GetTotalSalesAsync(),

                TotalPurchases = await _dashboardRepository.GetTotalPurchasesAsync(),

                RecentSales = await _dashboardRepository.GetRecentSalesAsync(),

                RecentPurchases = await _dashboardRepository.GetRecentPurchasesAsync(),

                SalesTrend = await _dashboardRepository.GetSalesTrendAsync()
            };

            return dashboard;
        }
    }
}