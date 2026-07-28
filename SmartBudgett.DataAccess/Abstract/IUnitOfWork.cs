namespace SmartBudgett.DataAccess.Abstract
{
 
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ICategoryRepository Categories { get; }
        IIncomeRepository Incomes { get; }
        IExpenseRepository Expenses { get; }

      
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
