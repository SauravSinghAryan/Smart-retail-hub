using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hardwareAPI.Models
{
    public class SaleItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SaleId { get; set; }

        [ForeignKey(nameof(SaleId))]
        public Sale? Sale { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerUnit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }
    }
}