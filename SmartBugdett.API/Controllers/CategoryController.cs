using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.API.Common;
using SmartBudgett.API.Extensions;
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
        public async Task<IActionResult> GetAll()
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var categories = await _categoryService.GetByUserIdAsync(userId);

            return Ok(_mapper.Map<List<CategoryResponseDto>>(categories));
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var category = await _categoryService.GetByIdAsync(id);

            if (category == null || category.UserId != userId)
                return NotFound();

            return Ok(_mapper.Map<CategoryResponseDto>(category));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto dto)
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var category = _mapper.Map<Category>(dto);

            category.Name = dto.Name.Trim();
            category.UserId = userId;
            category.CreatedDate = DateTime.UtcNow;
            category.UpdatedDate = category.CreatedDate;

            await _categoryService.AddAsync(category);

            return Ok(_mapper.Map<CategoryResponseDto>(category));
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, CategoryUpdateDto dto)
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var category = await _categoryService.GetByIdAsync(id);

            if (category == null || category.UserId != userId)
                return NotFound();

            category.Name = dto.Name.Trim();
            category.UpdatedDate = DateTime.UtcNow;

            await _categoryService.UpdateAsync(category);

            return NoContent();
        }

        
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.TryGetUserId(out var userId))
                return Unauthorized();

            var category = await _categoryService.GetByIdAsync(id);

            if (category == null || category.UserId != userId)
                return NotFound();

            await _categoryService.DeleteAsync(category);

            return NoContent();
        }
    }
}
