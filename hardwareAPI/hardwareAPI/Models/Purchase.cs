using System.ComponentModel.DataAnnotations;

namespace hardwareAPI.Models
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string PurchaseNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string SupplierName { get; set; } = string.Empty;

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        public ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
    }
}