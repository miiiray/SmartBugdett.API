using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.Business.Abstract;
using SmartBudgett.Entities;

namespace SmartBudgett.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet]
        public IActionResult Getall()
        {
            var values = _incomeService.GetAll();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _incomeService.GetById(id);
            if (value == null)
            {
                return NotFound("Gelir bulunmadı");
            }
            return Ok(value);
        }
        [HttpPost]
        public IActionResult Add(Income income)
        {
            _incomeService.Add(income);
            return Ok("Gelir başarıyla eklendi");
        }
        [HttpPut ("{id}")]
        public IActionResult Update(int id, Income income)
        {
            if (id != income.Id)
            {
                return BadRequest("Id uyuşmuyor");
            }
            var existingIncome = _incomeService.GetById(id);
            if (existingIncome == null)
            {
                return NotFound("Gelir bulunamadı");
            }
            _incomeService.Update(income);
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