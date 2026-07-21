using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.Business.Abstract;
using SmartBudgett.Entities;

namespace SmartBudgett.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var values = _expenseService.GetAll();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _expenseService.GetById(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPost]
        public IActionResult Add(Expense expense)
        {
            _expenseService.Add(expense);
            return Ok("Gider başarıyla eklendi");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest("ıd uyuşmuyor");
            }
            var existingExpense = _expenseService.GetById(id);
            if (existingExpense == null)
            {
                return NotFound("Gider bulunamadı");
            }
            _expenseService.Update(expense);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingExpense = _expenseService.GetById(id);
            if (existingExpense == null)
            {
                return NotFound("Gider bulunamadı");
            }
            _expenseService.Delete(existingExpense);
            return NoContent();
        }
    }
}