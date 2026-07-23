using Microsoft.AspNetCore.Mvc;
using SmartBudgett.Business.Abstract;
using SmartBudgett.DTO;
using System;
using System.Linq;

namespace SmartBudgett.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthController(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto userLoginDto)
        {
            var users = _userService.GetAll();

            if (users == null || !users.Any())
            {
                return BadRequest("Veri tabanından hiçbir kullanıcı verisi çekilemedi.");
            }

            // 1. E-postayı boşluklardan arındırıp küçük harfe çevirerek ara
            var inputEmail = userLoginDto.Email?.Trim().ToLower();
            var inputPassword = userLoginDto.Password?.Trim();

            var user = users.FirstOrDefault(u => u.Email != null && u.Email.Trim().ToLower() == inputEmail);

            if (user == null)
            {
                return BadRequest($"'{userLoginDto.Email}' adresiyle kayıtlı kullanıcı bulunamadı.");
            }

            // 2. Veri tabanından şifre alanı null geldiyse uyar
            if (string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Veri tabanındaki kullanıcı kaydında şifre alanı boş (null) dönüyor. EF Core haritalamasını kontrol edin.");
            }

            // 3. Şifre kontrolü
            if (user.Password.Trim() != inputPassword)
            {
                return BadRequest("Şifre eşleşmiyor.");
            }

            // 4. Token Üretimi
            try
            {
                var token = _tokenHelper.CreateToken(user);
                return Ok(new
                {
                    Token = token,
                    Expiration = DateTime.Now.AddHours(2),
                    Message = "Giriş başarılı"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Token üretilirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}