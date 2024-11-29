using Inventory.DataModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class PurchaseOrderController : Controller
{
    private readonly IPurchaseOrderRepo _repository;

    public PurchaseOrderController(IPurchaseOrderRepo repository)
    {
        _repository = repository;
    }

    // List all purchase orders
    public IActionResult Index()
    {
        var headers = _repository.GetAllHeaders();
        return View(headers);
    }

    // Render Create View
    public IActionResult Create()
    {
        // Get all products with their Unit values
        var products = _repository.GetProducts().ToList();
        ViewData["Products"] = products; // Pass the product list to the view

        // Initialize the ViewModel with Suppliers and Products
        var vm = new PurchaseOrderHeaderVM
        {
            Suppliers = _repository.GetSuppliers().Select(s => new SelectListItem
            {
                Value = s.SupplierID.ToString(),
                Text = s.CompanyName
            }).ToList(),

            Products = products.Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(),
                Text = p.Name
            }).ToList()
        };

        return View(vm);
    }

    // Handle POST Request for Create
    [HttpPost]
    public IActionResult Create(PurchaseOrderHeaderVM vm)
    {
        if (!ModelState.IsValid)
        {
            // Serialize the ViewModel and store it in the session for persistence
            HttpContext.Session.SetString("PurchaseOrder", JsonSerializer.Serialize(vm));

            // Re-populate Suppliers and Products for re-rendering the form
            var products = _repository.GetProducts().ToList();
            ViewData["Products"] = products;

            vm.Suppliers = _repository.GetSuppliers().Select(s => new SelectListItem
            {
                Value = s.SupplierID.ToString(),
                Text = s.CompanyName
            }).ToList();

            vm.Products = products.Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(),
                Text = p.Name
            }).ToList();

            return View(vm); // Return the form with validation errors
        }

        // Create PurchaseOrderHeader entity
        var header = new PurchaseOrderHeader
        {
            SupplierId = vm.SupplierId,
            Status = vm.Status,
            DateAdded = DateTime.Now
        };

        // Map PurchaseOrderDetail entities from ViewModel
        var details = vm.Details.Select(d => new PurchaseOrderDetail
        {
            ProductId = d.ProductId,
            Quantity = d.Quantity,
            Unit = d.Unit,
            Price = d.Price
        }).ToList();

        // Add the purchase order and details to the database
        _repository.AddPurchaseOrder(header, details);

        // Clear session data after successful submission
        HttpContext.Session.Remove("PurchaseOrder");

        return RedirectToAction(nameof(Index));
    }

    // Render Partial View for Dynamic Rows
    public IActionResult RenderPartialView()
    {
        // Get all products with their Unit values
        var products = _repository.GetProducts().ToList();
        ViewData["Products"] = products; // Pass the product list to the view

        // Initialize a new PurchaseOrderDetailVM with Products for the dropdown
        var detail = new PurchaseOrderDetailVM
        {
            Products = products.Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(),
                Text = p.Name
            }).ToList()
        };

        return PartialView("_PurchaseOrderDetailRow", detail);
    }

    // Mark Purchase Order as Delivered
    public IActionResult MarkAsDelivered(int id)
    {
        _repository.MarkAsDelivered(id);
        return RedirectToAction(nameof(Index));
    }

    // Void a Pending Purchase Order
    public IActionResult Void(int id)
    {
        _repository.VoidOrder(id);
        return RedirectToAction(nameof(Index));
    }
}
