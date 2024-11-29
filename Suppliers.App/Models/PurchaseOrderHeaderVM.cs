using Inventory.DataModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class PurchaseOrderHeaderVM
{
    [Required(ErrorMessage = "Supplier is required.")]
    public int SupplierId { get; set; }

    public string Status { get; set; } = "Pending Delivery";

    [Required(ErrorMessage = "At least one product must be added.")]
    public List<PurchaseOrderDetailVM> Details { get; set; } = new List<PurchaseOrderDetailVM>();

    public List<SelectListItem> Suppliers { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> Products { get; set; } = new List<SelectListItem>();
}



