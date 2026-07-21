using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.DataAccess.Context;
using SmartBudgett.Entities;

namespace SmartBudgett.DataAccess.Concrete
{
    public class IncomeRepository : GenericRepository<Income>, IIncomeRepository
    {
        public IncomeRepository(SmartBudgetContext context) : base(context)
        {
        }
    }
}