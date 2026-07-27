using Microsoft.AspNetCore.Mvc;
using SmartBudgett.Business.Abstract.Services;

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

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var result = await _aiService.AnalyzeBudgetAsync(1);

            return Ok(result);
        }
    }
}