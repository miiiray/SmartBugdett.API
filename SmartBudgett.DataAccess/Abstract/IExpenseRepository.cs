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
        
        List<Expense> GetAll();
        Expense GetById(int id);
        void Add(Expense expense);
        void Update (Expense expense);
        void delete(Expense expense);
    }
}
