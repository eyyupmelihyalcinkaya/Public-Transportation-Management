using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace internshipProject1.WebUI.Controllers
{

    public class AdminPanelController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AdminPanelController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
        }

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

        // Admin rolü kontrolü
        private bool IsUserAdmin()
        {
            var userRole = HttpContext.Session.GetString("userRole");
            Console.WriteLine($"DEBUG - User Role from Session: {userRole}");
            
            // Role = "1" ise Admin
            var isAdmin = userRole == "1";
            Console.WriteLine($"DEBUG - Is User Admin: {isAdmin}");
            
            return isAdmin;
        }

        // API Proxy metodları - API key'i server-side'da gizler
        private async Task<IActionResult> ProxyApiRequest(string endpoint, HttpMethod method = null, object body = null)
        {
            try
            {
                var gatewayUrl = _configuration["ApiSettings:GatewayUrl"];
                var apiKey = _configuration["ApiSettings:ApiKey"];
                
                // Null kontrolü
                if (string.IsNullOrEmpty(gatewayUrl))
                {
                    Console.WriteLine("ERROR - Gateway URL is null or empty");
                    return StatusCode(500, new { error = "Gateway URL configuration is missing" });
                }
                
                if (string.IsNullOrEmpty(apiKey))
                {
                    Console.WriteLine("ERROR - API Key is null or empty");
                    return StatusCode(500, new { error = "API Key configuration is missing" });
                }
                
                Console.WriteLine($"DEBUG - Gateway URL: {gatewayUrl}");
                Console.WriteLine($"DEBUG - API Key: {apiKey.Substring(0, Math.Min(10, apiKey.Length))}...");
                Console.WriteLine($"DEBUG - Endpoint: {endpoint}");
                Console.WriteLine($"DEBUG - Method: {method ?? HttpMethod.Get}");
                
                var fullUrl = $"{gatewayUrl}{endpoint}";
                Console.WriteLine($"DEBUG - Full URL: {fullUrl}");
                
                var request = new HttpRequestMessage(method ?? HttpMethod.Get, fullUrl);
                request.Headers.Add("X-Api-Key", apiKey);

                if (body != null)
                {
                    var jsonBody = JsonSerializer.Serialize(body);
                    request.Content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");
                    Console.WriteLine($"DEBUG - Request Body: {jsonBody}");
                }

                Console.WriteLine($"DEBUG - Headers: {string.Join(", ", request.Headers.Select(h => $"{h.Key}={string.Join(",", h.Value)}"))}");

                var response = await _httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"DEBUG - Response Status: {response.StatusCode}");
                Console.WriteLine($"DEBUG - Response Content: {content}");

                if (response.IsSuccessStatusCode)
                {
                    return Content(content, "application/json");
                }
                else
                {
                    Console.WriteLine($"ERROR - API request failed with status {response.StatusCode}: {content}");
                    return StatusCode((int)response.StatusCode, content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG - Exception: {ex.Message}");
                Console.WriteLine($"DEBUG - Stack Trace: {ex.StackTrace}");
                return StatusCode(500, new { error = ex.Message, details = ex.StackTrace });
            }
        }

        // Routes API Proxy
        [HttpGet]
        public async Task<IActionResult> GetRoutes(int page = 1, int pageSize = 10)
        {
            return await ProxyApiRequest($"/api/routes?page={page}&pageSize={pageSize}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoute([FromBody] object routeData)
        {
            return await ProxyApiRequest("/api/routes", HttpMethod.Post, routeData);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            return await ProxyApiRequest($"/api/routes/{id}", HttpMethod.Delete);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoutesCount()
        {
            return await ProxyApiRequest("/api/routes/TotalCount");
        }

        [HttpGet]
        public async Task<IActionResult> GetStopsByRouteId(int routeId, int page = 1, int pageSize = 10)
        {
            return await ProxyApiRequest($"/api/routes/{routeId}/stops/?page={page}&pageSize={pageSize}");
        }

        // Stops API Proxy
        [HttpGet]
        public async Task<IActionResult> GetStops(int page = 1, int pageSize = 10)
        {
            return await ProxyApiRequest($"/api/stops?page={page}&pageSize={pageSize}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateStop([FromBody] object stopData)
        {
            return await ProxyApiRequest("/api/stops", HttpMethod.Post, stopData);
        }

        [HttpGet]
        public async Task<IActionResult> GetStopsCount()
        {
            return await ProxyApiRequest("/api/stops/TotalCount");
        }

        // Trips API Proxy
        [HttpGet]
        public async Task<IActionResult> GetTrips(int page = 1, int pageSize = 10)
        {
            return await ProxyApiRequest($"/api/trips?page={page}&pageSize={pageSize}");
        }

        [HttpGet]
        public async Task<IActionResult> GetTripsCount()
        {
            return await ProxyApiRequest("/api/trips/TotalCount");
        }

        // RouteStops API Proxy
        [HttpGet]
        public async Task<IActionResult> GetRouteStops(int page = 1, int pageSize = 10)
        {
            return await ProxyApiRequest($"/api/routestop/GetAll?page={page}&pageSize={pageSize}");
        }

        [HttpGet]
        public async Task<IActionResult> GetRouteStopsCount()
        {
            return await ProxyApiRequest("/api/routestop/TotalCount");
        }

        public IActionResult AdminMainPage()
        {
            if (!IsUserAuthenticated())
            {
                TempData["ErrorMessage"] = "Admin Panel'e erişmek için giriş yapmanız gerekiyor.";
                return RedirectToAction("Login", "Account");
            }
            
            // Admin rolü kontrolü
            if (!IsUserAdmin())
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişim yetkiniz bulunmamaktadır. Sadece Admin kullanıcıları bu sayfaya erişebilir.";
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.GatewayUrl = _configuration["ApiSettings:GatewayUrl"];
            ViewBag.ApiKey = _configuration["ApiSettings:ApiKey"];
            return View();
        }

        public IActionResult Routes()
        {
            if (!IsUserAuthenticated())
            {
                TempData["ErrorMessage"] = "Admin Panel'e erişmek için giriş yapmanız gerekiyor.";
                return RedirectToAction("Login", "Account");
            }
            
            // Admin rolü kontrolü
            if (!IsUserAdmin())
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişim yetkiniz bulunmamaktadır. Sadece Admin kullanıcıları bu sayfaya erişebilir.";
                return RedirectToAction("Index", "Home");
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
            
            // Admin rolü kontrolü
            if (!IsUserAdmin())
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişim yetkiniz bulunmamaktadır. Sadece Admin kullanıcıları bu sayfaya erişebilir.";
                return RedirectToAction("Index", "Home");
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
            
            // Admin rolü kontrolü
            if (!IsUserAdmin())
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişim yetkiniz bulunmamaktadır. Sadece Admin kullanıcıları bu sayfaya erişebilir.";
                return RedirectToAction("Index", "Home");
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
            
            // Admin rolü kontrolü
            if (!IsUserAdmin())
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişim yetkiniz bulunmamaktadır. Sadece Admin kullanıcıları bu sayfaya erişebilir.";
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }
    }
}
