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

        public void Add(Expense expense)
        {
            if (expense.Amount <= 0)
            {
                throw new Exception("Expense amount must be greater than zero.");
            }

            _expenseRepository.Add(expense);
        }

        public List<Expense> GetAll()
        {
            return _expenseRepository.GetAll();
        }


        public Expense GetById(int id)
        {
            return _expenseRepository.GetById(id);
        }

        public void Update(Expense expense)
        {
            if (expense.Amount <= 0)
            {
                throw new Exception("Expense amount must be greater than zero.");
            }

            _expenseRepository.Update(expense);
        }

        public void Delete(Expense expense)
        {
            _expenseRepository.Delete(expense);
        }
    }
}