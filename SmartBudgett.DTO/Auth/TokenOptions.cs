// TokenOptions.cs
namespace SmartBudgett.DTO.Auth
{
    public class TokenOptions
    {
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; } = string.Empty;
    }
}

// AccessToken.cs


namespace SmartBudgett.DTO.Auth
{
    public class AccessTokenDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}