using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using internshipproject1.Application.DTOs;
using internshipproject1.Application.Features.User.Commands.Register;
using internshipproject1.Application.Features.User;
using Microsoft.Extensions.Configuration;
using MediatR;
using internshipproject1.Application.Features.User.Commands.Login;
using internshipproject1.Application.Features.User.Commands.ChangePassword;
using internshipproject1.Application.Features.User.Queries.GetAllUsers;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase

    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Public API

        //POST Login
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            if (response == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(response);
        }

        //POST Register
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            if (response == null)
            {
                return BadRequest("User registration failed.");
            }
            return Ok(response);
        }

        //PUT Update User
        [HttpPut("update")]
        public async Task<ActionResult> UpdatePassword(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            if (response == null)
            {
                return BadRequest("Password update failed.");
            }
            return Ok(response);
        }
        // Private API

        //GET Get All Users
        [HttpGet("all")]
        public async Task<ActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllUsersQueryRequest(), cancellationToken);
            if (response == null || !response.Any())
            {
                return NotFound("No users found.");
            }
            return Ok(response);
        }
    }
}
