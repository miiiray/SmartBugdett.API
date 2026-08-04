using SmartBudgett.Entities;

namespace SmartBudgett.Business.Abstract.Services
{
    public interface IExpenseService
    {
        Expense? GetById(int id);
        void Add(Expense expense);
        void Update(Expense expense);
        void Delete(Expense expense);

 
        Task<List<Expense>> GetByUserIdAsync(int userId);
        Task<Expense?> GetByIdAsync(int id);
        Task AddAsync(Expense expense);
        Task UpdateAsync(Expense expense);
        Task DeleteAsync(Expense expense);
    }
}
