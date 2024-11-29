using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        [Display(Name = "Product Description")]
        [MaxLength]
        public string Description { get; set; }

        [Display(Name = "Quantity")]
        [BindNever]
        public int Qty { get; set; } = 0;

        [MaxLength]
        [Display(Name = "Product Unit")]
        public string Unit { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateModified { get; set; }

        public ProductVM()
        {
            Qty = 0;
        }
    }

}
