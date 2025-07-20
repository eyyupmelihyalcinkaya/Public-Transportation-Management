using Microsoft.AspNetCore.Mvc;

namespace internshipProject1.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
