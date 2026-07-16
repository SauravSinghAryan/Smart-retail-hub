using System.Text.Json.Serialization;

namespace hardwareAPI.DTOs.Dashboard
{
    public class RecentPurchaseDto
    {
        [JsonPropertyName("purchase_number")]
        public string PurchaseNumber { get; set; } = string.Empty;

        [JsonPropertyName("supplier_name")]
        public string SupplierName { get; set; } = string.Empty;

        [JsonPropertyName("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("purchase_date")]
        public DateTime PurchaseDate { get; set; }
    }
}