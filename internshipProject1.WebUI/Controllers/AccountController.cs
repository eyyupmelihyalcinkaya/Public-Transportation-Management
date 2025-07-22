using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace internshipProject1.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetSession()
        {
            try
            {
                // Request body'yi manuel olarak oku
                using var reader = new StreamReader(Request.Body);
                var requestBody = await reader.ReadToEndAsync();
                Console.WriteLine($"DEBUG - Raw request body: {requestBody}");
                
                if (string.IsNullOrEmpty(requestBody))
                {
                    Console.WriteLine("DEBUG - Request body is empty");
                    return Json(new { success = false, error = "Request body is empty" });
                }
                
                // JSON'u manuel parse et
                var jsonData = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(requestBody);
                
                var token = jsonData.ContainsKey("Token") ? jsonData["Token"].ToString() : "";
                var username = jsonData.ContainsKey("Username") ? jsonData["Username"].ToString() : "";
                
                Console.WriteLine($"DEBUG - Parsed Token: {token?.Substring(0, Math.Min(20, token?.Length ?? 0))}...");
                Console.WriteLine($"DEBUG - Parsed Username: {username}");
                
                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(username))
                {
                    Console.WriteLine("DEBUG - Token or username is empty");
                    return Json(new { success = false, error = "Token or username is empty" });
                }
                
                // Session'a token ve kullanıcı bilgilerini kaydet
                HttpContext.Session.SetString("UserToken", token);
                HttpContext.Session.SetString("username", username);
                
                Console.WriteLine($"DEBUG - Session UserToken set: {HttpContext.Session.GetString("UserToken")?.Substring(0, Math.Min(20, HttpContext.Session.GetString("UserToken")?.Length ?? 0))}...");
                
                // Cookie'ye de kaydet (güvenli) - Development için Secure = false
                Response.Cookies.Append("UserToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false, // Development için false
                    SameSite = SameSiteMode.Lax, // Daha esnek
                    Expires = DateTimeOffset.Now.AddHours(1)
                });
                
                Console.WriteLine("DEBUG - Cookie UserToken set successfully");
                
                return Json(new { success = true, message = "Session and cookie set successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG - SetSession error: {ex.Message}");
                Console.WriteLine($"DEBUG - SetSession stack trace: {ex.StackTrace}");
                return Json(new { success = false, error = ex.Message });
            }
        }
        
        public IActionResult Register()
        {
            return View();
        }
        
        public IActionResult Logout()
        {
            // Session ve cookie'yi temizle
            HttpContext.Session.Clear();
            Response.Cookies.Delete("UserToken");
            
            // Logout sayfasına yönlendir (JavaScript ile localStorage temizlemek için)
            return View("LogoutProcess");
        }
    }
}
