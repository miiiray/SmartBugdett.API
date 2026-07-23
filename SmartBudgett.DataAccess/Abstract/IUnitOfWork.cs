namespace SmartBudgett.DataAccess.Abstract
{
    /// <summary>
    /// Unit of Work pattern - tüm repository'leri bir yerde yönetir ve SaveChanges işlemini kontrol eder
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ICategoryRepository Categories { get; }
        IIncomeRepository Incomes { get; }
        IExpenseRepository Expenses { get; }

        // SaveChanges işlemleri
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
