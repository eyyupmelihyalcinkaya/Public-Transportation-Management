using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace internshipProject1.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public UserController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
        }

        // API Proxy helper method
        private async Task<IActionResult> ProxyApiRequest(string endpoint, HttpMethod? method = null, object? data = null)
        {
            try
            {
                method = method ?? HttpMethod.Get;
                var gatewayUrl = _configuration["ApiSettings:GatewayUrl"];
                var apiKey = _configuration["ApiSettings:ApiKey"];
                
                var request = new HttpRequestMessage(method, $"{gatewayUrl}{endpoint}");
                request.Headers.Add("X-Api-Key", apiKey);
                
                if (data != null && (method == HttpMethod.Post || method == HttpMethod.Put))
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(data);
                    request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                }

                var response = await _httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return Content(content, "application/json");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, content);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
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

        // Public API Endpoints for User Pages (No Admin Authentication Required)
        
        [HttpGet]
        public async Task<IActionResult> GetPublicRoutes(int page = 1, int pageSize = 100)
        {
            // Public endpoint - no authentication required for viewing routes
            return await ProxyApiRequest($"/api/routes?page={page}&pageSize={pageSize}");
        }

        [HttpGet]
        public async Task<IActionResult> GetPublicRouteStops(int routeId, int page = 1, int pageSize = 100)
        {
            // Public endpoint - no authentication required for viewing route stops
            return await ProxyApiRequest($"/api/routes/{routeId}/stops?page={page}&pageSize={pageSize}");
        }

        [HttpGet]
        public async Task<IActionResult> GetPublicStops(int page = 1, int pageSize = 100)
        {
            // Public endpoint - no authentication required for viewing stops
            return await ProxyApiRequest($"/api/stops?page={page}&pageSize={pageSize}");
        }
    }
} 