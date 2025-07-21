using FluentValidation;
using internshipproject1.Application.DTOs;
using internshipproject1.Application.Exceptions;
using internshipproject1.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace internshipProject1.WebAPI.Middlewares
{
    public class ErrorMiddleware : IMiddleware
    {
        private readonly IErrorLogService _errorLogService;
        private readonly ILogger<ErrorMiddleware> _logger;

        public ErrorMiddleware(IErrorLogService errorLogService, ILogger<ErrorMiddleware> logger)
        {
            _errorLogService = errorLogService;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            
            List<string> errors = new()
            {
                exception.Message,
                exception.InnerException?.ToString()
            };

            if (exception.GetType() == typeof(FluentValidation.ValidationException))
            {
                var validationException = (FluentValidation.ValidationException)exception;

                await context.Response.WriteAsync(
                    new ExceptionModel
                    {
                        Errors = validationException.Errors.Select(e => e.ErrorMessage).ToList(),
                        StatusCode = (int)statusCode
                    }.ToString()
                );

            }
            var errorLog = new ErrorLogDTO
            {
                Id = Guid.NewGuid().ToString(),
                ErrorMessage = exception.Message,
                UserFriendlyMessage = GetUserFriendlyMessage(exception),
                Path = context.Request.Path,
                Method = context.Request.Method,
                CreatedAt = DateTime.UtcNow
            };

            // MongoDB'ye logla
            try
            {
                await _errorLogService.LogErrorAsync(errorLog);
                _logger.LogError(exception, "Hata MongoDB'ye kaydedildi. Path: {Path}, Method: {Method}", 
                    context.Request.Path, context.Request.Method);
            }
            catch (Exception logException)
            {
                _logger.LogError(logException, "MongoDB'ye hata loglanamadı. Orijinal hata: {OriginalError}", exception.Message);
            }

            var response = new
            {
                error = new
                {
                    message = GetUserFriendlyMessage(exception),
                    statusCode = (int)statusCode,
                    timestamp = DateTime.UtcNow,
                    path = context.Request.Path.ToString()
                }
            };

            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }

        private static HttpStatusCode GetStatusCode(Exception exception)
        {
            return exception switch
            {
                ArgumentNullException => HttpStatusCode.BadRequest,
                ArgumentException => HttpStatusCode.BadRequest,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                KeyNotFoundException => HttpStatusCode.NotFound,
                NotImplementedException => HttpStatusCode.NotImplemented,
                TimeoutException => HttpStatusCode.RequestTimeout,
                _ => HttpStatusCode.InternalServerError
            };
        }

        private static string GetUserFriendlyMessage(Exception exception)
        {
            return exception switch
            {
                ArgumentNullException => "Gerekli parametreler eksik.",
                WrongUsernameOrPasswordException => "Kullanıcı adı veya şifre yanlış.",
                ArgumentException => "Geçersiz parametreler.",
                UnauthorizedAccessException => "Bu işlem için yetkiniz bulunmuyor.",
                KeyNotFoundException => "Aradığınız kayıt bulunamadı.",
                NotImplementedException => "Bu özellik henüz uygulanmamış.",
                UserAlreadyRegisteredException => "Bu kullanıcı zaten kayıtlı.",
                InvalidPasswordException => "Geçersiz şifre.",
                TimeoutException => "İşlem zaman aşımına uğradı.",
                _ => "Beklenmeyen bir hata oluştu."
            };
        }
    }
}
