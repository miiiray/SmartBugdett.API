using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.Business.Abstract;
using SmartBudgett.DTO;
using SmartBudgett.DTO.Categories;
using SmartBudgett.Entities;

namespace SmartBudgett.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper; 

        
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var values = _categoryService.GetAll();

            // 3. Sözdizimi hatası düzeltildi: List<CategoryResponseDto> olarak eşliyoruz
            var response = _mapper.Map<List<CategoryResponseDto>>(values);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _categoryService.GetById(id);
            if (value == null)
            {
                return NotFound("Kategori bulunamadı.");
            }

            // Dilersen tekil nesneyi de DTO'ya dönüştürüp dönebilirsin:
            var response = _mapper.Map<CategoryResponseDto>(value);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add(CategoryCreateDto categoryCreateDto)
        {
            // DTO -> Entity dönüşümü
            var category = _mapper.Map<Category>(categoryCreateDto);

            _categoryService.Add(category);
            return Ok("Kategori başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryResponseDto categoryResponseDto)
        {
            if (id != categoryResponseDto.Id)
            {
                return BadRequest("ID uyuşmuyor.");
            }

            var existingCategory = _categoryService.GetById(id);
            if (existingCategory == null)
            {
                return NotFound("Kategori bulunamadı.");
            }

            // DTO'daki yeni bilgileri var olan Entity üzerine eşliyoruz
            _mapper.Map(categoryResponseDto, existingCategory);

            _categoryService.Update(existingCategory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound("Kategori bulunamadı.");
            }

            _categoryService.Delete(category);
            return NoContent();
        }
    }
}
