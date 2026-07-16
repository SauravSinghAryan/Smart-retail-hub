using hardwareAPI.DTOs.Sale;
using hardwareAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hardwareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        // GET: api/sales
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sales = await _saleService.GetAllAsync();
            return Ok(sales);
        }

        // POST: api/sales
        [HttpPost]
        public async Task<IActionResult> Create(CreateSaleDto dto)
        {
            var sale = await _saleService.CreateSaleAsync(dto);

            return Ok(new
            {
                saleId = sale.Id,
                saleNumber = sale.SaleNumber,
                customerName = sale.CustomerName,
                totalAmount = sale.TotalAmount,
                message = "Sale completed successfully."
            });
        }
    }
}