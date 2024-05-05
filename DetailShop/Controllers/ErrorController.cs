using Microsoft.AspNetCore.Mvc;

namespace DetailShop.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult AccsessDenied()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AdminError()
        {
            return View();
        }
    }
}
