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
        }
    }
}