using DetailShop.App_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DetailShop.Controllers
{
    public class MainController : Controller
    {
        private readonly ApplicationContext _context;
        public MainController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
        public async Task<IActionResult> Reviews()
        {
            var reviews = await _context.Reviews.ToListAsync();
            return View(reviews);
        }
        public async Task<IActionResult> Users()
        {
            var users = await _context.User.ToListAsync();
            return View(users);
        }
    }
}
