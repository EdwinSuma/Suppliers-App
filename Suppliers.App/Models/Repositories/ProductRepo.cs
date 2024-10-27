using Inventory.DataModel;
using Repo.DataModel.Repository;

namespace Suppliers.App.Models.Repositories
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        public ProductRepo(AppDbContext context) : base(context)
        {
        }
        public Task<List<Product>> GetProductsAddedAfterAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProductsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
