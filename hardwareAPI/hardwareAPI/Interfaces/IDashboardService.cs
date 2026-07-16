using hardwareAPI.DTOs.Dashboard;

namespace hardwareAPI.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync();
    }
}