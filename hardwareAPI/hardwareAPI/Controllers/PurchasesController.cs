using hardwareAPI.DTOs.Purchase;
using hardwareAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hardwareAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchasesController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _purchaseService.GetAllAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePurchaseDto dto)
        {
            var result = await _purchaseService.CreatePurchaseAsync(dto);

            if (!result)
                return BadRequest();

            return Ok(new { message = "Purchase created successfully" });
        }
    }
}