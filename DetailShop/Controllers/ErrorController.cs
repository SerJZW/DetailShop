using DetailShop.App_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DetailShop.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ApplicationContext _context;
        public ErrorController(ApplicationContext context)
        {
            _context = context;
        }
        public void CheckRole()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _context.Account.FirstOrDefault(u => u.ID_Account == Convert.ToInt32(userId));

            int userRole = default;
            if (user != null)
            {
                userRole = user.ID_Role;
            }

            ViewBag.UserRole = userRole;
        }
        [HttpGet]
        public IActionResult AccsessDenied()
        {
            CheckRole();
            return View();
        }
        [HttpGet]
        public IActionResult AdminError()
        {
            CheckRole();
            return View();
        }
        [HttpGet]
        public IActionResult NotFound()
        {
            CheckRole();
            return View();
        }
    }
}
