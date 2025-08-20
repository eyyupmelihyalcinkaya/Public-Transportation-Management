using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentService.Events;
using PaymentService.Features.Commands.CreateBoardingTransaction;
using PaymentService.Services;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly RabbitMqService _rabbitMqService;
        private readonly PaymentDbContext _dbContext;
        private readonly IMediator _mediator;

        public PaymentController(RabbitMqService rabbitMqService, PaymentDbContext dbContext, IMediator mediator)
        {
            _rabbitMqService = rabbitMqService;
            _dbContext = dbContext;
            _mediator = mediator;
        }

        [HttpPost("Boarding")]
        public async Task<IActionResult> CreateBoardingTransaction([FromBody] CreateBoardingTransactionCommandRequest request)
        {
            try 
            {
                var result = await _mediator.Send(request);
                if (result == null)
                {
                    return BadRequest("Failed to create boarding transaction.");
                }
                var boardingEvent = new BoardingCompletedEvent
                {
                    CardNumber = result.CardNumber,
                    UserId = result.UserId,
                    Amount = result.Amount,
                    TransactionType = result.TransactionType,
                    TransactionDate = result.TransactionDate,
                    VehicleType = result.VehicleType,
                    isStudent = result.isStudent
                };

                _rabbitMqService.Publish(boardingEvent);
                return Ok(new { Message = "Boarding transaction created successfully.", TransactionId = result.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var transactions = await _dbContext.BoardingTransactions
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var totalCount = await _dbContext.BoardingTransactions.CountAsync();

                var result = new
                {
                    Items = transactions,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.Message,
                    innerException = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

    }
}
