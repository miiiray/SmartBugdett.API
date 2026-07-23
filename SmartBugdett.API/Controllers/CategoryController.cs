using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.API.Common;
using SmartBudgett.Business.Abstract;
using SmartBudgett.DTO.Categories;
using SmartBudgett.Entities;

namespace SmartBudgett.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<ApiResponse<List<CategoryResponseDto>>>> GetAll()
        {
            try
            {
                var values = await _categoryService.GetAllAsync();
                var response = _mapper.Map<List<CategoryResponseDto>>(values);
                return Ok(ApiResponse<List<CategoryResponseDto>>.Ok(response, "Kategoriler başarıyla alındı"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<List<CategoryResponseDto>>.Error("Kategoriler alınırken hata oluştu", ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CategoryResponseDto>>> GetById(int id)
        {
            try
            {
                var value = await _categoryService.GetByIdAsync(id);
                if (value == null)
                {
                    return NotFound(ApiResponse<CategoryResponseDto>.Error("Kategori bulunamadı", $"ID: {id} olan kategori bulunamadı"));
                }

                var response = _mapper.Map<CategoryResponseDto>(value);
                return Ok(ApiResponse<CategoryResponseDto>.Ok(response, "Kategori başarıyla alındı"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<CategoryResponseDto>.Error("Kategori alınırken hata oluştu", ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CategoryResponseDto>>> Add(CategoryCreateDto categoryCreateDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryCreateDto);
                await _categoryService.AddAsync(category);
                var result = _mapper.Map<CategoryResponseDto>(category);
                return CreatedAtAction(nameof(GetById), new { id = category.Id },
                    ApiResponse<CategoryResponseDto>.Ok(result, "Kategori başarıyla eklendi"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<CategoryResponseDto>.Error("Kategori eklenirken hata oluştu", ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<CategoryResponseDto>>> Update(int id, CategoryCreateDto categoryCreateDto)
        {
            try
            {
                var existingCategory = await _categoryService.GetByIdAsync(id);
                if (existingCategory == null)
                {
                    return NotFound(ApiResponse<CategoryResponseDto>.Error("Kategori bulunamadı", $"ID: {id} olan kategori bulunamadı"));
                }

                _mapper.Map(categoryCreateDto, existingCategory);
                await _categoryService.UpdateAsync(existingCategory);
                var result = _mapper.Map<CategoryResponseDto>(existingCategory);

                return Ok(ApiResponse<CategoryResponseDto>.Ok(result, "Kategori başarıyla güncellendi"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<CategoryResponseDto>.Error("Kategori güncellenirken hata oluştu", ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound(ApiResponse.Error("Kategori bulunamadı", $"ID: {id} olan kategori bulunamadı"));
                }

                await _categoryService.DeleteAsync(category);
                return Ok(ApiResponse.Ok("Kategori başarıyla silindi"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Error("Kategori silinirken hata oluştu", ex.Message));
            }
        }
    }
}
