using ApiGateway.Configuration;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("{*path}")] // Catch-all route for all requests
    public class GatewayController : ControllerBase
    {
        private readonly IRouteService _routeService;
        private readonly IProxyService _proxyService;
        private readonly ILogger<GatewayController> _logger;
        private readonly GatewaySettings _settings;

        public GatewayController(
            IRouteService routeService,
            IProxyService proxyService,
            ILogger<GatewayController> logger,
            IOptions<GatewaySettings> settings)
        {
            _routeService = routeService ?? throw new ArgumentNullException(nameof(routeService));
            _proxyService = proxyService ?? throw new ArgumentNullException(nameof(proxyService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        // This single method will handle all incoming API requests
        [AcceptVerbs("GET", "POST", "PUT", "DELETE")]
        public async Task<IActionResult> HandleRequest()
        {
            var requestId = GetOrGenerateRequestId();
            var method = HttpContext.Request.Method;
            var path = HttpContext.Request.Path.Value ?? "/";

            _logger.LogInformation(
                "[{RequestId}] Handling request for Path: {Path}, Method: {Method}",
                requestId, path, method);

            if (_settings.MaintenanceMode?.IsEnabled == true)
            {
                _logger.LogWarning("[{RequestId}] Request rejected due to maintenance mode.", requestId);
                return CreateErrorResponse(_settings.MaintenanceMode.Message ?? "Service unavailable", requestId, HttpStatusCode.ServiceUnavailable);
            }

            var route = _routeService.GetRoute(path, method);
            if (route == null)
            {
                _logger.LogWarning("[{RequestId}] No route found for Path: {Path}", requestId, path);
                return CreateErrorResponse($"The requested resource '{path}' was not found.", requestId, HttpStatusCode.NotFound);
            }

            if (!route.IsActive)
            {
                _logger.LogWarning("[{RequestId}] Route to service {ServiceName} is inactive.", requestId, route.ServiceName);
                return CreateErrorResponse("The requested service is temporarily unavailable.", requestId, HttpStatusCode.ServiceUnavailable);
            }

            // A proper middleware would be a better place for this check
            if (route.RequiresAuth)
            {
                if (!HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var apiKey) || apiKey != _settings.ApiKey)
                {
                    _logger.LogWarning("[{RequestId}] Unauthorized request: Missing or invalid API key.", requestId);
                    return CreateErrorResponse("Unauthorized: API key is missing or invalid.", requestId, HttpStatusCode.Unauthorized);
                }
            }

            var response = await _proxyService.ForwardRequestAsync(HttpContext, route);

            // Directly return the response from the downstream service
            // This makes the gateway transparent
            return new ContentResult
            {
                StatusCode = response.StatusCode,
                Content = response.Body?.ToString(), // Ensure Body is converted to string
                ContentType = response.ContentType
            };
        }

        private string GetOrGenerateRequestId()
        {
            return HttpContext.Request.Headers["X-Request-ID"].FirstOrDefault() ?? Guid.NewGuid().ToString();
        }

        private IActionResult CreateErrorResponse(string message, string requestId, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            var errorResponse = new
            {
                Success = false,
                Message = message,
                Errors = new List<string> { $"RequestId: {requestId}" },
                Timestamp = DateTime.UtcNow
            };
            return new ObjectResult(errorResponse) { StatusCode = (int)statusCode };
        }
    }
}

