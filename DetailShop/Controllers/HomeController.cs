using DetailShop.App_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DetailShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.ComponentModel;
using DetailShop.Models.DbModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DetailShop.Controllers
{

    public class HomeController : Controller
    {

        private readonly ApplicationContext _context;


        public HomeController(ApplicationContext context)
        {
            _context = context;
        }
        public void CheckRole()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _context.Account.FirstOrDefault(u => u.ID_Account == Convert.ToInt32(userId));

            int? userRole = null;
            if (user != null)
            {
                userRole = user.ID_Role;
            }

            ViewBag.UserRole = userRole;
        }

        [HttpGet]
        public IActionResult Index()
        {
            CheckRole();
            if (User.Identity!.IsAuthenticated)
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
            CheckRole();
            if (User.Identity!.IsAuthenticated)
            {

                var components = await _context.Component.ToListAsync();
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRole = await _context.Account
                  .Where(a => a.ID_Account.ToString() == userId)
                  .Select(a => a.ID_Role)
                  .FirstOrDefaultAsync();
                if (userRole != null && userRole == 3)
                {
                    return View(components);
                }
                else if (userRole != null && userRole == 1 || userRole == 2)
                {
                    var TypeList = _context.Type_Component.Select(c => new SelectListItem
                    {
                        Text = $"{c.Title}",
                        Value = c.ID_Type.ToString()
                    }).ToList();
                    TypeList.Insert(0, new SelectListItem { Text = "Тип продукта", Value = "" });
                    ViewBag.TypeList = TypeList;
                    var ManifList = _context.Provider.Select(c => new SelectListItem
                    {
                        Text = $"{c.Title}",
                        Value = c.ID_Provider.ToString()
                    }).ToList();
                    ManifList.Insert(0, new SelectListItem { Text = "Производитель", Value = "" });
                    ViewBag.ManifList = ManifList;
                    var prodlistt = _context.Component.Select(c => new SelectListItem
                    {
                        Text = $"{c.Name}",
                        Value = c.ID_Component.ToString()
                    }).ToList();
                    prodlistt.Insert(0, new SelectListItem { Text = "Выбирите комлектующее", Value = "" });
                    ViewBag.DeleteCompList = prodlistt;
                    return View("AddComponents", components);
                }
            }
            return RedirectToAction("AccsessDenied", "Error");
        }
        public async Task FindOrder()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userOrders = await _context.Orders
                                          .Where(order => order.ID_Account == Convert.ToInt32(userId))
                                          .ToListAsync();
            ViewBag.UserOrders = userOrders.Count;
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productID, double unitPrice)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _context.Account.FirstOrDefaultAsync(a => a.ID_Account == Convert.ToInt32(userId));

            var Order = new Orders
            {
                ID_Account = user.ID_Account,              
                Result = Convert.ToDecimal(unitPrice),
            };
            try
            {
                _context.Orders.Add(Order);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ïðîèçîøëà îøèáêà ïðè äîáàâëåíèè ïîëüçîâàòåëÿ: " + ex.Message);
            }
            var model = new Order_Item
            {
                ID_Component = productID,
                Quanity = 1,
                ID_Order = Order.ID_Order,
            };           
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Order_Item.Add(model);
                    _context.SaveChanges();
                    ViewBag.UserId = userId;
                    await FindOrder();
                    return RedirectToAction("Order", "Main");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ïðîèçîøëà îøèáêà ïðè äîáàâëåíèè ïîëüçîâàòåëÿ: " + ex.Message);
                }
            }
            return RedirectToAction("AunthError", "Error");
        }
        [HttpPost]
        public ActionResult AddComponents(IFormCollection form)
        {
            CheckRole();
            var model = new Components
            {
                Name = form["Title"],
                Description = form["description"],
                Cost = Convert.ToDecimal(form["price"]),
                ID_Provider = Convert.ToInt32(form["providers"]),
                ID_Type = Convert.ToInt32(form["type"]),
                Count = Convert.ToInt32(form["count"]),
                Specifications = form["spec"]
            };

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Component.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Components", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Произошла ошибка при добавлении комплектующих: " + ex.Message);
                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteComponents(int idprod)
        {
            var prod = await _context.Component.FindAsync(idprod);
            if (prod == null)
            {
                return View("AunthError");
            }

            try
            {
                _context.Component.Remove(prod);
                await _context.SaveChangesAsync();
                return RedirectToAction("Components", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ïðîèçîøëà îøèáêà ïðè óäàëåíèè ïîëüçîâàòåëÿ: " + ex.Message);
                return RedirectToAction("AunthError", "Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditProducts(Components prod)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingProd = await _context.Component.FindAsync(prod.ID_Component);

                    if (existingProd != null)
                    {
                        existingProd.Name = prod.Name;
                        existingProd.ID_Type = prod.ID_Type;
                        existingProd.ID_Provider = prod.ID_Provider;
                        existingProd.Description = prod.Description;
                        existingProd.Cost = prod.Cost;
                        existingProd.Specifications = prod.Specifications;
                        existingProd.Count = prod.Count;

                        _context.Entry(existingProd).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Components", "Home");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ïðîèçîøëà îøèáêà ïðè ðåäàêòèðîâàíèè ïîëüçîâàòåëÿ: " + ex.Message);
                    return RedirectToAction("AunthError", "Error");
                }
            }
            return View(prod);
        }
        public async Task<IActionResult> Discount()
        {
            CheckRole();
            var discounts = await _context.Discount.ToListAsync();
            return View(discounts);
        }
        public async Task<IActionResult> Providers()
        {
            CheckRole();
            if (User.Identity!.IsAuthenticated)
            {
                var providers = await _context.Provider.ToListAsync();
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRole = await _context.Account
                  .Where(a => a.ID_Account.ToString() == userId)
                  .Select(a => a.ID_Role)
                  .FirstOrDefaultAsync();
                var manuflist = _context.Provider.Select(c => new SelectListItem
                {
                    Text = $"{c.Title}",
                    Value = c.ID_Provider.ToString()
                }).ToList();
                manuflist.Insert(0, new SelectListItem { Text = "Производитель", Value = "" });
                ViewBag.DeleteManufList = manuflist;
                if (userRole != null && userRole == 3)
                {
                    return View(providers);
                }
                else if (userRole != null && userRole == 1 || userRole == 2)
                {
                    return View("AddProviders", providers);
                }
            }
            return RedirectToAction("AccsessDenied", "Error");
        }

        [HttpPost]
        public ActionResult AddProviders(IFormCollection form)
        {
            CheckRole();
            var model = new Provider
            {
                Title = form["Title"],
                Information = form["info"],
                ID_Type = Convert.ToInt32(form["type"]),
            };

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Provider.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Providers", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Произошла ошибка при добавлении комплектующих: " + ex.Message);
                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProviders(int idmanuf)
        {
            var manuf = await _context.Provider.FindAsync(idmanuf);
            if (manuf == null)
            {
                return View("AccsessDenied");
            }

            try
            {
                _context.Provider.Remove(manuf);
                await _context.SaveChangesAsync();
                return RedirectToAction("Providers", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ïðîèçîøëà îøèáêà ïðè óäàëåíèè ïîëüçîâàòåëÿ: " + ex.Message);
                return RedirectToAction("AccsessDenied", "Error");
            }
        }
    

    }

}



