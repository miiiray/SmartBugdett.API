using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartBudgett.Entities;

namespace SmartBudgett.DataAccess.Abstract
{
    public interface IExpenseRepository : IGenericRepository<Expense>
    {
        Task<List<Expense>> GetByUserIdAsync(int userId);
    }
}
