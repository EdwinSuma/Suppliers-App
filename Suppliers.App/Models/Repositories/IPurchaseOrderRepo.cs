using Inventory.DataModel;

public interface IPurchaseOrderRepo
{
    List<PurchaseOrderHeader> GetAllHeaders();
    void AddPurchaseOrder(PurchaseOrderHeader header, List<PurchaseOrderDetail> details);
    void MarkAsDelivered(int id);
    void VoidOrder(int id);
    List<Supplier> GetSuppliers();
    List<Product> GetProducts();
}

