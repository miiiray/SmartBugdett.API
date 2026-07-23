using Moq;
using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.Entities;

namespace SmartBudgett.Tests.Helpers
{
    /// <summary>
    /// Helper methods for setting up common test scenarios
    /// </summary>
    public static class MockHelpers
    {
        /// <summary>
        /// Setup income repository mock for GetByIdAsync
        /// </summary>
        public static void SetupIncomeGetById(Mock<IIncomeRepository> mock, Income income)
        {
            mock.Setup(r => r.GetByIdAsync(income.Id))
                .ReturnsAsync(income);
        }

        /// <summary>
        /// Setup income repository mock for GetAllAsync
        /// </summary>
        public static void SetupIncomeGetAll(Mock<IIncomeRepository> mock, List<Income> incomes)
        {
            mock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(incomes);
        }

        /// <summary>
        /// Setup income repository mock for AddAsync
        /// </summary>
        public static void SetupIncomeAdd(Mock<IIncomeRepository> mock)
        {
            mock.Setup(r => r.AddAsync(It.IsAny<Income>()))
                .Returns(Task.CompletedTask);
        }

        /// <summary>
        /// Setup income repository mock for UpdateAsync
        /// </summary>
        public static void SetupIncomeUpdate(Mock<IIncomeRepository> mock)
        {
            mock.Setup(r => r.UpdateAsync(It.IsAny<Income>()))
                .Returns(Task.CompletedTask);
        }

        /// <summary>
        /// Setup income repository mock for DeleteAsync
        /// </summary>
        public static void SetupIncomeDelete(Mock<IIncomeRepository> mock)
        {
            mock.Setup(r => r.DeleteAsync(It.IsAny<Income>()))
                .Returns(Task.CompletedTask);
        }

        // ==================== EXPENSE HELPERS ====================

        /// <summary>
        /// Setup expense repository mock for GetByIdAsync
        /// </summary>
        public static void SetupExpenseGetById(Mock<IExpenseRepository> mock, Expense expense)
        {
            mock.Setup(r => r.GetByIdAsync(expense.Id))
                .ReturnsAsync(expense);
        }

        /// <summary>
        /// Setup expense repository mock for GetAllAsync
        /// </summary>
        public static void SetupExpenseGetAll(Mock<IExpenseRepository> mock, List<Expense> expenses)
        {
            mock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(expenses);
        }

        /// <summary>
        /// Setup expense repository mock for AddAsync
        /// </summary>
        public static void SetupExpenseAdd(Mock<IExpenseRepository> mock)
        {
            mock.Setup(r => r.AddAsync(It.IsAny<Expense>()))
                .Returns(Task.CompletedTask);
        }

        /// <summary>
        /// Setup expense repository mock for UpdateAsync
        /// </summary>
        public static void SetupExpenseUpdate(Mock<IExpenseRepository> mock)
        {
            mock.Setup(r => r.UpdateAsync(It.IsAny<Expense>()))
                .Returns(Task.CompletedTask);
        }

        /// <summary>
        /// Setup expense repository mock for DeleteAsync
        /// </summary>
        public static void SetupExpenseDelete(Mock<IExpenseRepository> mock)
        {
            mock.Setup(r => r.DeleteAsync(It.IsAny<Expense>()))
                .Returns(Task.CompletedTask);
        }

        // ==================== CATEGORY HELPERS ====================

        /// <summary>
        /// Setup category repository mock for GetByIdAsync
        /// </summary>
        public static void SetupCategoryGetById(Mock<ICategoryRepository> mock, Category category)
        {
            mock.Setup(r => r.GetByIdAsync(category.Id))
                .ReturnsAsync(category);
        }

        /// <summary>
        /// Setup category repository mock for GetAllAsync
        /// </summary>
        public static void SetupCategoryGetAll(Mock<ICategoryRepository> mock, List<Category> categories)
        {
            mock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(categories);
        }

        /// <summary>
        /// Setup category repository mock for AddAsync
        /// </summary>
        public static void SetupCategoryAdd(Mock<ICategoryRepository> mock)
        {
            mock.Setup(r => r.AddAsync(It.IsAny<Category>()))
                .Returns(Task.CompletedTask);
        }

        // ==================== USER HELPERS ====================

        /// <summary>
        /// Setup user repository mock for GetByIdAsync
        /// </summary>
        public static void SetupUserGetById(Mock<IUserRepository> mock, User user)
        {
            mock.Setup(r => r.GetByIdAsync(user.Id))
                .ReturnsAsync(user);
        }

        /// <summary>
        /// Setup user repository mock for GetAllAsync
        /// </summary>
        public static void SetupUserGetAll(Mock<IUserRepository> mock, List<User> users)
        {
            mock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(users);
        }

        // ==================== VERIFICATION HELPERS ====================

        /// <summary>
        /// Verify that AddAsync was called exactly once with valid data
        /// </summary>
        public static void VerifyAddAsyncCalledOnce(Mock<IIncomeRepository> mock)
        {
            mock.Verify(r => r.AddAsync(It.IsAny<Income>()), Times.Once);
        }

        /// <summary>
        /// Verify that UpdateAsync was called exactly once
        /// </summary>
        public static void VerifyUpdateAsyncCalledOnce(Mock<IIncomeRepository> mock)
        {
            mock.Verify(r => r.UpdateAsync(It.IsAny<Income>()), Times.Once);
        }

        /// <summary>
        /// Verify that DeleteAsync was called exactly once
        /// </summary>
        public static void VerifyDeleteAsyncCalledOnce(Mock<IIncomeRepository> mock)
        {
            mock.Verify(r => r.DeleteAsync(It.IsAny<Income>()), Times.Once);
        }
    }

    /// <summary>
    /// Assertion helpers for common test validations
    /// </summary>
    public static class AssertHelpers
    {
        /// <summary>
        /// Assert that income is valid
        /// </summary>
        public static void AssertValidIncome(Income income)
        {
            Assert.NotNull(income);
            Assert.True(income.Amount > 0, "Income amount must be greater than zero");
            Assert.NotEmpty(income.Description);
            Assert.True(income.UserId > 0);
            Assert.True(income.CategoryId > 0);
        }

        /// <summary>
        /// Assert that expense is valid
        /// </summary>
        public static void AssertValidExpense(Expense expense)
        {
            Assert.NotNull(expense);
            Assert.True(expense.Amount > 0, "Expense amount must be greater than zero");
            Assert.NotEmpty(expense.Description);
            Assert.True(expense.UserId > 0);
        }

        /// <summary>
        /// Assert that category is valid
        /// </summary>
        public static void AssertValidCategory(Category category)
        {
            Assert.NotNull(category);
            Assert.NotEmpty(category.Name);
            Assert.True(category.UserId > 0);
        }

        /// <summary>
        /// Assert that income list has expected count
        /// </summary>
        public static void AssertIncomeCount(List<Income> incomes, int expectedCount)
        {
            Assert.NotNull(incomes);
            Assert.Equal(expectedCount, incomes.Count);
        }
    }
}
