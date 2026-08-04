using Microsoft.EntityFrameworkCore;
using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.DataAccess.Context;
using SmartBudgett.Entities;

namespace SmartBudgett.DataAccess.Concrete
{
    public class IncomeRepository : GenericRepository<Income>, IIncomeRepository
    {
        private readonly SmartBudgetContext _context;

        public IncomeRepository(SmartBudgetContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Income>> GetByUserIdAsync(int userId)
        {
            return _context.Incomes
                .AsNoTracking()
                .Where(income => income.UserId == userId)
                .OrderByDescending(income => income.IncomeDate)
                .ToListAsync();
        }
    }
}
