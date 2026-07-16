using hardwareAPI.DTOs.Sale;

namespace hardwareAPI.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleResponseDto>> GetAllAsync();

        Task<SaleResponseDto> CreateSaleAsync(CreateSaleDto dto);
    }
}