using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.Business.Abstract;
using SmartBudgett.DTO;
using SmartBudgett.Entities;

namespace SmartBudgett.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IActionResult GetAll()
        {
            var values = _expenseService.GetAll();
            
            var responseValues = _mapper.Map<List<ExpenseResponseDto>>(values);

            return Ok(responseValues);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _expenseService.GetById(id);
            if (value == null)
            {
                return NotFound();
            }
        
            var responseValue = _mapper.Map<ExpenseResponseDto>(value);

            return Ok(responseValue);
        }

        [HttpPost]
        public IActionResult Add(ExpenseCreateDto expenseDto)
        {
          
            var expense = _mapper.Map<Expense>(expenseDto);

            _expenseService.Add(expense);
            return Ok("Gider başarıyla eklendi");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ExpenseCreateDto expenseDto) 
        {
            var existingExpense = _expenseService.GetById(id);
            if (existingExpense == null)
            {
                return NotFound("Gider bulunamadı");
            }

       
            _mapper.Map(expenseDto, existingExpense);

            _expenseService.Update(existingExpense);
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