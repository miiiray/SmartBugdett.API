using System;

namespace SmartBudgett.DTO.Auth
{
    public class AccessToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}