namespace hardwareAPI.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Sku { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public decimal Cost { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int MinStockLevel { get; set; }

        public string Supplier { get; set; } = string.Empty;
    }
}