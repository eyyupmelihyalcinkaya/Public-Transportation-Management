using ApiGateway.Models;

namespace ApiGateway.Services
{
    public interface IRouteService
    {
        ServiceRoute? GetRoute(string path, string method);
        bool IsRouteAllowed(string path, string method);
        string GetServiceUrl(string serviceName, string path);
        List<ServiceRoute> GetAllRoutes();
    }
}
