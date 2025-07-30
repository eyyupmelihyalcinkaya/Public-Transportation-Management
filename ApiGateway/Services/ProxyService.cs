using ApiGateway.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ApiGateway.Services
{
    public class ProxyService : IProxyService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProxyService> _logger;

        public ProxyService(HttpClient httpClient, ILogger<ProxyService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ProxyResponse> ForwardRequestAsync(HttpContext context, ServiceRoute route)
        {
            var startTime = DateTime.UtcNow;
            var requestId = context.Request.Headers["X-Request-ID"].FirstOrDefault() ?? Guid.NewGuid().ToString();

            try
            {
                _logger.LogInformation("[{RequestId}] Forwarding request to {ServiceName} at {TargetUrl}", 
                    requestId, route.ServiceName, route.TargetUrl);

                // Create the target URL
                var targetUrl = $"{route.TargetUrl.TrimEnd('/')}{context.Request.Path.Value}";
                if (!string.IsNullOrEmpty(context.Request.QueryString.Value))
                {
                    targetUrl += context.Request.QueryString.Value;
                }

                // Create HTTP request message
                var requestMessage = new HttpRequestMessage
                {
                    Method = new HttpMethod(context.Request.Method),
                    RequestUri = new Uri(targetUrl)
                };

                // Copy headers (excluding host and connection headers)
                foreach (var header in context.Request.Headers)
                {
                    if (!header.Key.Equals("Host", StringComparison.OrdinalIgnoreCase) &&
                        !header.Key.Equals("Connection", StringComparison.OrdinalIgnoreCase))
                    {
                        requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                    }
                }

                // Copy body for POST/PUT requests
                if (context.Request.Body != null && 
                    (context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase) ||
                     context.Request.Method.Equals("PUT", StringComparison.OrdinalIgnoreCase) ||
                     context.Request.Method.Equals("PATCH", StringComparison.OrdinalIgnoreCase)))
                {
                    using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
                    var bodyContent = await reader.ReadToEndAsync();
                    if (!string.IsNullOrEmpty(bodyContent))
                    {
                        requestMessage.Content = new StringContent(bodyContent, Encoding.UTF8, context.Request.ContentType ?? "application/json");
                    }
                }

                // Set timeout
                _httpClient.Timeout = TimeSpan.FromSeconds(route.TimeoutSeconds);

                // Send request
                var response = await _httpClient.SendAsync(requestMessage);

                // Read response content
                var responseContent = await response.Content.ReadAsStringAsync();

                var responseTime = DateTime.UtcNow - startTime;

                _logger.LogInformation("[{RequestId}] Response received from {ServiceName} in {ResponseTime}ms with status {StatusCode}", 
                    requestId, route.ServiceName, responseTime.TotalMilliseconds, response.StatusCode);

                return new ProxyResponse
                {
                    StatusCode = (int)response.StatusCode,
                    Headers = response.Headers.ToDictionary(h => h.Key, h => string.Join(", ", h.Value)),
                    Body = responseContent,
                    IsSuccess = response.IsSuccessStatusCode,
                    ResponseTime = responseTime,
                    ContentType = response.Content.Headers.ContentType?.ToString()
                };
            }
            catch (HttpRequestException ex)
            {
                var responseTime = DateTime.UtcNow - startTime;
                _logger.LogError(ex, "[{RequestId}] HTTP request failed for {ServiceName}", requestId, route.ServiceName);

                return new ProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadGateway,
                    Body = "Service temporarily unavailable",
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseTime = responseTime
                };
            }
            catch (TaskCanceledException ex)
            {
                var responseTime = DateTime.UtcNow - startTime;
                _logger.LogError(ex, "[{RequestId}] Request timeout for {ServiceName}", requestId, route.ServiceName);

                return new ProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.GatewayTimeout,
                    Body = "Request timeout",
                    IsSuccess = false,
                    ErrorMessage = "Request timed out",
                    ResponseTime = responseTime
                };
            }
            catch (Exception ex)
            {
                var responseTime = DateTime.UtcNow - startTime;
                _logger.LogError(ex, "[{RequestId}] Unexpected error forwarding request to {ServiceName}", requestId, route.ServiceName);

                return new ProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Body = "Internal server error",
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseTime = responseTime
                };
            }
        }

        public async Task<bool> IsServiceHealthyAsync(string serviceUrl)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{serviceUrl.TrimEnd('/')}/health");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Health check failed for service at {ServiceUrl}", serviceUrl);
                return false;
            }
        }

        public async Task<ProxyResponse> HealthCheckAsync(string serviceUrl)
        {
            var startTime = DateTime.UtcNow;

            try
            {
                var response = await _httpClient.GetAsync($"{serviceUrl.TrimEnd('/')}/health");
                var content = await response.Content.ReadAsStringAsync();

                return new ProxyResponse
                {
                    StatusCode = (int)response.StatusCode,
                    Body = content,
                    IsSuccess = response.IsSuccessStatusCode,
                    ResponseTime = DateTime.UtcNow - startTime,
                    ContentType = response.Content.Headers.ContentType?.ToString()
                };
            }
            catch (Exception ex)
            {
                return new ProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.ServiceUnavailable,
                    Body = "Health check failed",
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseTime = DateTime.UtcNow - startTime
                };
            }
        }
    }
}
