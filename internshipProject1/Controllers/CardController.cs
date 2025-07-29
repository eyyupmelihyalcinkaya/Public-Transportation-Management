using internshipproject1.Application.Features.Card.Commands.AddCard;
using internshipproject1.Application.Features.Card.Commands.DeleteCard;
using internshipproject1.Application.Features.Card.Commands.UpdateCard;
using internshipproject1.Application.Features.Card.Queries.GetAllActiveCards;
using internshipproject1.Application.Features.Card.Queries.GetAllCards;
using internshipproject1.Application.Features.Card.Queries.GetCardById;
using internshipproject1.Application.Features.Card.Queries.IsCardExist;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace internshipProject1.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCards()
        {
            var cards = await _mediator.Send(new GetAllCardsQueryRequest());
            if (cards == null || !cards.Any())
            {
                return NotFound("No cards found.");
            }
            return Ok(cards);
        }

        [HttpGet("ActiveCards")]
        public async Task<ActionResult> GetAllActiveCards()
        {
            var activeCards = await _mediator.Send(new GetAllActiveCardsQueryRequest());
            if (activeCards == null || !activeCards.Any())
            {
                return NotFound("No active cards found.");
            }
            return Ok(activeCards);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult> GetCardById([FromQuery] int id)
        { 
            var card = await _mediator.Send(new GetCardByIdQueryRequest{ Id = id });
            if (card == null)
            {
                return NotFound($"Card with ID {id} not found.");
            }
            return Ok(card);
        }

        [HttpGet("InactiveCards")]
        public async Task<ActionResult> GetInactiveCards()
        { 
            var inactiveCards = await _mediator.Send(new GetAllCardsQueryRequest());

            if (inactiveCards == null || !inactiveCards.Any())
            {
                return NotFound("No inactive cards found.");
            }
            return Ok(inactiveCards.Where(c=>c.IsActive = false).ToList());

        }
        [HttpGet("IsExist")]
        public async Task<ActionResult> IsCardExist([FromQuery] int id)
        { 
            var isExist = await _mediator.Send(new IsCardExistQueryRequest() { Id = id });
            if (isExist == null)
            {
                return NotFound($"Card with ID {id} not found.");
            }
            return Ok(isExist);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewCard([FromBody] AddCardCommandRequest request)
        { 
            var newCard = await _mediator.Send(request);
            return Ok(newCard);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCard([FromQuery] int id)
        {
            var deletedCard = await _mediator.Send(new DeleteCardCommandRequest(id));
            if (deletedCard == null)
            {
                return NotFound($"Card with ID {id} not found.");
            }
            return Ok(deletedCard);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCard([FromBody] UpdateCardCommandRequest request)
        {
            var updatedCard = await _mediator.Send(request);
            if (updatedCard == null)
            {
                return NotFound($"Card with ID {request.Id} not found.");
            }
            return Ok(updatedCard);
        }
    }
}
