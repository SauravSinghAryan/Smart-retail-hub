using hardwareAPI.DTOs.Product;
using hardwareAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hardwareAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(
            string? category,
            string? search,
            bool lowStock = false)
        {
            var result = await _service.GetAllAsync(category, search, lowStock);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);

            if (result == null)
                return NotFound(new { error = "Product not found." });

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { error = "Product not found." });

            return Ok(new
            {
                message = "Product deleted successfully."
            });
        }
    }
}