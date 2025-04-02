using Microsoft.AspNetCore.Mvc;

namespace MyTestApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
