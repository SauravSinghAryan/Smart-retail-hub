using System.Text.Json.Serialization;

namespace hardwareAPI.DTOs.Purchase
{
    public class CreatePurchaseDto
    {
        [JsonPropertyName("supplier_name")]
        public string SupplierName { get; set; } = string.Empty;

        [JsonPropertyName("items")]
        public List<PurchaseItemDto> Items { get; set; } = new();
    }
}