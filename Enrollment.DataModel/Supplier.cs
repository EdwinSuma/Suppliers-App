using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(int.MaxValue)]
        public string CompanyName { get; set; }
        [Required]
        [MaxLength(int.MaxValue)]
        public string Address {  get; set; }
        [Required]
        [MaxLength(int.MaxValue)]
        public string Representative { get; set; }
        [Required]
        [MaxLength(int.MaxValue)]
        public string ContactNo { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
