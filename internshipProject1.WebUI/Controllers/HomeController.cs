using internshipProject1.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace internshipProject1.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.GatewayUrl = _configuration["ApiSettings:GatewayUrl"];
            ViewBag.ApiKey = _configuration["ApiSettings:ApiKey"];
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.GatewayUrl = _configuration["ApiSettings:GatewayUrl"];
            ViewBag.ApiKey = _configuration["ApiSettings:ApiKey"];
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
