namespace hardwareAPI.DTOs.Dashboard
{
    public class DashboardStatsDto
    {
        public int TotalProducts { get; set; }

        public int LowStockProducts { get; set; }

        public decimal TotalSales { get; set; }

        public decimal TotalPurchases { get; set; }

        public List<RecentSaleDto> RecentSales { get; set; } = new();

        public List<RecentPurchaseDto> RecentPurchases { get; set; } = new();

        public List<SalesTrendDto> SalesTrend { get; set; } = new();
    }
}