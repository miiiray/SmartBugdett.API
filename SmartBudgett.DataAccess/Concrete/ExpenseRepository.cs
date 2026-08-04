using Microsoft.EntityFrameworkCore;
using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.DataAccess.Context;
using SmartBudgett.Entities;

namespace SmartBudgett.DataAccess.Concrete
{
    public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
    {
        private readonly SmartBudgetContext _context;

        public ExpenseRepository(SmartBudgetContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Expense>> GetByUserIdAsync(int userId)
        {
            return _context.Expenses
                .AsNoTracking()
                .Where(expense => expense.UserId == userId)
                .OrderByDescending(expense => expense.ExpenseDate)
                .ToListAsync();
        }
    }
}
