using hardwareAPI.DTOs;
using hardwareAPI.DTOs.Billing;

namespace hardwareAPI.Interfaces
{
    public interface IBillingService
    {
        Task<IEnumerable<object>> GetInvoicesAsync();

        Task<InvoiceResponseDto?> GetInvoiceAsync(int saleId);
    }
}