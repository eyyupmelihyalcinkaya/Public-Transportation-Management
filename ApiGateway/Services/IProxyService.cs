using ApiGateway.Models;

namespace ApiGateway.Services
{
    public interface IProxyService
    {
        Task<ProxyResponse> ForwardRequestAsync(HttpContext context, ServiceRoute route);
        Task<bool> IsServiceHealthyAsync(string serviceUrl);
        Task<ProxyResponse> HealthCheckAsync(string serviceUrl);
    }
}