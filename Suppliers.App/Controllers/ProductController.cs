using AutoMapper;
using Inventory.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suppliers.App.Models;
using Suppliers.App.Models.Repositories;

namespace Suppliers.App.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepo repo;
        private readonly IMapper mapper;
        public ProductController(IProductRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(mapper.Map<List<ProductVM>>(await repo.GetAllAsync()));
        }

        public IActionResult Add()
        {
            return View(new ProductVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductVM model)
        {
            model.DateAdded = DateTime.Now;
            if (ModelState.IsValid)
            {
                await repo.AddAsync(mapper.Map<Product>(model));

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await repo.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            
            Product existingProduct = await repo.GetAsync((int)id);
            if (existingProduct == null) return RedirectToAction("Index");

            
            ProductVM product = mapper.Map<ProductVM>(existingProduct);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductVM product)
        {
            var existingProduct = await repo.GetAsync(product.ProductID); 
            if (existingProduct != null)
            {
                
                existingProduct.Name = product.Name; 
                existingProduct.Description = product.Description;
                existingProduct.Unit = product.Unit;

                
                existingProduct.DateModified = DateTime.Now;

                await repo.UpdateAsync(existingProduct);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
