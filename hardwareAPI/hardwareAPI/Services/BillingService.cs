using hardwareAPI.Data;
using hardwareAPI.DTOs;
using hardwareAPI.DTOs.Billing;
using hardwareAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hardwareAPI.Services
{
    public class BillingService : IBillingService
    {
        private readonly ApplicationDbContext _context;

        public BillingService(ApplicationDbContext context)
        {
            _context = context;
        }


        // Get all invoice records
        public async Task<IEnumerable<object>> GetInvoicesAsync()
        {
            var invoices = await _context.Sales
                .OrderByDescending(x => x.SaleDate)
                .Select(x => new
                {
                    id = x.Id,
                    invoiceNumber = x.SaleNumber,
                    customerName = x.CustomerName,
                    customerPhone = x.CustomerPhone,
                    saleDate = x.SaleDate,
                    paymentMethod = x.PaymentMethod,
                    totalAmount = x.TotalAmount
                })
                .ToListAsync();

            return invoices;
        }


        // Get invoice details
        public async Task<InvoiceResponseDto?> GetInvoiceAsync(int saleId)
        {
            var sale = await _context.Sales
                .Include(x => x.SaleItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == saleId);


            if (sale == null)
            {
                return null;
            }


            var subTotal = sale.SaleItems.Sum(x => x.SubTotal);


            var taxAmount = (subTotal - sale.Discount) * sale.TaxRate / 100;


            var invoice = new InvoiceResponseDto
            {
                InvoiceNumber = sale.SaleNumber,

                CustomerName = sale.CustomerName,

                CustomerPhone = sale.CustomerPhone,

                SaleDate = sale.SaleDate,

                PaymentMethod = sale.PaymentMethod,


                SubTotal = subTotal,

                Discount = sale.Discount,

                TaxRate = sale.TaxRate,

                TaxAmount = taxAmount,

                TotalAmount = sale.TotalAmount,


                Items = sale.SaleItems.Select(item => new InvoiceItemDto
                {
                    ProductName = item.Product.Name,

                    ProductSku = item.Product.Sku,

                    Quantity = item.Quantity,

                    PricePerUnit = item.PricePerUnit,

                    SubTotal = item.SubTotal

                }).ToList()
            };


            return invoice;
        }
    }
}