using System.Text.Json.Serialization;

namespace hardwareAPI.DTOs.Purchase
{
    public class PurchaseResponseDto
    {
        [JsonPropertyName("purchase_number")]
        public string PurchaseNumber { get; set; } = string.Empty;

        [JsonPropertyName("supplier_name")]
        public string SupplierName { get; set; } = string.Empty;

        [JsonPropertyName("purchase_date")]
        public DateTime PurchaseDate { get; set; }

        [JsonPropertyName("total_amount")]
        public decimal TotalAmount { get; set; }
    }
}