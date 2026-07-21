using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.Business.Abstract;
using SmartBudgett.Entities;

namespace SmartBudgett.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _userService.GetById(id);

            if (value == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            return Ok(value);
        }
        [HttpPost]
        public IActionResult Add(User user)
        {
            _userService.Add(user);
            return CreatedAtAction(
                nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest("Id uyuşmuyor.");
            }

            var existingUser = _userService.GetById(id);

            if (existingUser == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            _userService.Update(user);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            _userService.Delete(user);

            return NoContent();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var values = _userService.GetAll();
            return Ok(values);
        }
    } 
}
