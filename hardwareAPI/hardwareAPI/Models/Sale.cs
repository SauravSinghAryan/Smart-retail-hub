using System.ComponentModel.DataAnnotations;

namespace hardwareAPI.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string SaleNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? CustomerPhone { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.UtcNow;

        public decimal TaxRate { get; set; }

        public decimal Discount { get; set; }

        [Required]
        [MaxLength(20)]
        public string PaymentMethod { get; set; } = "Cash";

        public decimal TotalAmount { get; set; }

        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}