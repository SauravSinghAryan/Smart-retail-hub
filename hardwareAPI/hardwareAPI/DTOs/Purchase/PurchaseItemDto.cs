using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace hardwareAPI.DTOs.Purchase
{
    public class PurchaseItemDto
    {
        [JsonPropertyName("product_id")]
        [Required]
        public int ProductId { get; set; }

        [JsonPropertyName("quantity")]
        [Required]
        public int Quantity { get; set; }

        [JsonPropertyName("cost_per_unit")]
        [Required]
        public decimal CostPerUnit { get; set; }
    }
}