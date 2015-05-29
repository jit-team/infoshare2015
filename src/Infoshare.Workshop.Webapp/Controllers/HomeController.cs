using Microsoft.AspNet.Mvc;

namespace Infoshare.Workshop.Webapp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
} 
