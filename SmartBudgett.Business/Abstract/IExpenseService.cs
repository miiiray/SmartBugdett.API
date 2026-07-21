using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBudgett.Entities;
using System.Threading.Tasks;

namespace SmartBudgett.Business.Abstract
{
    public interface IExpenseService
    {
        List<Expense> GetAll();
        Expense GetById(int id);
        void Add(Expense expense);
        void Update(Expense expense);
        void Delete(Expense expense);
    }
}
