using DetailShop.App_Data;
using DetailShop.Models;
using DetailShop.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DetailShop.Controllers
{
    public class MainController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly Account account;
        public MainController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Order()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRole = await _context.Account
                  .Where(a => a.ID_Account.ToString() == userId)
                  .Select(a => a.ID_Role)
                  .FirstOrDefaultAsync();
                if (userRole != null && userRole.Value != 1)
                {
                    return View();
                }
            }
                return RedirectToAction("AccessDenied", "Authentication");
        }
        public async Task<IActionResult> Reviews()
        {
            var reviews = await _context.Reviews.ToListAsync();
            return View(reviews);
        }
        public async Task<IActionResult> Users()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRole = await _context.Account
                  .Where(a => a.ID_Account.ToString() == userId)
                  .Select(a => a.ID_Role)
                  .FirstOrDefaultAsync();


                if (userRole != null && userRole.Value == 1)
                {
                    var users = await _context.Account.ToListAsync();

                    return View(users);

                }
            }
            return RedirectToAction("AdminError", "Authentication");
        }

    }
    //[HttpPost]
    //public ActionResult AddUser(UserViewModel model)
    //{
        
    //}
}


