using hardwareAPI.DTOs.Purchase;

namespace hardwareAPI.Interfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseResponseDto>> GetAllAsync();

        Task<bool>CreatePurchaseAsync(CreatePurchaseDto dto);
    }
}
