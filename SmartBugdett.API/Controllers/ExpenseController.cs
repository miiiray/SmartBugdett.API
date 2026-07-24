using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.API.Common;
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
        private readonly IMapper _mapper;

        public ExpenseController(IExpenseService expenseService, IMapper mapper)
        {
            _expenseService = expenseService;
            _mapper = mapper;
        }

        
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ExpenseResponseDto>>>> GetAll()
        {
            var values = await _expenseService.GetAllAsync();
            var responseValues = _mapper.Map<List<ExpenseResponseDto>>(values);

            return Ok(ApiResponse<List<ExpenseResponseDto>>
                .Ok(responseValues, "Giderler başarıyla alındı"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ExpenseResponseDto>>> GetById(int id)
        {
            try
            {
                var value = await _expenseService.GetByIdAsync(id);
                if (value == null)
                {
                    return NotFound(ApiResponse<ExpenseResponseDto>.Error("Gider bulunamadı", $"ID: {id} olan gider bulunamadı"));
                }

                var responseValue = _mapper.Map<ExpenseResponseDto>(value);
                return Ok(ApiResponse<ExpenseResponseDto>.Ok(responseValue, "Gider başarıyla alındı"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<ExpenseResponseDto>.Error("Gider alınırken hata oluştu", ex.Message));
            }
        }


        [HttpPost]
        public async Task<ActionResult<ApiResponse<ExpenseResponseDto>>> Add(ExpenseCreateDto expenseDto)
        {
            var expense = _mapper.Map<Expense>(expenseDto);

            await _expenseService.AddAsync(expense);

            var result = _mapper.Map<ExpenseResponseDto>(expense);

            return CreatedAtAction(nameof(GetById), new { id = expense.Id },
                ApiResponse<ExpenseResponseDto>.Ok(result, "Gider başarıyla eklendi"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ExpenseResponseDto>>> Update(int id, ExpenseUpdateDto expenseDto)
        {
            try
            {
                if (id != expenseDto.Id)
                {
                    return BadRequest(ApiResponse<ExpenseResponseDto>.Error("ID uyuşmuyor", "Route'daki ID ile DTO'daki ID uyuşmıyor"));
                }

                var existingExpense = await _expenseService.GetByIdAsync(id);
                if (existingExpense == null)
                {
                    return NotFound(ApiResponse<ExpenseResponseDto>.Error("Gider bulunamadı", $"ID: {id} olan gider bulunamadı"));
                }

                _mapper.Map(expenseDto, existingExpense);
                await _expenseService.UpdateAsync(existingExpense);
                var result = _mapper.Map<ExpenseResponseDto>(existingExpense);

                return Ok(ApiResponse<ExpenseResponseDto>.Ok(result, "Gider başarıyla güncellendi"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<ExpenseResponseDto>.Error("Gider güncellenirken hata oluştu", ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            try
            {
                var existingExpense = await _expenseService.GetByIdAsync(id);
                if (existingExpense == null)
                {
                    return NotFound(ApiResponse.Error("Gider bulunamadı", $"ID: {id} olan gider bulunamadı"));
                }

                await _expenseService.DeleteAsync(existingExpense);
                return Ok(ApiResponse.Ok("Gider başarıyla silindi"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Error("Gider silinirken hata oluştu", ex.Message));
            }
        }
    }
}