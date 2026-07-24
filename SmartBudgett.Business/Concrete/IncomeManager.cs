using SmartBudgett.Entities;
using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.Business.Abstract;

namespace SmartBudgett.Business.Concrete
{
    public class IncomeManager : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeManager(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        // Sync methods
        public Income GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Income id must be greater than zero.");

            return _incomeRepository.GetById(id);
        }

        public void Add(Income income)
        {
            ValidateIncome(income);
            _incomeRepository.Add(income);
        }

        public List<Income> GetAll()
        {
            return _incomeRepository.GetAll();
        }

        public void Update(Income income)
        {
            ValidateIncome(income);
            _incomeRepository.Update(income);
        }

        public void Delete(Income income)
        {
            if (income == null)
                throw new ArgumentNullException(nameof(income));

            _incomeRepository.Delete(income);
        }

        // Async methods
        public async Task<Income> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Income id must be greater than zero.");

            return await _incomeRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Income income)
        {
            ValidateIncome(income);
            await _incomeRepository.AddAsync(income);
        }

        public async Task<List<Income>> GetAllAsync()
        {
            return await _incomeRepository.GetAllAsync();
        }

        public async Task UpdateAsync(Income income)
        {
            ValidateIncome(income);
            await _incomeRepository.UpdateAsync(income);
        }

        public async Task DeleteAsync(Income income)
        {
            if (income == null)
                throw new ArgumentNullException(nameof(income));

            await _incomeRepository.DeleteAsync(income);
        }

        // Helper validation method
        private void ValidateIncome(Income income)
        {
            if (income == null)
                throw new ArgumentNullException(nameof(income));

            if (income.Amount <= 0)
                throw new ArgumentException("Income amount must be greater than zero.");

            if (string.IsNullOrWhiteSpace(income.Description))
                throw new ArgumentException("Income description cannot be empty.");

            if (income.CategoryId <= 0)
                throw new ArgumentException("Income category id must be greater than zero.");

            if (income.UserId <= 0)
                throw new ArgumentException("Income user id must be greater than zero.");
        }
    }
}
