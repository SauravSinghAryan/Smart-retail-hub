using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace hardwareAPI.DTOs.Sale
{
    public class CreateSaleItemDto
    {
        [JsonPropertyName("product_id")]
        [Required]
        public int ProductId { get; set; }

        [JsonPropertyName("quantity")]
        [Required]
        public int Quantity { get; set; }

        [JsonPropertyName("price_per_unit")]
        [Required]
        public decimal PricePerUnit { get; set; }
    }
}