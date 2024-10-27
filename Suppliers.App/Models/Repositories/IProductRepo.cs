using Inventory.DataModel;
using Repo.DataModel.Repository;

namespace Suppliers.App.Models.Repositories
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        Task<List<Product>> GetProductsByNameAsync(string name);
        Task<List<Product>> GetProductsAddedAfterAsync(DateTime date);
    }
}
