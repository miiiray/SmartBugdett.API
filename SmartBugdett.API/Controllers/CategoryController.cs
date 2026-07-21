using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.Business.Abstract;
using SmartBudgett.Entities;

namespace SmartBudgett.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
                var values = _categoryService.GetAll();
                return Ok(values);
            
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _categoryService.GetById(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }
        [HttpPost]
        public IActionResult Add(Category category)
        {
            _categoryService.Add(category);
            return Ok("Kategori başarıyla eklendi");
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest("ıd uyuşmuyor");
            }
            var existingCategory = _categoryService.GetById(id);

            if (existingCategory == null)
            {
                return NotFound("Kategori bulunamadı");
            }
            _categoryService.Update(category);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound("Kategori bulunamadı");
            }
            _categoryService.Delete(category);
            return NoContent();
        }

    }
}