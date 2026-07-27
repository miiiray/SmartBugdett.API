using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBudgett.API.Common;
using SmartBudgett.Business.Abstract;
using SmartBudgett.DTO.Categories;
using SmartBudgett.Entities;
using System.Security.Claims;

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
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var categories = await _categoryService.GetAllAsync();

            var userCategories = categories
                .Where(x => x.UserId == userId)
                .ToList();

            return Ok(_mapper.Map<List<CategoryResponseDto>>(userCategories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            if (category.UserId != userId)
                return Forbid();

            return Ok(_mapper.Map<CategoryResponseDto>(category));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto dto)
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var category = _mapper.Map<Category>(dto);

            category.UserId = userId;

            await _categoryService.AddAsync(category);

            return Ok(_mapper.Map<CategoryResponseDto>(category));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryUpdateDto dto)
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            if (category.UserId != userId)
                return Forbid();

            category.Name = dto.Name;

            await _categoryService.UpdateAsync(category);

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            if (category.UserId != userId)
                return Forbid();

            await _categoryService.DeleteAsync(category);

            return NoContent();
        }
    }
}