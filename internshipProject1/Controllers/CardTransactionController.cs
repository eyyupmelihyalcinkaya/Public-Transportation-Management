using internshipproject1.Application.Features.CardTransaction.Commands.CreateTransaction;
using internshipproject1.Application.Features.CardTransaction.Commands.DeleteTransaction;
using internshipproject1.Application.Features.CardTransaction.Commands.UpdateTransaction;
using internshipproject1.Application.Features.CardTransaction.Queries.GetAll;
using internshipproject1.Application.Features.CardTransaction.Queries.GetByCardId;
using internshipproject1.Application.Features.CardTransaction.Queries.GetById;
using internshipproject1.Application.Features.CardTransaction.Queries.IsExists;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace internshipProject1.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardTransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CardTransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("AllCardTransactions")]
        public async Task<ActionResult> GetAllTransactions()
        { 
            var transactions = await _mediator.Send(new GetAllTransactionsQueryRequest());
            if (transactions == null || !transactions.Any())
            {
                return NotFound("No transactions found.");
            }
            return Ok(transactions);
        }
        [HttpGet("GetTransactionById")]
        public async Task<ActionResult> GetCardTransactionById([FromQuery] int id)
        { 
            var transaction = await _mediator.Send(new GetByIdQueryRequest(id));
            if (transaction == null)
            {
                return NotFound($"Transaction with ID {id} not found.");
            }
            return Ok(transaction);
        }
        [HttpGet("GetTransactionByCardId")]
        public async Task<ActionResult> GetCardTransactionByCardId([FromQuery] int cardId)
        {
            var transactions = await _mediator.Send(new GetByCardIdQueryRequest(cardId));
            if (transactions == null)
            {
                return NotFound($"No transactions found for card ID {cardId}.");
            }
            return Ok(transactions);
        }
        [HttpGet("IsExists")]
        public async Task<ActionResult> IsTransactionExists([FromQuery] int id)
        {
            var exists = await _mediator.Send(new IsExistQueryRequest(id));
            if (exists == null)
            {
                return NotFound($"Transaction with ID {id} not found.");
            }
            return Ok(exists);
        }









        [HttpPost]
        public async Task<ActionResult> AddCardTransaction([FromBody] CreateTransactionCommandRequest request)
        { 
            var transaction = await _mediator.Send(request);
            if (transaction == null)
            {
                return BadRequest("Transaction could not be created.");
            }
            return CreatedAtAction(nameof(AddCardTransaction), new { id = transaction.Id }, transaction);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCardTransaction([FromQuery] int id)
        {
            var deletedTransaction = await _mediator.Send(new DeleteTransactionCommandRequest(id));
            if (deletedTransaction == null)
            {
                return NotFound($"Transaction with ID {id} not found.");
            }
            return Ok(deletedTransaction);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateCardTransaction([FromBody] UpdateTransactionCommandRequest request)
        { 
            var updatedTransaction = await _mediator.Send(request);
            if (updatedTransaction == null)
            {
                return NotFound($"Transaction with ID {request.Id} not found.");
            }
            return Ok(updatedTransaction);
        }
    }
}
