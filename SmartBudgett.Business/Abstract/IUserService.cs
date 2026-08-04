using SmartBudgett.Entities;

namespace SmartBudgett.Business.Abstract
{
    public interface IUserService
    {
       
        User? GetById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(User user);

        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
