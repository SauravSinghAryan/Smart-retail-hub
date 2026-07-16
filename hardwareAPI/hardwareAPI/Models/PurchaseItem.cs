using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hardwareAPI.Models
{
    public class PurchaseItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PurchaseId { get; set; }

        [ForeignKey(nameof(PurchaseId))]
        public Purchase? Purchase { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPerUnit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }
    }
}