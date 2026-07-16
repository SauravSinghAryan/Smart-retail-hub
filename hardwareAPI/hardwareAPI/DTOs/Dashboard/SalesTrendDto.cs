using System.Text.Json.Serialization;

namespace hardwareAPI.DTOs.Dashboard
{
    public class SalesTrendDto
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
    }
}
