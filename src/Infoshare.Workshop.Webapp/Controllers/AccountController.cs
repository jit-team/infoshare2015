using Microsoft.AspNet.Mvc;
using Infoshare.Workshop.Webapp.Models;

namespace Infoshare.Workshop.Webapp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginForm loginForm)
        {
            return View(loginForm);
        }
    }
}
