using ApiGateway.Configuration;
using Microsoft.Extensions.Options;
using ApiGateway.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Services.Configure<GatewaySettings>(
    builder.Configuration.GetSection("GatewaySettings"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HTTP Client
builder.Services.AddHttpClient();

// Services
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IProxyService, ProxyService>();

// Health Checks
var healthChecksBuilder = builder.Services.AddHealthChecks();
var settings = builder.Configuration.GetSection("GatewaySettings").Get<GatewaySettings>();
if (settings?.Services != null)
{
    foreach (var (serviceName, serviceConfig) in settings.Services)
    {
        if (serviceConfig.IsActive && !string.IsNullOrEmpty(serviceConfig.BaseUrl) && !string.IsNullOrEmpty(serviceConfig.HealthEndpoint))
        {
            var healthCheckUrl = $"{serviceConfig.BaseUrl.TrimEnd('/')}{serviceConfig.HealthEndpoint}";
            healthChecksBuilder.AddUrlGroup(
                new Uri(healthCheckUrl, UriKind.RelativeOrAbsolute),
                name: serviceName,
                failureStatus: HealthStatus.Unhealthy,
                timeout: TimeSpan.FromSeconds(serviceConfig.TimeoutSeconds > 0 ? serviceConfig.TimeoutSeconds : 10),
                tags: new[] { "services" });
        }
    }
}

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.User.Identity?.Name ?? context.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 1000,
                Window = TimeSpan.FromMinutes(1)
            }));
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = WriteHealthCheckResponse
});

static Task WriteHealthCheckResponse(HttpContext context, HealthReport report)
{
    context.Response.ContentType = "application/json";
    var options = new JsonSerializerOptions { WriteIndented = true };

    var result = new
    {
        status = report.Status.ToString(),
        timestamp = DateTime.UtcNow,
        services = report.Entries.ToDictionary(
            e => e.Key,
            e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                responseTime = e.Value.Duration.ToString(),
                errorMessage = e.Value.Exception?.Message ?? e.Value.Description,
                lastChecked = DateTime.UtcNow
            }),
        uptime = report.TotalDuration.ToString()
    };

    return context.Response.WriteAsync(JsonSerializer.Serialize(result, options));
}

app.Run();
