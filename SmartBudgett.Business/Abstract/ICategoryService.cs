using SmartBudgett.Entities;

namespace SmartBudgett.Business.Abstract
{
    public interface ICategoryService
    {
        
        Category? GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);

        Task<List<Category>> GetByUserIdAsync(int userId);
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
