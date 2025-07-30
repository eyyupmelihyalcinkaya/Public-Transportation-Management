using ApiGateway.Models;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly IProxyService _proxyService;
        private readonly IRouteService _routeService;
        private readonly ILogger<HealthController> _logger;

        public HealthController(
            IProxyService proxyService,
            IRouteService routeService,
            ILogger<HealthController> logger)
        {
            _proxyService = proxyService;
            _routeService = routeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<HealthCheckResponse>> GetHealth()
        {
            var startTime = DateTime.UtcNow;
            var services = new Dictionary<string, ServiceHealth>();

            try
            {
                // Main API Health Check
                var mainApiHealth = await CheckServiceHealth("MainApi", "/api/cards");
                services.Add("MainApi", mainApiHealth);

                // Payment Service Health Check
                var paymentServiceHealth = await CheckServiceHealth("PaymentService", "/api/payments");
                services.Add("PaymentService", paymentServiceHealth);

                var overallStatus = services.All(s => s.Value.Status == "Healthy") ? "Healthy" : "Unhealthy";

                return Ok(new HealthCheckResponse
                {
                    Status = overallStatus,
                    Timestamp = DateTime.UtcNow,
                    Services = services,
                    Uptime = DateTime.UtcNow - startTime
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during health check");
                return StatusCode(500, new HealthCheckResponse
                {
                    Status = "Error",
                    Timestamp = DateTime.UtcNow,
                    Services = services,
                    Uptime = DateTime.UtcNow - startTime
                });
            }
        }

        private async Task<ServiceHealth> CheckServiceHealth(string serviceName, string healthEndpoint)
        {
            var startTime = DateTime.UtcNow;
            
            try
            {
                var serviceUrl = _routeService.GetServiceUrl(serviceName, healthEndpoint);
                if (string.IsNullOrEmpty(serviceUrl))
                {
                    return new ServiceHealth
                    {
                        Name = serviceName,
                        Status = "Unknown",
                        ErrorMessage = "Service URL not found",
                        LastChecked = DateTime.UtcNow
                    };
                }

                var healthResponse = await _proxyService.HealthCheckAsync(serviceUrl);
                
                return new ServiceHealth
                {
                    Name = serviceName,
                    Status = healthResponse.IsSuccess ? "Healthy" : "Unhealthy",
                    ResponseTime = healthResponse.ResponseTime,
                    ErrorMessage = healthResponse.IsSuccess ? null : healthResponse.ErrorMessage,
                    LastChecked = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                return new ServiceHealth
                {
                    Name = serviceName,
                    Status = "Error",
                    ResponseTime = DateTime.UtcNow - startTime,
                    ErrorMessage = ex.Message,
                    LastChecked = DateTime.UtcNow
                };
            }
        }
    }
} 