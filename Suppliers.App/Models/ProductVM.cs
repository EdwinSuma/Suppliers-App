using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace Suppliers.App.Models
{
    public class ProductVM
    {
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [MinLength(2)]
        [MaxLength]
        public string Name { get; set; }

        [Required]
        [Display(Description = "Product Description")]
        [MaxLength]
        public string Description { get; set; }

        public int Qty { get; set; }

        [MaxLength]
        public string Unit { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
