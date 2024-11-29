using Inventory.DataModel;
using Microsoft.EntityFrameworkCore;

public class PurchaseOrderRepo : IPurchaseOrderRepo
{
    private readonly AppDbContext _context;

    public PurchaseOrderRepo(AppDbContext context)
    {
        _context = context;
    }

    public List<PurchaseOrderHeader> GetAllHeaders()
    {
        return _context.PurchaseOrderHeaders
            .Include(po => po.Supplier)
            .Include(po => po.PurchaseOrderDetails)
            .ToList();
    }

    public void AddPurchaseOrder(PurchaseOrderHeader header, List<PurchaseOrderDetail> details)
    {
        // Set the Date property if it's required and not nullable
        header.DateAdded = DateTime.Now;

        // Add the header
        _context.PurchaseOrderHeaders.Add(header);

        // Add the details
        foreach (var detail in details)
        {
            detail.PurchaseOrderHeaderId = header.Id;
            _context.PurchaseOrderDetails.Add(detail);
        }

        // Save changes to the database
        _context.SaveChanges();
    }



    public void MarkAsDelivered(int id)
    {
        var order = _context.PurchaseOrderHeaders
            .Include(po => po.PurchaseOrderDetails)
            .FirstOrDefault(po => po.Id == id && po.Status == "Pending Delivery");

        if (order == null) return;

        order.Status = "Delivered";
        order.DateFinalized = DateTime.Now;

        foreach (var detail in order.PurchaseOrderDetails)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductID == detail.ProductId);
            if (product != null)
            {
                product.Qty += detail.Quantity;
            }
        }

        _context.SaveChanges();
    }

    public void VoidOrder(int id)
    {
        var order = _context.PurchaseOrderHeaders.FirstOrDefault(po => po.Id == id && po.Status != "Delivered");
        if (order == null) return;

        order.Status = "Void";
        _context.SaveChanges();
    }

    public List<Supplier> GetSuppliers()
    {
        return _context.Suppliers.ToList();
    }

    public List<Product> GetProducts()
    {
        return _context.Products.ToList();
    }


}

