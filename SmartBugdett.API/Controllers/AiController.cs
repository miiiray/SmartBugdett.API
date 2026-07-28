using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.Business.Abstract;
using SmartBudgett.Business.Abstract.Services;
using System.Security.Claims;


namespace SmartBudgett.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly IAiService _aiService;
        public AiController(IAiService aiService)
        {
            _aiService = aiService;
        }
       
        [Authorize]
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var userIdText = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdText, out var userId))
            {
                return Unauthorized();
            }

   

            var result = await _aiService.AnalyzeBudgetAsync(userId);

            return Ok(result);


        }
    }
}