using SmartBudgett.Business.Abstract;
using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.Entities;

namespace SmartBudgett.Business.Concrete
{
    public class ExpenseManager : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseManager(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        // Sync methods
        public void Add(Expense expense)
        {
            ValidateExpense(expense);
            _expenseRepository.Add(expense);
        }

        public List<Expense> GetAll()
        {
            return _expenseRepository.GetAll();
        }

        public Expense GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Expense id must be greater than zero.");

            return _expenseRepository.GetById(id);
        }

        public void Update(Expense expense)
        {
            ValidateExpense(expense);
            _expenseRepository.Update(expense);
        }

        public void Delete(Expense expense)
        {
            if (expense == null)
                throw new ArgumentNullException(nameof(expense));

            _expenseRepository.Delete(expense);
        }

        // Async methods
        public async Task AddAsync(Expense expense)
        {
            ValidateExpense(expense);
            await _expenseRepository.AddAsync(expense);
        }

        public async Task<List<Expense>> GetAllAsync()
        {
            return await _expenseRepository.GetAllAsync();
        }

        public async Task<Expense> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Expense id must be greater than zero.");

            return await _expenseRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Expense expense)
        {
            ValidateExpense(expense);
            await _expenseRepository.UpdateAsync(expense);
        }

        public async Task DeleteAsync(Expense expense)
        {
            if (expense == null)
                throw new ArgumentNullException(nameof(expense));

            await _expenseRepository.DeleteAsync(expense);
        }

        // Helper validation method
        private void ValidateExpense(Expense expense)
        {
            if (expense == null)
                throw new ArgumentNullException(nameof(expense));

            if (expense.Amount <= 0)
                throw new Exception("Expense amount must be greater than zero.");

            if (string.IsNullOrWhiteSpace(expense.Description))
                throw new Exception("Expense description cannot be empty.");

            if (expense.CategoryId <= 0)
                throw new Exception("Expense category id must be greater than zero.");

            if (expense.UserId <= 0)
                throw new Exception("Expense user id must be greater than zero.");
        }
    }
}