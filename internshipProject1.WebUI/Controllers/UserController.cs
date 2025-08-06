using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace internshipProject1.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // User Dashboard - Kullanıcı ana sayfası
        public IActionResult Dashboard()
        {
            // Session kontrolü
            var sessionToken = HttpContext.Session.GetString("UserToken");
            var userRole = HttpContext.Session.GetString("userRole");
            var username = HttpContext.Session.GetString("username");
            var userId = HttpContext.Session.GetString("userId");
            
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
            ViewBag.UserId = userId;
            ViewBag.GatewayUrl = _configuration["ApiSettings:GatewayUrl"];
            ViewBag.ApiKey = _configuration["ApiSettings:ApiKey"];
            
            return View();
        }

        // Kullanıcı profili
        public IActionResult Profile()
        {
            var sessionToken = HttpContext.Session.GetString("UserToken");
            var userRole = HttpContext.Session.GetString("userRole");
            var username = HttpContext.Session.GetString("username");
            var userId = HttpContext.Session.GetString("userId");
            
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
            ViewBag.UserId = userId;
            ViewBag.GatewayUrl = _configuration["ApiSettings:GatewayUrl"];
            ViewBag.ApiKey = _configuration["ApiSettings:ApiKey"];
            
            return View();
        }

        // Kullanıcı ayarları
        public IActionResult Settings()
        {
            var sessionToken = HttpContext.Session.GetString("UserToken");
            var userRole = HttpContext.Session.GetString("userRole");
            var username = HttpContext.Session.GetString("username");
            var userId = HttpContext.Session.GetString("userId");
            
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
            ViewBag.UserId = userId;
            ViewBag.GatewayUrl = _configuration["ApiSettings:GatewayUrl"];
            ViewBag.ApiKey = _configuration["ApiSettings:ApiKey"];
            
            return View();
        }

        // Kullanıcı kartı
        public IActionResult Card()
        {
            var sessionToken = HttpContext.Session.GetString("UserToken");
            var userRole = HttpContext.Session.GetString("userRole");
            var username = HttpContext.Session.GetString("username");
            var userId = HttpContext.Session.GetString("userId");
            
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
            ViewBag.UserId = userId;
            
            return View();
        }

        // Araç takip
        public IActionResult VehicleTracking()
        {
            var sessionToken = HttpContext.Session.GetString("UserToken");
            var userRole = HttpContext.Session.GetString("userRole");
            var username = HttpContext.Session.GetString("username");
            var userId = HttpContext.Session.GetString("userId");
            
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
            ViewBag.UserId = userId;
            ViewBag.GatewayUrl = _configuration["ApiSettings:GatewayUrl"];
            ViewBag.ApiKey = _configuration["ApiSettings:ApiKey"];
            
            return View();
        }
    }
} 