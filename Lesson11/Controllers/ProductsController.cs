using Lesson11.Data;
using Lesson11.Models;
using Lesson11.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lesson11.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DiyorMarketDbContext _context;
        private readonly ProductsService _productsService;

        public ProductsController(DiyorMarketDbContext context)
        {
            _context = context;
            _productsService = new ProductsService();
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            var categories = _context.Categories.ToList();

            ViewBag.Categories = categories.Select(category => 
            new SelectListItem
            {
                Text = category.Name,
                Value = category.Id.ToString(),
            }).ToList();

            ViewBag.Products = products;

            return View();
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product newProduct)
        {
            try
            {
                _productsService.Create(newProduct);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var product = _productsService.FindById(id);
            if (product is null)
            {
                return View("Error! Product not found");
            }
            return View(product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product productToUpdate)
        {
            try
            {
                _productsService.Update(productToUpdate);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {

            var product = _productsService.FindById(id);
            if (product is null)
            {
                return View("Error! Product not found");
            }
            return View(product);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                _productsService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
