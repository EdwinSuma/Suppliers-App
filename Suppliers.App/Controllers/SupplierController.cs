using Inventory.DataModel;
using Microsoft.AspNetCore.Mvc;


namespace Suppliers.App.Controllers
{
    public class SupplierController : Controller
    {
        private readonly AppDbContext context;
        public SupplierController(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View(context.Suppliers.OrderBy(o => o.CompanyName).ToList());
        }

        public IActionResult Add()
        {
            return View(new Inventory.DataModel.Supplier());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Inventory.DataModel.Supplier supplier)
        {
            supplier.DateAdded = DateTime.Now;
            //context.Add(supplier);
            await context.Set<Inventory.DataModel.Supplier>().AddAsync(supplier);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await context.Suppliers.FindAsync(id);
            context.Set<Inventory.DataModel.Supplier>().Remove(supplier);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            var supplier = await context.Suppliers.FindAsync(id);

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(Inventory.DataModel.Supplier supplier)
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

                context.Set<Inventory.DataModel.Supplier>().Update(existingSupplier);
                await context.SaveChangesAsync();

            }
            return RedirectToAction("Index");
        }
    }
}
