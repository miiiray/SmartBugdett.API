using SmartBudgett.Entities;

namespace SmartBudgett.Business.Abstract
{
    public interface IIncomeService
    {
        
        Income? GetById(int id);
        void Add(Income income);
        void Update(Income income);
        void Delete(Income income);

    
        Task<List<Income>> GetByUserIdAsync(int userId);
        Task<Income?> GetByIdAsync(int id);
        Task AddAsync(Income income);
        Task UpdateAsync(Income income);
        Task DeleteAsync(Income income);
    }
}
