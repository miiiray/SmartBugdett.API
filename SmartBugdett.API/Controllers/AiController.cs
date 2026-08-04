using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SmartBudgett.API.Extensions;
using SmartBudgett.Business.Abstract.Services;

namespace SmartBudgett.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AiController : ControllerBase
    {
        private readonly IAiService _aiService;

        public AiController(IAiService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("budget-analysis")]
        [EnableRateLimiting("ai-analysis")]
        public async Task<IActionResult> AnalyzeBudget(CancellationToken cancellationToken)
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var result = await _aiService.AnalyzeBudgetAsync(
                userId,
                cancellationToken);

            return Ok(result);
        }
    }
}
