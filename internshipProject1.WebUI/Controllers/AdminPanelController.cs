using Microsoft.AspNetCore.Mvc;

namespace internshipProject1.WebUI.Controllers
{
    public class AdminPanelController : Controller
    {
        // Admin Panel erişimi için authentication kontrolü
        private bool IsUserAuthenticated()
        {
            // Session kontrolü
            var sessionToken = HttpContext.Session.GetString("UserToken");
            
            // Cookie kontrolü
            var cookieToken = Request.Cookies["UserToken"];
            
            // Debug için log ekle
            Console.WriteLine($"DEBUG - Session Token: {sessionToken}");
            Console.WriteLine($"DEBUG - Cookie Token: {cookieToken}");
            Console.WriteLine($"DEBUG - Is Authenticated: {!string.IsNullOrEmpty(sessionToken) || !string.IsNullOrEmpty(cookieToken)}");
            
            return !string.IsNullOrEmpty(sessionToken) || !string.IsNullOrEmpty(cookieToken);
        }

        public IActionResult AdminMainPage()
        {
            if (!IsUserAuthenticated())
            {
                TempData["ErrorMessage"] = "Admin Panel'e erişmek için giriş yapmanız gerekiyor.";
                return RedirectToAction("Login", "Account");
            }
            
            return View();
        }

        public IActionResult Routes()
        {
            if (!IsUserAuthenticated())
            {
                TempData["ErrorMessage"] = "Admin Panel'e erişmek için giriş yapmanız gerekiyor.";
                return RedirectToAction("Login", "Account");
            }
            
            return View();
        }

        public IActionResult Stops()
        {
            if (!IsUserAuthenticated())
            {
                TempData["ErrorMessage"] = "Admin Panel'e erişmek için giriş yapmanız gerekiyor.";
                return RedirectToAction("Login", "Account");
            }
            
            return View();
        }

        public IActionResult Trips()
        {
            if (!IsUserAuthenticated())
            {
                TempData["ErrorMessage"] = "Admin Panel'e erişmek için giriş yapmanız gerekiyor.";
                return RedirectToAction("Login", "Account");
            }
            
            return View();
        }

        public IActionResult RouteStops()
        {
            if (!IsUserAuthenticated())
            {
                TempData["ErrorMessage"] = "Admin Panel'e erişmek için giriş yapmanız gerekiyor.";
                return RedirectToAction("Login", "Account");
            }
            
            return View();
        }
    }
}
