using AutoMapper;
using Inventory.DataModel;
using Microsoft.AspNetCore.Mvc;
using Suppliers.App.Models;


namespace Suppliers.App.Controllers
{
    public class SupplierController : Controller
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        public SupplierController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            List<SupplierVM> list = mapper.Map<List<SupplierVM>>(context.Suppliers.ToList());
            return View(list);
        }

        public IActionResult Add()
        {
            return View(new SupplierVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SupplierVM model)
        {
            model.DateAdded = DateTime.Now;
            //context.Add(supplier);
            Supplier entity = mapper.Map<Supplier>(model);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await context.Suppliers.FindAsync(id);
            context.Set<Supplier>().Remove(supplier);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            SupplierVM supplier = mapper.Map<SupplierVM>(await context.Suppliers.FindAsync(id));

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(SupplierVM supplier)
        {
            var existingSupplier = await context.Suppliers.FindAsync(supplier.SupplierID);

            if (existingSupplier != null)
            {
                // Update only the modifiable fields, excluding DateAdded
                existingSupplier.CompanyName = supplier.CompanyName;
                existingSupplier.Address = supplier.Address;
                existingSupplier.Representative = supplier.Representative;
                existingSupplier.ContactNo = supplier.ContactNo;

                // Update DateModified but keep DateAdded intact
                existingSupplier.DateModified = DateTime.Now;

                context.Set<Supplier>().Update(mapper.Map<Supplier>(existingSupplier));
                await context.SaveChangesAsync();

            }
            return RedirectToAction("Index");
        }
    }
}
