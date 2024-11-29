using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inventory.DataModel
{
    public class PurchaseOrderDetail
    {
        public int Id { get; set; }
        public int PurchaseOrderHeaderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }

        // Relationships
        public Product Product { get; set; }
        public PurchaseOrderHeader PurchaseOrderHeader { get; set; }
    }


}
