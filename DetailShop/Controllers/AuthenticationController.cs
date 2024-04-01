using DetailShop.App_Data;
using DetailShop.Models;
using DetailShop.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DetailShop.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ApplicationContext _context;

        public AuthenticationController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Enter(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Account.FirstOrDefault(u => u.Login == model.Login);
                if (user != null)
                {
                    var encpass = EncryptPassword(model.Password);
                    if (encpass == user.Password)
                    {
                        return RedirectToAction("Components", "Home");
                    }
                }
                ViewBag.ErrorMessage = "Неверный логин или пароль";
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

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(Account model)
        {
            
            if (ModelState.IsValid)
            {
                var existingAccount = _context.Account.FirstOrDefault(a => a.Login == model.Login);
                if (existingAccount != null)
                {
                    ViewBag.ErrorMessage = "Пользователь с таким логином уже существует.";
                    return View(model);
                }
                var account = new Account
                {
                    ID_Role = 1,
                    Last_Sign = DateOnly.FromDateTime(DateTime.UtcNow),
                    Login = model.Login,
                    Password = EncryptPassword(model.Password!)
                };

                _context.Account.Add(account);
                _context.SaveChanges();

                return RedirectToAction("Enter");
            }
            return View("Registration",model);
        }
    }
}
