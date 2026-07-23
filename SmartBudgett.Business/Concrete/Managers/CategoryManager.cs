using SmartBudgett.Business.Abstract.Services;
using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.Entities;

namespace SmartBudgett.Business.Concrete.Managers
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // Sync methods
        public void Add(Category category)
        {
            ValidateCategory(category);
            _categoryRepository.Add(category);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetById(int id)
        {
            if (id <= 0)
                throw new Exception("Invalid category ID.");

            return _categoryRepository.GetById(id);
        }

        public void Update(Category category)
        {
            ValidateCategory(category);
            _categoryRepository.Update(category);
        }

        public void Delete(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _categoryRepository.Delete(category);
        }

        // Async methods
        public async Task AddAsync(Category category)
        {
            ValidateCategory(category);
            await _categoryRepository.AddAsync(category);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new Exception("Invalid category ID.");

            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Category category)
        {
            ValidateCategory(category);
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            await _categoryRepository.DeleteAsync(category);
        }

        // Helper validation method
        private void ValidateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            if (string.IsNullOrWhiteSpace(category.Name))
                throw new Exception("Category name cannot be empty.");

            if (category.UserId <= 0)
                throw new Exception("Category must belong to a valid user.");
        }
    }
}
