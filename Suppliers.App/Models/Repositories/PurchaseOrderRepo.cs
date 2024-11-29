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
            .Include(h => h.Supplier)
            .Include(h => h.PurchaseOrderDetails) // Include details for total calculation
            .ToList();
    }
    public decimal GetTotalAmount(int purchaseOrderHeaderId)
    {
        return _context.PurchaseOrderDetails
            .Where(d => d.PurchaseOrderHeaderId == purchaseOrderHeaderId)
            .Sum(d => d.Amount);
    }



    public void AddPurchaseOrder(PurchaseOrderHeader header, List<PurchaseOrderDetail> details)
    {
        foreach (var detail in details)
        {
            detail.Amount = detail.Qty * detail.Price; // Calculate Amount
        }

        _context.PurchaseOrderHeaders.Add(header);
        _context.PurchaseOrderDetails.AddRange(details);
        _context.SaveChanges(); // Save changes to the database
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
                product.Qty += detail.Qty;
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

