namespace hardwareAPI.DTOs.Billing
{
    public class InvoiceItemDto
    {
        public string ProductName { get; set; } = string.Empty;

        public string ProductSku { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal PricePerUnit { get; set; }

        public decimal SubTotal { get; set; }
    }
}