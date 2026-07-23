using Moq;
using SmartBudgett.Business.Abstract;
using SmartBudgett.Business.Concrete;
using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.Entities;

namespace SmartBudgett.Tests.Fixtures
{
    /// <summary>
    /// Base test fixture for setting up mock dependencies
    /// </summary>
    public class TestFixture : IDisposable
    {
        public Mock<IIncomeRepository> IncomeRepositoryMock { get; private set; }
        public Mock<IExpenseRepository> ExpenseRepositoryMock { get; private set; }
        public Mock<ICategoryRepository> CategoryRepositoryMock { get; private set; }
        public Mock<IUserRepository> UserRepositoryMock { get; private set; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; private set; }

        public IIncomeService IncomeService { get; private set; }
        public IExpenseService ExpenseService { get; private set; }
        public ICategoryService CategoryService { get; private set; }
        public IUserService UserService { get; private set; }

        public TestFixture()
        {
            // Initialize Repository Mocks
            IncomeRepositoryMock = new Mock<IIncomeRepository>();
            ExpenseRepositoryMock = new Mock<IExpenseRepository>();
            CategoryRepositoryMock = new Mock<ICategoryRepository>();
            UserRepositoryMock = new Mock<IUserRepository>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();

            // Initialize Services with mocked repositories
            IncomeService = new IncomeManager(IncomeRepositoryMock.Object);
            ExpenseService = new ExpenseManager(ExpenseRepositoryMock.Object);
            CategoryService = new CategoryManager(CategoryRepositoryMock.Object);
            UserService = new UserManager(UserRepositoryMock.Object);
        }

        public void ResetAllMocks()
        {
            IncomeRepositoryMock.Reset();
            ExpenseRepositoryMock.Reset();
            CategoryRepositoryMock.Reset();
            UserRepositoryMock.Reset();
            UnitOfWorkMock.Reset();
        }

        public void Dispose()
        {
            // Dispose mocks properly
            IncomeRepositoryMock?.Reset();
            ExpenseRepositoryMock?.Reset();
            CategoryRepositoryMock?.Reset();
            UserRepositoryMock?.Reset();
            UnitOfWorkMock?.Reset();
        }
    }
}
