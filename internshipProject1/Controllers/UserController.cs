using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Core.DTOs;
using Core.Entities;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Auth;
using internshipProject1.Infrastructure.Data.Context;
using internshipproject1.Application.DTOs;
using internshipproject1.Application.Features.User.Commands.Register;
using internshipproject1.Application.Features.User;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase

    {
       private readonly AppDbContext _dbContext;
       private readonly IConfiguration _configuration;
        private readonly UserRegisterHandler _userRegisterHandler;

        public UserController(AppDbContext dbContext, IConfiguration configuration, UserRegisterHandler userRegisterHandler) {

            _dbContext = dbContext;
            _configuration = configuration;
            _userRegisterHandler = userRegisterHandler;
            
        }
        // Password Hash üretme fonksiyonu
        private static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt) { 
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        // Hash kontrolü
        private static bool VerifyPasswordHash(string password, byte[] hash, byte[] salt) {
            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(hash);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterDTO request) {
            var command = new UserRegisterCommand
            {
                userName = request.userName,
                password = request.password
            };
            if(string.IsNullOrEmpty(command.userName) || string.IsNullOrEmpty(command.password)) {
                return BadRequest("Username and Password cannot be empty");
            }
            var result = await _userRegisterHandler.Handle(command);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDTO request) { 
            
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.userName == request.userName);
            if (user == null) { return BadRequest("Username cannot be empty"); };
            if (!VerifyPasswordHash(request.password, user.passwordHash, user.passwordSalt)) { return BadRequest("Wrong UserName or Password"); };
            Token token = TokenHandler.CreateToken(_configuration, request.userName);
            return Ok(new { message = "Login Successfully", token = token });  
        }
    }
}
