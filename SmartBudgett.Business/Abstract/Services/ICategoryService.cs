using SmartBudgett.Entities;

namespace SmartBudgett.Business.Abstract.Services
{
    public interface ICategoryService
    {
        // Sync methods
        List<Category> GetAll();
        Category GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);

        // Async methods
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
