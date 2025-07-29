using Microsoft.AspNetCore.Mvc;

namespace internshipProject1.WebUI.Controllers
{
    public class UserController : Controller
    {
        // User Dashboard - Kullanıcı ana sayfası
        public IActionResult Dashboard()
        {
            // Session kontrolü
            var sessionToken = HttpContext.Session.GetString("UserToken");
            var userRole = HttpContext.Session.GetString("userRole");
            var username = HttpContext.Session.GetString("username");
            
            // Giriş yapılmamışsa login sayfasına yönlendir
            if (string.IsNullOrEmpty(sessionToken))
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişmek için giriş yapmanız gerekiyor.";
                return RedirectToAction("Login", "Account");
            }
            
            // Admin kullanıcı ise admin paneline yönlendir
            if (userRole == "1")
            {
                return RedirectToAction("AdminMainPage", "AdminPanel");
            }
            
            ViewBag.Username = username;
            ViewBag.UserRole = userRole;
            
            return View();
        }

        // Kullanıcı profili
        public IActionResult Profile()
        {
            var sessionToken = HttpContext.Session.GetString("UserToken");
            var userRole = HttpContext.Session.GetString("userRole");
            var username = HttpContext.Session.GetString("username");
            
            if (string.IsNullOrEmpty(sessionToken))
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişmek için giriş yapmanız gerekiyor.";
                return RedirectToAction("Login", "Account");
            }
            
            if (userRole == "1")
            {
                return RedirectToAction("AdminMainPage", "AdminPanel");
            }
            
            ViewBag.Username = username;
            ViewBag.UserRole = userRole;
            
            return View();
        }

        // Kullanıcı ayarları
        public IActionResult Settings()
        {
            var sessionToken = HttpContext.Session.GetString("UserToken");
            var userRole = HttpContext.Session.GetString("userRole");
            var username = HttpContext.Session.GetString("username");
            
            if (string.IsNullOrEmpty(sessionToken))
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişmek için giriş yapmanız gerekiyor.";
                return RedirectToAction("Login", "Account");
            }
            
            if (userRole == "1")
            {
                return RedirectToAction("AdminMainPage", "AdminPanel");
            }
            
            ViewBag.Username = username;
            ViewBag.UserRole = userRole;
            
            return View();
        }

        // Kullanıcı kartı
        public IActionResult Card()
        {
            var sessionToken = HttpContext.Session.GetString("UserToken");
            var userRole = HttpContext.Session.GetString("userRole");
            var username = HttpContext.Session.GetString("username");
            
            if (string.IsNullOrEmpty(sessionToken))
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişmek için giriş yapmanız gerekiyor.";
                return RedirectToAction("Login", "Account");
            }
            
            if (userRole == "1")
            {
                return RedirectToAction("AdminMainPage", "AdminPanel");
            }
            
            ViewBag.Username = username;
            ViewBag.UserRole = userRole;
            
            return View();
        }

        // Araç takip
        public IActionResult VehicleTracking()
        {
            var sessionToken = HttpContext.Session.GetString("UserToken");
            var userRole = HttpContext.Session.GetString("userRole");
            var username = HttpContext.Session.GetString("username");
            
            if (string.IsNullOrEmpty(sessionToken))
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişmek için giriş yapmanız gerekiyor.";
                return RedirectToAction("Login", "Account");
            }
            
            if (userRole == "1")
            {
                return RedirectToAction("AdminMainPage", "AdminPanel");
            }
            
            ViewBag.Username = username;
            ViewBag.UserRole = userRole;
            
            return View();
        }
    }
} 