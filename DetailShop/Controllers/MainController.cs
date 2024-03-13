using Microsoft.AspNetCore.Mvc;

namespace DetailShop.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
