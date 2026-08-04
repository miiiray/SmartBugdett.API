using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.API.Common;
using SmartBudgett.Business.Abstract;
using SmartBudgett.Core.Security.Abstract;
using SmartBudgett.Core.Security.Hashing;
using SmartBudgett.DTO.Auth;
using SmartBudgett.Entities;

namespace SmartBudgett.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly int _accessTokenExpirationMinutes;

        public AuthController(
            IUserService userService,
            ITokenHelper tokenHelper,
            IConfiguration configuration)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _accessTokenExpirationMinutes = int.TryParse(
                configuration["Jwt:AccessTokenExpirationMinutes"],
                out var configuredExpirationMinutes)
                ? configuredExpirationMinutes
                : 120;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse>> Register(UserRegisterDto dto)
        {
            var normalizedEmail = dto.Email.Trim().ToLowerInvariant();
            var existingUser = await _userService.GetByEmailAsync(normalizedEmail);

            if (existingUser is not null)
            {
                return Conflict(ApiResponse.Error(
                    "Kayıt başarısız",
                    "Bu e-posta adresiyle zaten kayıtlı bir kullanıcı var."));
            }

            var now = DateTime.UtcNow;
            var user = new User
            {
                FirstName = dto.FirstName.Trim(),
                LastName = dto.LastName.Trim(),
                Email = normalizedEmail,
                Password = PasswordHasher.CreatePasswordHash(dto.Password),
                CreatedDate = now,
                UpdatedDate = now
            };

            await _userService.AddAsync(user);

            return StatusCode(
                StatusCodes.Status201Created,
                ApiResponse.Ok("Kullanıcı kaydı başarıyla oluşturuldu."));
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<object>>> Login(UserLoginDto dto)
        {
            var normalizedEmail = dto.Email.Trim().ToLowerInvariant();
            var user = await _userService.GetByEmailAsync(normalizedEmail);

            if (user is null || !IsPasswordValid(dto.Password, user.Password))
            {
                return Unauthorized(ApiResponse<object>.Error(
                    "Giriş başarısız",
                    "E-posta veya şifre hatalı."));
            }

            var token = _tokenHelper.CreateToken(user);
            var response = new
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(_accessTokenExpirationMinutes),
                User = new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email
                }
            };

            return Ok(ApiResponse<object>.Ok(response, "Giriş başarılı."));
        }

        private static bool IsPasswordValid(string password, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                return false;

            try
            {
                return PasswordHasher.VerifyPasswordHash(password, passwordHash);
            }
            catch
            {
                return false;
            }
        }
    }
}
