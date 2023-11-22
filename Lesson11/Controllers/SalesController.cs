using Lesson11.Data;
using Lesson11.Models;
using Lesson11.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class SalesController : Controller
    {
        private readonly DiyorMarketDbContext _context;
        private readonly SalesService _salesService;

        public SalesController(DiyorMarketDbContext context)
        {
            _context = context;
            _salesService = new SalesService();
        }

        public IActionResult Index()
        {
            var sales = _context.Sales.ToList();

            ViewBag.Sales = sales;

            return View();
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sale newSale)
        {
            try
            {
                _salesService.Create(newSale);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var sale = _salesService.FindById(id);
            if (sale is null)
            {
                return View("Error! Sale not found");
            }
            return View(sale);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Sale saleToUpdate)
        {
            try
            {
                _salesService.Update(saleToUpdate);
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

            var sale = _salesService.FindById(id);
            if (sale is null)
            {
                return View("Error! Sale not found");
            }
            return View(sale);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Sale sale)
        {
            try
            {
                _salesService.Delete(id);
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
