using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.API.Common;
using SmartBudgett.API.Extensions;
using SmartBudgett.Business.Abstract;
using SmartBudgett.DTO.Incomes;
using SmartBudgett.Entities;

namespace SmartBudgett.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public IncomeController(
            IIncomeService incomeService,
            ICategoryService categoryService,
            IMapper mapper)
        {
            _incomeService = incomeService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<IncomeResponseDto>>>> GetAll()
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var incomes = await _incomeService.GetByUserIdAsync(userId);
            var response = _mapper.Map<List<IncomeResponseDto>>(incomes);

            return Ok(ApiResponse<List<IncomeResponseDto>>.Ok(
                response,
                "Gelirler başarıyla alındı"));
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<ApiResponse<IncomeResponseDto>>> GetById(int id)
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var income = await _incomeService.GetByIdAsync(id);

            if (income is null || income.UserId != userId)
            {
                return NotFound(ApiResponse<IncomeResponseDto>.Error(
                    "Gelir bulunamadı"));
            }

            return Ok(ApiResponse<IncomeResponseDto>.Ok(
                _mapper.Map<IncomeResponseDto>(income),
                "Gelir başarıyla alındı"));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<IncomeResponseDto>>> Add(IncomeCreateDto dto)
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var category = await _categoryService.GetByIdAsync(dto.CategoryId);

            if (category is null || category.UserId != userId)
                return NotFound(ApiResponse<IncomeResponseDto>.Error("Kategori bulunamadı"));

            var now = DateTime.UtcNow;
            var income = _mapper.Map<Income>(dto);
            income.Description = dto.Description.Trim();
            income.UserId = userId;
            income.IncomeDate = now;
            income.CreatedDate = now;
            income.UpdatedDate = now;

            await _incomeService.AddAsync(income);

            var result = _mapper.Map<IncomeResponseDto>(income);

            return CreatedAtAction(
                nameof(GetById),
                new { id = income.Id },
                ApiResponse<IncomeResponseDto>.Ok(result, "Gelir başarıyla eklendi"));
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<ApiResponse<IncomeResponseDto>>> Update(
            int id,
            IncomeUpdateDto dto)
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var income = await _incomeService.GetByIdAsync(id);

            if (income is null || income.UserId != userId)
                return NotFound(ApiResponse<IncomeResponseDto>.Error("Gelir bulunamadı"));

            var category = await _categoryService.GetByIdAsync(dto.CategoryId);

            if (category is null || category.UserId != userId)
                return NotFound(ApiResponse<IncomeResponseDto>.Error("Kategori bulunamadı"));

            income.Amount = dto.Amount;
            income.Description = dto.Description.Trim();
            income.IncomeDate = dto.IncomeDate;
            income.CategoryId = dto.CategoryId;
            income.UpdatedDate = DateTime.UtcNow;

            await _incomeService.UpdateAsync(income);

            return Ok(ApiResponse<IncomeResponseDto>.Ok(
                _mapper.Map<IncomeResponseDto>(income),
                "Gelir başarıyla güncellendi"));
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var income = await _incomeService.GetByIdAsync(id);

            if (income is null || income.UserId != userId)
                return NotFound(ApiResponse.Error("Gelir bulunamadı"));

            await _incomeService.DeleteAsync(income);
            return Ok(ApiResponse.Ok("Gelir başarıyla silindi"));
        }
    }
}
