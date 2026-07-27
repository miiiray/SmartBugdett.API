using System;
using BCrypt.Net;

namespace SmartBudgett.Core.Security.Hashing
{
    public static class PasswordHasher
    {
        // Şifreyi şifrelenmiş (hashlenmiş) metne çevirir
        public static string CreatePasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Girilen şifre ile veritabanındaki hashlenmiş şifreyi karşılaştırır
        public static bool VerifyPasswordHash(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}