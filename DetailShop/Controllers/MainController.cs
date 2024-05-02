using DetailShop.App_Data;
using DetailShop.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace DetailShop.Controllers
{
    public class MainController : Controller
    {
        private readonly ApplicationContext _context;
        public MainController(ApplicationContext context)
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
        public IActionResult Index()
        {
            CheckRole();
            return View();
        }
        public async Task<IActionResult> Order()
        {
            CheckRole();
            if (User.Identity!.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var userOrders = await _context.Orders
                                               .Where(order => order.ID_Account == Convert.ToInt32(userId))
                                               .ToListAsync();

                if (userOrders != null && userOrders.Count > 0)
                {
                    return View(userOrders);
                }
                else
                {
                    return View("EmptyCart");
                }
            }
            return RedirectToAction("NotEnoughRights", "Error");
        }

        public async Task<IActionResult> Reviews()
        {
            CheckRole();
            if (User.Identity.IsAuthenticated)
            {
                var reviews = await _context.Reviews.ToListAsync();
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRole = await _context.Account
                  .Where(a => a.ID_Account.ToString() == userId)
                  .Select(a => a.ID_Role)
                  .FirstOrDefaultAsync();
                if (userRole != default && userRole == 3)
                {
                    var componentList = _context.Component.Select(c => new SelectListItem
                    {
                        Text = $"{c.Name}",
                        Value = c.ID_Component.ToString()
                    }).ToList();
                    componentList.Insert(0, new SelectListItem { Text = "Выберите комплектующее", Value = "" });
                    ViewBag.ComponentList = componentList;
                
                    return View("AddReviews",reviews);
                }
                else
                {
                    return View(reviews);
                }
            }
           return RedirectToAction("AccessDenied", "Authentication");

        }
        [HttpPost]
        public async Task<ActionResult> AddReviews(IFormCollection form)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _context.Account.FirstOrDefaultAsync(a => a.ID_Account == Convert.ToInt32(userId));
            var model = new Reviews
            {
                ID_Account = user.ID_Account,
                Comment = form["comment"],
                ID_Component = Convert.ToInt32(form["type"]),
                Rating = Convert.ToInt32(form["rating"])
            };

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Reviews.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Reviews", "Main");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Произошла ошибка при добавлении пользователя: " + ex.Message);
                }
            }

            return View(model);
        }
        public async Task<IActionResult> Users()
        {
            if (User.Identity!.IsAuthenticated)
            {
                CheckRole();
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRole = await _context.Account
                  .Where(a => a.ID_Account.ToString() == userId)
                  .Select(a => a.ID_Role)
                  .FirstOrDefaultAsync();


                if (userRole != default && userRole == 1)
                {
                    var users = await _context.Account.ToListAsync();
                    return View(users);
                }
            }
            return RedirectToAction("AdminError", "Authentication");
        }
        [HttpPost]
        public ActionResult Users(IFormCollection form)
        {
            var model = new Account
            {
                Login = form["login"],
                Password = EncryptPassword(form["password"]!),
                ID_Role = Convert.ToInt32(form["role"]),
                Last_Sign = DateOnly.FromDateTime(DateTime.Now)
            };

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Account.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Users", "Main");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Произошла ошибка при добавлении пользователя: " + ex.Message);
                }
            }

            return View(model);
        }

        private string EncryptPassword(string password)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha512.ComputeHash(inputBytes);
                string hash = BitConverter.ToString(hashBytes);

                return hash;
            }
        }
    }
}




