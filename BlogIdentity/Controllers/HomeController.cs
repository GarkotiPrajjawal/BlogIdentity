using Microsoft.AspNetCore.Mvc;

namespace BlogIdentity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
