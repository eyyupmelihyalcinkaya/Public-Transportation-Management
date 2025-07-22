using internshipproject1.Application.Interfaces.Services;
using internshipproject1.Domain.Entities;
using System.Text;

namespace internshipProject1.WebAPI.Middlewares
{
    public class LogMiddleware : IMiddleware
    {
        private readonly ILogService _logService;

        public LogMiddleware(ILogService logService)
        {
            _logService = logService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Request.EnableBuffering();

            string requestBody = string.Empty;
            if (context.Request.ContentLength > 0 && context.Request.Body.CanSeek)
            {
                context.Request.Body.Position = 0;
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
                {
                    requestBody = await reader.ReadToEndAsync();
                }
                context.Request.Body.Position = 0;
            }

            var originalResponseBodyStream = context.Response.Body;

            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                await next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                var level = context.Response.StatusCode >= 500 ? "Error"
                          : context.Response.StatusCode >= 400 ? "Warning"
                          : "Info";

                var log = new Log
                {
                    Level = level,
                    Message = $"Request: {requestBody}, Response: {responseBodyText}",
                    RequestPath = context.Request.Path,
                    RequestMethod = context.Request.Method,
                    StatusCode = context.Response.StatusCode,
                    CreatedAt = DateTime.UtcNow
                };

                await _logService.LogAsync(log);

                await responseBody.CopyToAsync(originalResponseBodyStream);
            }
            finally
            {
                context.Response.Body = originalResponseBodyStream;
            }
        }
    }
}
