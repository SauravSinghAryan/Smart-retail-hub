using hardwareAPI.DTOs.Purchase;
using hardwareAPI.Interfaces;
using hardwareAPI.Models;

namespace hardwareAPI.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<IEnumerable<PurchaseResponseDto>> GetAllAsync()
        {
            var purchases = await _purchaseRepository.GetAllAsync();

            return purchases.Select(p => new PurchaseResponseDto
            {
                PurchaseNumber = p.PurchaseNumber,
                SupplierName = p.SupplierName,
                PurchaseDate = p.PurchaseDate,
                TotalAmount = p.TotalAmount
            });
        }

        public async Task<bool> CreatePurchaseAsync(CreatePurchaseDto dto)
        {
            // Calculate Total Amount
            decimal totalAmount = dto.Items.Sum(i => i.Quantity * i.CostPerUnit);

            // Generate Purchase Number
            string purchaseNumber = $"PUR-{DateTime.UtcNow:yyyyMMddHHmmss}";

            // Create Purchase
            var purchase = new Purchase
            {
                PurchaseNumber = purchaseNumber,
                SupplierName = dto.SupplierName,
                PurchaseDate = DateTime.UtcNow,
                TotalAmount = totalAmount
            };

            // Save Purchase
            await _purchaseRepository.AddAsync(purchase);

            foreach (var item in dto.Items)
            {
                // Find Product
                var product = await _purchaseRepository.GetProductByIdAsync(item.ProductId);

                if (product == null)
                    continue;

                // Increase Stock
                product.Quantity += item.Quantity;

                // Update Latest Cost
                product.Cost = item.CostPerUnit;

                // Create Purchase Item
                var purchaseItem = new PurchaseItem
                {
                    Purchase = purchase,
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    CostPerUnit = item.CostPerUnit,
                    SubTotal = item.Quantity * item.CostPerUnit
                };

                await _purchaseRepository.AddPurchaseItemAsync(purchaseItem);
            }

            await _purchaseRepository.SaveChangesAsync();

            return true;
        }
    }
}