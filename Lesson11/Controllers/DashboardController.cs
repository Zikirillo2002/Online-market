using Lesson11.Data;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DiyorMarketDbContext _context;

        public DashboardController(DiyorMarketDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.SummaryCount = _context.Products.Count();
            ViewBag.SalesCount = _context.Sales.Count();
            ViewBag.SuppliesCount = _context.Supplies.Count();

            var salesByCategory = from category in _context.Categories
                                  join product in _context.Products on category.Id equals product.CategoryId
                                  join saleItem in _context.SaleItems on product.Id equals saleItem.ProductId
                                  group saleItem by category.Name into groupedCategories
                                  select new
                                  {
                                      CategoryName = groupedCategories.Key,
                                      SalesCount = groupedCategories.Count()
                                  };

            ViewBag.SalesByCategory = salesByCategory.ToList();

            return View();
        }
    }
}
