namespace hardwareAPI.DTOs.Billing
{
    public class InvoiceResponseDto
    {
        public string InvoiceNumber { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string CustomerPhone { get; set; } = string.Empty;

        public DateTime SaleDate { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public decimal SubTotal { get; set; }

        public decimal Discount { get; set; }

        public decimal TaxRate { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public List<InvoiceItemDto> Items { get; set; } = new();
    }
}