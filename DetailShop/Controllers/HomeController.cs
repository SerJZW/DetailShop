using DetailShop.App_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DetailShop.Controllers
{
 
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Components()
        {
            var components = await _context.Component.ToListAsync();
            return View(components);
        }
        public async Task<IActionResult> Discount()
        {
            var discounts = await _context.Discount.ToListAsync();
            return View(discounts);
        }
        public async Task<IActionResult> Providers()
        {
            var providers = await _context.Provider.ToListAsync();
            return View(providers);
        }
    }
}
