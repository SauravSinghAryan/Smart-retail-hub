using System.ComponentModel.DataAnnotations;

namespace hardwareAPI.DTOs.Product
{
    public class UpdateProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Sku { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int MinStockLevel { get; set; }

        public string Supplier { get; set; } = string.Empty;
    }
}