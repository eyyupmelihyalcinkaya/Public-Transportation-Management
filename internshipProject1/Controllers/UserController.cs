using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using internshipProject1.Data;
using internshipProject1.DTOs;
using internshipProject1.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using internshipProject1.Security;
namespace internshipProject1.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase

    {
       private readonly AppDbContext _dbContext;
       private readonly IConfiguration _configuration;

        public UserController(AppDbContext dbContext, IConfiguration configuration) {

            _dbContext = dbContext;
            _configuration = configuration;
            
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

            CreatePasswordHash(request.password, out var hash, out var salt);

            var user = new User { 
                userName = request.userName,
                passwordHash = hash,
                passwordSalt = salt
            };
            if (_dbContext.Users.Any(u => u.userName == request.userName)) {
                return BadRequest("UserName Already Used !");
            }
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
            return Ok(
                new UserResponseDTO
                {
                    Id = user.Id,
                    userName = user.userName
                }
            );
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
