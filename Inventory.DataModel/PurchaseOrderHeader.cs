using System.ComponentModel.DataAnnotations;

namespace Inventory.DataModel
{
    public class PurchaseOrderHeader
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public string Status { get; set; } = "Pending Delivery";
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public DateTime? DateFinalized { get; set; }

        // Relationships
        public Supplier Supplier { get; set; }
        public List<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    }


}
