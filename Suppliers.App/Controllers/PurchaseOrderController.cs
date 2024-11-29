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
        // Fetch products and initialize ViewModel
        var products = _repository.GetProducts().ToList();
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

        ViewData["Products"] = products; // Pass the product list to the view
        return View(vm);
    }

    // Handle POST Request for Create
    [HttpPost]
    public IActionResult Create(PurchaseOrderHeaderVM vm)
    {
        if (!ModelState.IsValid)
        {
            HttpContext.Session.SetString("PurchaseOrder", JsonSerializer.Serialize(vm));

            var products = _repository.GetProducts().ToList();
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

            return View(vm);
        }

        var header = new PurchaseOrderHeader
        {
            SupplierId = vm.SupplierId,
            Status = "Pending Delivery",
            DateAdded = DateTime.Now
        };

        var details = vm.Details.Select(d => new PurchaseOrderDetail
        {
            ProductId = d.ProductId,
            Qty = d.Qty,
            Unit = d.Unit,
            Price = d.Price,
            Amount = d.Qty * d.Price
        }).ToList();

        _repository.AddPurchaseOrder(header, details);
        HttpContext.Session.Remove("PurchaseOrder");

        return RedirectToAction(nameof(Index));
    }




    // Render Partial View for Dynamic Rows
    public IActionResult RenderPartialView()
    {
        var detail = new PurchaseOrderDetailVM
        {
            Products = _repository.GetProducts().Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(),
                Text = p.Name
            }).ToList(),
            Qty = 1, // Default value
            Price = 0.00m, // Default value
            Amount = 0.00m // Default value
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
