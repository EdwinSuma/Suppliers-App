using AutoMapper;
using Inventory.DataModel;
using Suppliers.App.Models;

namespace Suppliers.App.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Supplier, SupplierVM>().ReverseMap();
            CreateMap<Product, ProductVM>().ReverseMap();
        }
    }
}
