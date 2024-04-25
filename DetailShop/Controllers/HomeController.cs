using DetailShop.App_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DetailShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace DetailShop.Controllers
{

    public class HomeController : Controller
    {

        private readonly ApplicationContext _context;


        public HomeController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
                
            }
            else
            {
                return RedirectToAction("Enter", "Authentication");
            }
        }

        public async Task<IActionResult> Components()
        {
            if (User.Identity.IsAuthenticated)
            {
                var components = await _context.Component.ToListAsync();
                return View(components);
            }
            else
            {
                return RedirectToAction("AccsessDenied", "Authentication");
            }
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

