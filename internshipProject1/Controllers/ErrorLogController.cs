using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using internshipproject1.Application.Interfaces.Services;

namespace internshipProject1.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Sadece authenticated kullanıcılar error log'ları görebilir
    public class ErrorLogController : ControllerBase
    {
        private readonly IErrorLogService _errorLogService;

        public ErrorLogController(IErrorLogService errorLogService)
        {
            _errorLogService = errorLogService;
        }

        /// <summary>
        /// Tüm error log'ları getirir (en yeniden eskiye doğru sıralı)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllErrorLogs()
        {
            try
            {
                var errorLogs = await _errorLogService.GetAllAsync();
                return Ok(errorLogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error log'ları getirirken hata oluştu: {ex.Message}");
            }
        }

        /// <summary>
        /// ID'ye göre spesifik error log getirir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetErrorLogById(string id)
        {
            try
            {
                var errorLog = await _errorLogService.GetByIdAsync(id);
                if (errorLog == null)
                {
                    return NotFound($"ID {id} ile error log bulunamadı.");
                }
                return Ok(errorLog);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error log getirirken hata oluştu: {ex.Message}");
            }
        }
    }
} 