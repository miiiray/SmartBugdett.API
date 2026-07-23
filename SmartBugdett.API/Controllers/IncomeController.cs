using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.Business.Abstract;
using SmartBudgett.DTO;
using SmartBudgett.Entities;


namespace SmartBudgett.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IActionResult GetAll()
        {
            var values = _incomeService.GetAll();
            var response = _mapper.Map<List<IncomeResponseDto>>(values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _incomeService.GetById(id);
            if (value == null)
            {
                return NotFound("Gelir bulunamadı");
            }

            var response = _mapper.Map<IncomeResponseDto>(value);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add(IncomeCreateDto incomeCreateDto)
        {
            var income = _mapper.Map<Income>(incomeCreateDto);
            _incomeService.Add(income);
            return Ok("Gelir başarıyla eklendi");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, IncomeUpdateDto incomeUpdateDto)
        {
            if (id != incomeUpdateDto.Id)
            {
                return BadRequest("Id uyuşmuyor");
            }

            var existingIncome = _incomeService.GetById(id);
            if (existingIncome == null)
            {
                return NotFound("Gelir bulunamadı");
            }

            // DTO'daki yeni bilgileri var olan Entity üzerine eşliyoruz
            _mapper.Map(incomeUpdateDto, existingIncome);
            _incomeService.Update(existingIncome);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var income = _incomeService.GetById(id);
            if (income == null)
            {
                return NotFound("Gelir bulunamadı");
            }
            _incomeService.Delete(income);
            return NoContent();
        }
    }
}