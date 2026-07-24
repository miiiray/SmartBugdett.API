using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.API.Common;
using SmartBudgett.Business.Abstract;
using SmartBudgett.Core.Security.Abstract;
using SmartBudgett.DTO.Auth;
using SmartBudgett.Entities;
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

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse>> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            try
            {
                var users = await _userService.GetAllAsync();

                var inputEmail = userRegisterDto.Email?.Trim().ToLower();

                if (users != null && users.Any(u => u.Email != null && u.Email.Trim().ToLower() == inputEmail))
                {
                    return BadRequest(ApiResponse.Error("Kayıt başarısız", $"'{userRegisterDto.Email}' adresiyle zaten kayıtlı bir kullanıcı var."));
                }

                // BCrypt ile şifreyi güvenli şekilde hash'le
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password);

                var newUser = new User
                {
                    FirstName = userRegisterDto.FirstName,
                    LastName = userRegisterDto.LastName,
                    Email = userRegisterDto.Email?.Trim(),
                    Password = passwordHash
                };

                await _userService.AddAsync(newUser);

                return Ok(ApiResponse.Ok("Kullanıcı kaydı başarıyla oluşturuldu."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Error("Kayıt sırasında hata oluştu", ex.Message));
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<object>>> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var users = await _userService.GetAllAsync();

                if (users == null || string.IsNullOrEmpty(users.FirstOrDefault()?.Email))
                {
                    return BadRequest(ApiResponse<object>.Error("Giriş başarısız", "Veri tabanından hiçbir kullanıcı verisi çekilemedi."));
                }

                var inputEmail = userLoginDto.Email?.Trim().ToLower();
                var inputPassword = userLoginDto.Password?.Trim();

                var user = users.FirstOrDefault(u => u.Email != null && u.Email.Trim().ToLower() == inputEmail);

                if (user == null)
                {
                    return BadRequest(ApiResponse<object>.Error("Giriş başarısız", $"'{userLoginDto.Email}' adresiyle kayıtlı kullanıcı bulunamadı."));
                }

                if (string.IsNullOrEmpty(user.Password))
                {
                    return BadRequest(ApiResponse<object>.Error("Giriş başarısız", "Veri tabanındaki kullanıcı kaydında şifre alanı boş."));
                }

                // BCrypt ile şifre doğrulaması
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(inputPassword, user.Password);

                if (!isPasswordValid)
                {
                    return BadRequest(ApiResponse<object>.Error("Giriş başarısız", "E-posta veya şifre hatalı."));
                }

                // Token üretimi
                try
                {
                    var token = _tokenHelper.CreateToken(user);
                    var response = new
                    {
                        Token = token,
                        Expiration = DateTime.Now.AddHours(2),
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
                catch (Exception ex)
                {
                    return StatusCode(500, ApiResponse<object>.Error("Token üretilirken hata oluştu", ex.Message));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.Error("Giriş sırasında hata oluştu", ex.Message));
            }
        }
    }
}
