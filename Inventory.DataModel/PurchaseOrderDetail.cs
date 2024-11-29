using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inventory.DataModel
{
    public class PurchaseOrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PurchaseOrderHeaderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Qty { get; set; }

        [MaxLength]
        public string Unit { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public Product Product { get; set; }
        public PurchaseOrderHeader PurchaseOrderHeader { get; set; }

    }
}
