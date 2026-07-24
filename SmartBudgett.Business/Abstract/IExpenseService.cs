using SmartBudgett.Entities;

namespace SmartBudgett.Business.Abstract.Services
{
    public interface IExpenseService
    {
        // Sync methods
        List<Expense> GetAll();
        Expense GetById(int id);
        void Add(Expense expense);
        void Update(Expense expense);
        void Delete(Expense expense);

        // Async methods
        Task<List<Expense>> GetAllAsync();
        Task<Expense> GetByIdAsync(int id);
        Task AddAsync(Expense expense);
        Task UpdateAsync(Expense expense);
        Task DeleteAsync(Expense expense);
    }
}
