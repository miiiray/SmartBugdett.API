using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.DataAccess.Context;

namespace SmartBudgett.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SmartBudgetContext _context;
        private IUserRepository? _userRepository;
        private ICategoryRepository? _categoryRepository;
        private IIncomeRepository? _incomeRepository;
        private IExpenseRepository? _expenseRepository;

        public UnitOfWork(SmartBudgetContext context)
        {
            _context = context;
        }

        public IUserRepository Users
        {
            get { return _userRepository ??= new UserRepository(_context); }
        }

        public ICategoryRepository Categories
        {
            get { return _categoryRepository ??= new CategoryRepository(_context); }
        }

        public IIncomeRepository Incomes
        {
            get { return _incomeRepository ??= new IncomeRepository(_context); }
        }

        public IExpenseRepository Expenses
        {
            get { return _expenseRepository ??= new ExpenseRepository(_context); }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
