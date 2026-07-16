using System.Text.Json.Serialization;

namespace hardwareAPI.DTOs.Sale
{
    public class SaleResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("sale_number")]
        public string SaleNumber { get; set; } = string.Empty;

        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; } = string.Empty;

        [JsonPropertyName("sale_date")]
        public DateTime SaleDate { get; set; }

        [JsonPropertyName("total_amount")]
        public decimal TotalAmount { get; set; }
    }
}