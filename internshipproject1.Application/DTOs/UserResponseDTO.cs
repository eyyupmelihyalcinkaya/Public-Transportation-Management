using internshipproject1.Domain.Enums;

namespace internshipproject1.Application.DTOs
{
    public class UserResponseDTO
    {
        public int Id { get; set; } 
        public string userName { get; set; }

    }

    public class UserLoginResponseDTO
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public UserRole Role { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public string Message { get; set; } = "Login Successfully";
    }
}
