using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class PurchaseOrderDetailVM
{
    [Required(ErrorMessage = "Please select a product.")]
    public int ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int Qty { get; set; }

    public string Unit { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }
    public decimal Amount { get; set; }

    // Dropdown list for products
    public List<SelectListItem> Products { get; set; } = new List<SelectListItem>();
}
