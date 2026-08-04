using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.API.Common;
using SmartBudgett.API.Extensions;
using SmartBudgett.Business.Abstract;
using SmartBudgett.Business.Abstract.Services;
using SmartBudgett.DTO.Expenses;
using SmartBudgett.Entities;

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
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var expenses = await _expenseService.GetByUserIdAsync(userId);

            return Ok(
                _mapper.Map<List<ExpenseResponseDto>>(expenses)
            );
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var expense = await _expenseService.GetByIdAsync(id);

            if (expense == null || expense.UserId != userId)
                return NotFound("Harcama bulunamadı.");

            return Ok(_mapper.Map<ExpenseResponseDto>(expense));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseCreateDto dto)
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var category =
                await _categoryService.GetByIdAsync(dto.CategoryId);

            if (category == null || category.UserId != userId)
                return BadRequest("Kategori bulunamadı.");

            var expense = _mapper.Map<Expense>(dto);

            expense.Description = dto.Description.Trim();
            expense.UserId = userId;
            expense.CreatedDate = DateTime.UtcNow;
            expense.UpdatedDate = expense.CreatedDate;

            await _expenseService.AddAsync(expense);

            return Ok(_mapper.Map<ExpenseResponseDto>(expense));
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, ExpenseUpdateDto dto)
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var expense = await _expenseService.GetByIdAsync(id);

            if (expense == null || expense.UserId != userId)
                return NotFound("Harcama bulunamadı.");

            var category =
                await _categoryService.GetByIdAsync(dto.CategoryId);

            if (category == null || category.UserId != userId)
                return BadRequest("Kategori bulunamadı.");

            expense.Amount = dto.Amount;
            expense.Description = dto.Description.Trim();
            expense.ExpenseDate = dto.ExpenseDate;
            expense.CategoryId = dto.CategoryId;
            expense.UpdatedDate = DateTime.UtcNow;

            await _expenseService.UpdateAsync(expense);

            var result = _mapper.Map<ExpenseResponseDto>(expense);

            return Ok(
                ApiResponse<ExpenseResponseDto>.Ok(
                    result,
                    "Gider başarıyla güncellendi"));
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var expense = await _expenseService.GetByIdAsync(id);

            if (expense == null || expense.UserId != userId)
                return NotFound("Harcama bulunamadı.");

            await _expenseService.DeleteAsync(expense);

            return NoContent();
        }
    }
}
