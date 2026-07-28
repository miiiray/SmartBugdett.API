using System;
using BCrypt.Net;

namespace SmartBudgett.Core.Security.Hashing
{
    public static class PasswordHasher
    {
        
        public static string CreatePasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        
        public static bool VerifyPasswordHash(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}