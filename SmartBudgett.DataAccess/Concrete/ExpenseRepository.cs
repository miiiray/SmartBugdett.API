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
        public List<Expense> GetAll()
        {
            return _context.Expenses.ToList();
        }
        public Expense GetById(int id)
        {
            return _context.Expenses.FirstOrDefault(e => e.Id == id);
        }
        public void Add(Expense expense)
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges();
        }
        public void Update(Expense expense)
        {
            _context.Expenses.Update(expense);
            _context.SaveChanges();
        }
        public void Delete(Expense expense)
        {
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
        }
    }
}