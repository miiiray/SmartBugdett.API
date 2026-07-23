using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.API.Common;
using SmartBudgett.Business.Abstract.Services;
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
        private readonly IMapper _mapper;

        public IncomeController(IIncomeService incomeService, IMapper mapper)
        {
            _incomeService = incomeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<IncomeResponseDto>>>> GetAll()
        {
            try
            {
                var values = await _incomeService.GetAllAsync();
                var response = _mapper.Map<List<IncomeResponseDto>>(values);
                return Ok(ApiResponse<List<IncomeResponseDto>>.Ok(response, "Gelirler başarıyla alınldı"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<List<IncomeResponseDto>>.Error("Gelirler alınırken hata oluştu", ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<IncomeResponseDto>>> GetById(int id)
        {
            try
            {
                var value = await _incomeService.GetByIdAsync(id);
                if (value == null)
                {
                    return NotFound(ApiResponse<IncomeResponseDto>.Error("Gelir bulunamadı", $"ID: {id} olan gelir bulunamadı"));
                }

                var response = _mapper.Map<IncomeResponseDto>(value);
                return Ok(ApiResponse<IncomeResponseDto>.Ok(response, "Gelir başarıyla alınldı"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<IncomeResponseDto>.Error("Gelir alınırken hata oluştu", ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<IncomeResponseDto>>> Add(IncomeCreateDto incomeCreateDto)
        {
            try
            {
                var income = _mapper.Map<Income>(incomeCreateDto);
                await _incomeService.AddAsync(income);
                var result = _mapper.Map<IncomeResponseDto>(income);
                return CreatedAtAction(nameof(GetById), new { id = income.Id }, 
                    ApiResponse<IncomeResponseDto>.Ok(result, "Gelir başarıyla eklendi"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<IncomeResponseDto>.Error("Gelir eklenirken hata oluştu", ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<IncomeResponseDto>>> Update(int id, IncomeUpdateDto incomeUpdateDto)
        {
            try
            {
                if (id != incomeUpdateDto.Id)
                {
                    return BadRequest(ApiResponse<IncomeResponseDto>.Error("ID uyuşmuyor", "Route'daki ID ile DTO'daki ID uyuşmıyor"));
                }

                var existingIncome = await _incomeService.GetByIdAsync(id);
                if (existingIncome == null)
                {
                    return NotFound(ApiResponse<IncomeResponseDto>.Error("Gelir bulunamadı", $"ID: {id} olan gelir bulunamadı"));
                }

                _mapper.Map(incomeUpdateDto, existingIncome);
                await _incomeService.UpdateAsync(existingIncome);
                var result = _mapper.Map<IncomeResponseDto>(existingIncome);

                return Ok(ApiResponse<IncomeResponseDto>.Ok(result, "Gelir başarıyla güncellendi"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<IncomeResponseDto>.Error("Gelir güncellenirken hata oluştu", ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            try
            {
                var income = await _incomeService.GetByIdAsync(id);
                if (income == null)
                {
                    return NotFound(ApiResponse.Error("Gelir bulunamadı", $"ID: {id} olan gelir bulunamadı"));
                }

                await _incomeService.DeleteAsync(income);
                return Ok(ApiResponse.Ok("Gelir başarıyla silindi"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Error("Gelir silinirken hata oluştu", ex.Message));
            }
        }
    }
}