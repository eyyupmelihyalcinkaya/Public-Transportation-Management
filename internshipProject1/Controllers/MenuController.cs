using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using internshipproject1.Application.Features.Menu.Queries.GetAll;
using Microsoft.AspNetCore.Http.HttpResults;
using internshipproject1.Application.Features.Menu.Queries.GetById;
using internshipproject1.Application.Features.Menu.Queries.IsExists;
using internshipproject1.Application.Features.Menu.Commands.AddMenu;
using internshipproject1.Application.Features.Menu.Commands.DeleteMenu;
using internshipproject1.Application.Features.Menu.Commands.UpdateMenu;
namespace internshipProject1.WebAPI.Controllers
{

    [Route("/api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MenuController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllMenus")]
        public async Task<IActionResult> GetAllMenus()
        {
            var menus = await _mediator.Send(new GetAllQueryRequest());
            if(menus == null || !menus.Any())
            {
                return NotFound("No menus found.");
            }
            return Ok(menus);
        }

        [HttpGet("GetMenuById")]
        public async Task<IActionResult> GetMenuById([FromQuery] int id)
        {
            var menu = await _mediator.Send(new GetByIdQueryRequest(id));
            if (menu == null)
            {
                return NotFound($"Menu with ID {id} not found.");
            }
            return Ok(menu);
        }
        [HttpGet("IsMenuExist")]
        public async Task<ActionResult> IsMenuExist([FromQuery] int id)
        { 
            var isExist = await _mediator.Send(new IsExistsQueryRequest(id));
            return Ok(isExist);
        }

        [HttpPost("CreateMenu")]
        public async Task<ActionResult> CreateMenu([FromBody] AddMenuCommandRequest request)
        {
            var menu = await _mediator.Send(request);
            if (menu == null)
            {
                return BadRequest("Menu creation failed.");
            }
            return CreatedAtAction(nameof(GetMenuById), new { id = menu.Id }, menu);
        }

        [HttpDelete("DeleteMenu")]
        public async Task<ActionResult> DeleteMenu([FromQuery] int id)
        { 
            var deletedMenu = await _mediator.Send(new DeleteMenuCommandRequest(id));
            return Ok(deletedMenu);
        }

        [HttpPut("UpdateMenu")]
        public async Task<ActionResult> UpdateMenu([FromQuery] int id, [FromBody] UpdateMenuCommandRequest request)
        { 
            var updateMenu = await _mediator.Send(new UpdateMenuCommandRequest() 
            {
                Id=id,
                Name=request.Name,
                Url = request.Url,
                DisplayOrder=request.DisplayOrder,
                ParentMenuId=request.ParentMenuId 
            });
            if (updateMenu == null)
            {
                return NotFound($"Menu with ID {id} not found.");
            }
            return Ok(updateMenu);
        }
    }
}
