using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.API.Common;
using SmartBudgett.API.Extensions;
using SmartBudgett.Business.Abstract;
using SmartBudgett.DTO.Users;
using SmartBudgett.Entities;

namespace SmartBudgett.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> GetCurrentUser()
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var user = await _userService.GetByIdAsync(userId);

            if (user is null)
                return NotFound(ApiResponse<UserResponseDto>.Error("Kullanıcı bulunamadı"));

            return Ok(ApiResponse<UserResponseDto>.Ok(
                ToResponseDto(user),
                "Kullanıcı başarıyla alındı"));
        }

        [HttpPut("me")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> UpdateCurrentUser(UserUpdateDto dto)
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var user = await _userService.GetByIdAsync(userId);

            if (user is null)
                return NotFound(ApiResponse<UserResponseDto>.Error("Kullanıcı bulunamadı"));

            var normalizedEmail = dto.Email.Trim().ToLowerInvariant();
            var emailOwner = await _userService.GetByEmailAsync(normalizedEmail);

            if (emailOwner is not null && emailOwner.Id != userId)
            {
                return Conflict(ApiResponse<UserResponseDto>.Error(
                    "Güncelleme başarısız",
                    "Bu e-posta adresi başka bir kullanıcı tarafından kullanılıyor."));
            }

            user.FirstName = dto.FirstName.Trim();
            user.LastName = dto.LastName.Trim();
            user.Email = normalizedEmail;
            user.UpdatedDate = DateTime.UtcNow;

            await _userService.UpdateAsync(user);

            return Ok(ApiResponse<UserResponseDto>.Ok(
                ToResponseDto(user),
                "Kullanıcı başarıyla güncellendi"));
        }

        [HttpDelete("me")]
        public async Task<ActionResult<ApiResponse>> DeleteCurrentUser()
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var user = await _userService.GetByIdAsync(userId);

            if (user is null)
                return NotFound(ApiResponse.Error("Kullanıcı bulunamadı"));

            await _userService.DeleteAsync(user);
            return Ok(ApiResponse.Ok("Kullanıcı başarıyla silindi"));
        }

        private static UserResponseDto ToResponseDto(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate
            };
        }
    }
}
