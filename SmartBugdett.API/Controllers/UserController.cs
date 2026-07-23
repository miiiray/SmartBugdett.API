using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.API.Common;
using SmartBudgett.Business.Abstract.Services;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<User>>> GetById(int id)
        {
            try
            {
                var value = await _userService.GetByIdAsync(id);

                if (value == null)
                {
                    return NotFound(ApiResponse<User>.Error("Kullanıcı bulunamadı", $"ID: {id} olan kullanıcı bulunamadı"));
                }

                return Ok(ApiResponse<User>.Ok(value, "Kullanıcı başarıyla alındı"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<User>.Error("Kullanıcı alırken hata oluştu", ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<User>>> Update(int id, User user)
        {
            try
            {
                if (id != user.Id)
                {
                    return BadRequest(ApiResponse<User>.Error("ID uyuşmuyor", "Route'daki ID ile DTO'daki ID uyuşmıyor"));
                }

                var existingUser = await _userService.GetByIdAsync(id);

                if (existingUser == null)
                {
                    return NotFound(ApiResponse<User>.Error("Kullanıcı bulunamadı", $"ID: {id} olan kullanıcı bulunamadı"));
                }

                await _userService.UpdateAsync(user);
                return Ok(ApiResponse<User>.Ok(user, "Kullanıcı başarıyla güncellendi"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<User>.Error("Kullanıcı güncellenirken hata oluştu", ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);

                if (user == null)
                {
                    return NotFound(ApiResponse.Error("Kullanıcı bulunamadı", $"ID: {id} olan kullanıcı bulunamadı"));
                }

                await _userService.DeleteAsync(user);
                return Ok(ApiResponse.Ok("Kullanıcı başarıyla silindi"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Error("Kullanıcı silinirken hata oluştu", ex.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<User>>>> GetAll()
        {
            try
            {
                var values = await _userService.GetAllAsync();
                return Ok(ApiResponse<List<User>>.Ok(values, "Kullanıcılar başarıyla alındı"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<List<User>>.Error("Kullanıcılar alınırken hata oluştu", ex.Message));
            }
        }
    }
}
