using hardwareAPI.DTOs.Sale;
using hardwareAPI.Interfaces;
using hardwareAPI.Models;

namespace hardwareAPI.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<IEnumerable<SaleResponseDto>> GetAllAsync()
        {
            var sales = await _saleRepository.GetAllAsync();

            return sales.Select(s => new SaleResponseDto
            {
                Id = s.Id,
                SaleNumber = s.SaleNumber,
                CustomerName = s.CustomerName,
                SaleDate = s.SaleDate,
                TotalAmount = s.TotalAmount
            });
        }

        public async Task<SaleResponseDto> CreateSaleAsync(CreateSaleDto dto)
        {
            decimal subtotal = dto.Items.Sum(i => i.Quantity * i.PricePerUnit);

            decimal taxAmount = (subtotal - dto.Discount) * (dto.TaxRate / 100);

            decimal totalAmount = subtotal - dto.Discount + taxAmount;

            string saleNumber = $"SAL-{DateTime.UtcNow:yyyyMMddHHmmss}";

            var sale = new Sale
            {
                SaleNumber = saleNumber,
                CustomerName = dto.CustomerName,
                CustomerPhone = dto.CustomerPhone,
                SaleDate = DateTime.UtcNow,
                TaxRate = dto.TaxRate,
                Discount = dto.Discount,
                PaymentMethod = dto.PaymentMethod,
                TotalAmount = totalAmount
            };

            await _saleRepository.AddAsync(sale);

            foreach (var item in dto.Items)
            {
                var product = await _saleRepository.GetProductByIdAsync(item.ProductId);

                if (product == null)
                    continue;

                // Check Stock
                if (product.Quantity < item.Quantity)
                    throw new Exception($"{product.Name} has only {product.Quantity} items in stock.");

                // Reduce Stock
                product.Quantity -= item.Quantity;

                var saleItem = new SaleItem
                {
                    Sale = sale,
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    PricePerUnit = item.PricePerUnit,
                    SubTotal = item.Quantity * item.PricePerUnit
                };

                await _saleRepository.AddSaleItemAsync(saleItem);
            }

            await _saleRepository.SaveChangesAsync();

            return new SaleResponseDto
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                CustomerName = sale.CustomerName,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount
            };
        }
    }
}