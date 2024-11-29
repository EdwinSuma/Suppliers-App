using System.ComponentModel.DataAnnotations;

namespace Inventory.DataModel
{
    public class PurchaseOrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [MaxLength]
        public string Status { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateFinalized { get; set; }

        public Supplier Supplier { get; set; }
        public List<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    }
}
