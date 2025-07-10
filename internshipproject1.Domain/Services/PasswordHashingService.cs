using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Domain.Services;

namespace internshipproject1.Domain.Services
{
    public class PasswordHashingService : IPasswordHashingService
    {
        public void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        { 
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        public bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        { 
            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(hash);
        }
    }
}
