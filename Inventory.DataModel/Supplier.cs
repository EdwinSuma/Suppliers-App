using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataModel
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [Required]
        [MaxLength(30)]
        public string CompanyName { get; set; }
        [Required]
        [MaxLength(30)]
        public string Address {  get; set; }
        [Required]
        [MaxLength(30)]
        public string Representative { get; set; }
        [Required]
        [MaxLength(30)]
        public string ContactNo { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateModified { get; set; }

        public List<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
    }
}
