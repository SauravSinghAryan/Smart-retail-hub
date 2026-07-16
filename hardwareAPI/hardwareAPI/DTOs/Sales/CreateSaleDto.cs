using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace hardwareAPI.DTOs.Sale
{
    public class CreateSaleDto
    {
        [JsonPropertyName("customer_name")]
        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [JsonPropertyName("customer_phone")]
        public string? CustomerPhone { get; set; }

        [JsonPropertyName("items")]
        [Required]
        public List<CreateSaleItemDto> Items { get; set; } = new();

        [JsonPropertyName("tax_rate")]
        public decimal TaxRate { get; set; }

        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }

        [JsonPropertyName("payment_method")]
        public string PaymentMethod { get; set; } = "Cash";
    }
}