using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Security.Claims;
using internshipproject1.Domain.Auth;

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

        // Admin rolü kontrolü - session ve cookie'den kontrol et
        private bool IsUserAdminOrSuperAdmin()
        {
            // Session'dan rol kontrolü
            var userRole = HttpContext.Session.GetString("userRole");
            Console.WriteLine($"DEBUG - User Role from Session: {userRole}");
            
            // Cookie'den rol kontrolü (fallback)
            if (string.IsNullOrEmpty(userRole))
            {
                userRole = Request.Cookies["userRole"];
                Console.WriteLine($"DEBUG - User Role from Cookie: {userRole}");
            }
            
            // LocalStorage'dan gelen token'ı kontrol et (JavaScript'ten gönderilen)
            if (string.IsNullOrEmpty(userRole))
            {
                // Request header'larından token bilgisini al
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                {
                    // JWT token'dan rol bilgisini çıkar (basit kontrol)
                    Console.WriteLine($"DEBUG - Authorization Header Found: {authHeader}");
                }
            }
            
            // Role = "1" (SuperAdmin) veya "2" (Admin) ise erişim ver
            var isAdmin = userRole == "1" || userRole == "2";
            Console.WriteLine($"DEBUG - Is User Admin: {isAdmin} (Role: {userRole})");
            
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
                // Authorization'ı ilet (varsa)
                try
                {
                    var incomingAuth = Request.Headers["Authorization"].FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(incomingAuth))
                    {
                        request.Headers.Add("Authorization", incomingAuth);
                    }
                    else
                    {
                        var sessionToken = HttpContext.Session.GetString("UserToken");
                        if (!string.IsNullOrWhiteSpace(sessionToken))
                        {
                            if (sessionToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                            {
                                request.Headers.Add("Authorization", sessionToken);
                            }
                            else
                            {
                                // JSON olabilir
                                try
                                {
                                    using var doc = JsonDocument.Parse(sessionToken);
                                    string? access = null;
                                    if (doc.RootElement.TryGetProperty("token", out var tokenObj) && tokenObj.TryGetProperty("accessToken", out var acc))
                                    {
                                        access = acc.GetString();
                                    }
                                    else if (doc.RootElement.TryGetProperty("accessToken", out var acc2))
                                    {
                                        access = acc2.GetString();
                                    }
                                    if (!string.IsNullOrWhiteSpace(access))
                                    {
                                        request.Headers.Add("Authorization", $"Bearer {access}");
                                    }
                                }
                                catch { /* ignore parse errors */ }
                            }
                        }
                    }
                }
                catch { }

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
            if (!IsUserAdminOrSuperAdmin())
            {
                return Forbid("Bu API'ye erişim yetkiniz bulunmamaktadır.");
            }
            return await ProxyApiRequest($"/api/routes?page={page}&pageSize={pageSize}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoute([FromBody] object routeData)
        {
            return await ProxyApiRequest("/api/routes", HttpMethod.Post, routeData);
        }

        // Create Route with multiple stops
        [HttpPost]
        public async Task<IActionResult> CreateRouteWithStops([FromBody] object routeWithStops)
        {
            return await ProxyApiRequest("/api/Routes/CreateRouteWithStops", HttpMethod.Post, routeWithStops);
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

        [HttpPost]
        public async Task<IActionResult> CreateTrip([FromBody] object tripData)
        {
            return await ProxyApiRequest("/api/trips", HttpMethod.Post, tripData);
        }

        // RouteStops API Proxy
        [HttpGet]
        public async Task<IActionResult> GetRouteStops(int routeId, int page = 1, int pageSize = 100)
        {
            return await ProxyApiRequest($"/api/routes/{routeId}/stops?page={page}&pageSize={pageSize}");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRouteStops(int page = 1, int pageSize = 10)
        {
            return await ProxyApiRequest($"/api/routestop/GetAll?page={page}&pageSize={pageSize}");
        }

        [HttpGet]
        public async Task<IActionResult> GetRouteStopsCount()
        {
            return await ProxyApiRequest("/api/routestop/TotalCount");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRouteStop([FromBody] object routeStopData)
        {
            return await ProxyApiRequest("/api/routestop", HttpMethod.Post, routeStopData);
        }

        public IActionResult AdminMainPage()
        {
            // RBAC kontrolü - SuperAdmin veya Admin rolü gerekli
            if (!IsUserAdminOrSuperAdmin())
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişim yetkiniz bulunmamaktadır. Sadece Admin ve SuperAdmin kullanıcıları bu sayfaya erişebilir.";
                return RedirectToAction("Dashboard", "User"); // Normal kullanıcıları User Dashboard'a yönlendir
            }
            
            ViewBag.GatewayUrl = _configuration["ApiSettings:GatewayUrl"];
            ViewBag.ApiKey = _configuration["ApiSettings:ApiKey"];
            return View();
        }

        // Only SuperAdmin (role 1) visible/accessible page for managing permissions (frontend-only)
        public IActionResult Permissions()
        {
            // Frontend guard: allow only SuperAdmin
            var role = HttpContext.Session.GetString("userRole") ?? Request.Cookies["userRole"];
            if (role != "1")
            {
                TempData["ErrorMessage"] = "Bu sayfaya sadece SuperAdmin erişebilir.";
                return RedirectToAction("AdminMainPage");
            }

            ViewBag.GatewayUrl = _configuration["ApiSettings:GatewayUrl"];
            ViewBag.ApiKey = _configuration["ApiSettings:ApiKey"];
            return View();
        }

        public IActionResult Routes()
        {
            if (!IsUserAdminOrSuperAdmin())
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişim yetkiniz bulunmamaktadır. Sadece Admin ve SuperAdmin kullanıcıları bu sayfaya erişebilir.";
                return RedirectToAction("Dashboard", "User");
            }
            
            return View();
        }

        public IActionResult Stops()
        {
            if (!IsUserAdminOrSuperAdmin())
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişim yetkiniz bulunmamaktadır. Sadece Admin ve SuperAdmin kullanıcıları bu sayfaya erişebilir.";
                return RedirectToAction("Dashboard", "User");
            }
            
            return View();
        }

        public IActionResult Trips()
        {
            if (!IsUserAdminOrSuperAdmin())
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişim yetkiniz bulunmamaktadır. Sadece Admin ve SuperAdmin kullanıcıları bu sayfaya erişebilir.";
                return RedirectToAction("Dashboard", "User");
            }
            
            return View();
        }

        public IActionResult RouteStops()
        {
            if (!IsUserAdminOrSuperAdmin())
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişim yetkiniz bulunmamaktadır. Sadece Admin ve SuperAdmin kullanıcıları bu sayfaya erişebilir.";
                return RedirectToAction("Dashboard", "User");
            }
            
            return View();
        }
    }
}
