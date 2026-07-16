using System.Text.Json.Serialization;

namespace hardwareAPI.DTOs.Dashboard
{
    public class RecentSaleDto
    {
        [JsonPropertyName("invoice_number")]
        public string InvoiceNumber { get; set; } = string.Empty;

        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; } = string.Empty;

        [JsonPropertyName("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("sale_date")]
        public DateTime SaleDate { get; set; }
    }
}