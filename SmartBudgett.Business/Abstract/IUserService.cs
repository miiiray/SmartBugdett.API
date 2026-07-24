using SmartBudgett.Entities;

namespace SmartBudgett.Business.Abstract
{
    public interface IUserService
    {
        // Sync methods
        List<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(User user);

        // Async methods
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
