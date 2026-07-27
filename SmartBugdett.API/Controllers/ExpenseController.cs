using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.API.Common;
using SmartBudgett.Business.Abstract;
using SmartBudgett.Business.Abstract.Services;
using SmartBudgett.DTO.Expenses;
using SmartBudgett.Entities;
using System.Security.Claims;

namespace SmartBudgett.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ExpenseController(IExpenseService expenseService,
            ICategoryService categoryService,IMapper mapper)
        {
            _expenseService = expenseService;
            _categoryService= categoryService;
            _mapper = mapper;
        }

        
       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userIdValue =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var expenses = await _expenseService.GetAllAsync();

            var userExpenses = expenses
                .Where(x => x.UserId == userId)
                .ToList();

            return Ok(
                _mapper.Map<List<ExpenseResponseDto>>(userExpenses)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userIdValue =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var expense = await _expenseService.GetByIdAsync(id);

            if (expense == null)
                return NotFound("Harcama bulunamadı.");

            if (expense.UserId != userId)
                return Forbid();

            return Ok(_mapper.Map<ExpenseResponseDto>(expense));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseCreateDto dto)
        {
            var userIdValue =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var category =
                await _categoryService.GetByIdAsync(dto.CategoryId);

            if (category == null)
                return BadRequest("Kategori bulunamadı.");

            if (category.UserId != userId)
                return Forbid();

            var expense = _mapper.Map<Expense>(dto);

            expense.UserId = userId;

            await _expenseService.AddAsync(expense);

            return Ok(_mapper.Map<ExpenseResponseDto>(expense));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ExpenseUpdateDto dto)
        {
            var userIdValue =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var expense = await _expenseService.GetByIdAsync(id);

            if (expense == null)
                return NotFound("Harcama bulunamadı.");

            if (expense.UserId != userId)
                return Forbid();

            var category =
                await _categoryService.GetByIdAsync(dto.CategoryId);

            if (category == null)
                return BadRequest("Kategori bulunamadı.");

            if (category.UserId != userId)
                return Forbid();

            expense.Amount = dto.Amount;
            expense.Description = dto.Description;
            expense.ExpenseDate = dto.ExpenseDate;
            expense.CategoryId = dto.CategoryId;

            await _expenseService.UpdateAsync(expense);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userIdValue =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var expense = await _expenseService.GetByIdAsync(id);

            if (expense == null)
                return NotFound("Harcama bulunamadı.");

            if (expense.UserId != userId)
                return Forbid();

            await _expenseService.DeleteAsync(expense);

            return NoContent();
        }
    }
}