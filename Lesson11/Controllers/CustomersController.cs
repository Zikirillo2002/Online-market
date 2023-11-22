using Lesson11.Data;
using Lesson11.Models;
using Lesson11.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lesson11.Controllers
{
    public class CustomersController : Controller
    {
        private readonly DiyorMarketDbContext _context;
        private readonly CustomerService _customersService;

        public CustomersController(DiyorMarketDbContext context)
        {
            _context = context;
            _customersService = new CustomerService();
        }

        public IActionResult Index()
        {
            var customers = _context.Customers.ToList();
            
            ViewBag.Customers = customers;

            return View();
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer newCustomer)
        {
            try
            {
                _customersService.Create(newCustomer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var customer = _customersService.FindById(id);
            if (customer is null)
            {
                return View("Error! Customer not found");
            }
            return View(customer);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer customerToUpdate)
        {
            try
            {
                _customersService.Update(customerToUpdate);
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

            var customer = _customersService.FindById(id);
            if (customer is null)
            {
                return View("Error! Customer not found");
            }
            return View(customer);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Customer customer)
        {
            try
            {
                _customersService.Delete(id);
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
