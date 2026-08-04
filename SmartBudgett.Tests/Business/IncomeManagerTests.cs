using SmartBudgett.Business.Concrete;
using SmartBudgett.Tests.Builders;
using SmartBudgett.Tests.Helpers;
using SmartBudgett.Tests.Fixtures;

namespace SmartBudgett.Tests.Business
{
    /// <summary>
    /// Income Manager Unit Tests
    /// 
    /// Bu test class'ı IncomeManager'ın tüm method'larını test eder.
    /// Yapı: Arrange (setup) → Act (execute) → Assert (verify)
    /// </summary>
    public class IncomeManagerTests : IDisposable
    {
        private readonly TestFixture _fixture;

        public IncomeManagerTests()
        {
            _fixture = new TestFixture();
        }

        // ==================== ADD ASYNC TESTS ====================

        [Fact]
        public async Task AddAsync_WithValidIncome_ShouldCallRepository()
        {
            // Arrange - Test data hazırla
            var income = new IncomeBuilder()
                .WithAmount(1500)
                .WithDescription("Salary")
                .WithUserId(1)
                .WithCategoryId(1)
                .Build();

            MockHelpers.SetupIncomeAdd(_fixture.IncomeRepositoryMock);

            // Act - Method'u çalıştır
            await _fixture.IncomeService.AddAsync(income);

            // Assert - Repository'nin çağrıldığını kontrol et
            MockHelpers.VerifyAddAsyncCalledOnce(_fixture.IncomeRepositoryMock);
        }

        [Fact]
        public async Task AddAsync_WithNegativeAmount_ShouldThrowException()
        {
            // Arrange
            var income = new IncomeBuilder()
                .WithAmount(-100)  // Negative amount!
                .WithDescription("Invalid Income")
                .Build();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await _fixture.IncomeService.AddAsync(income)
            );
        }

        [Fact]
        public async Task AddAsync_WithEmptyDescription_ShouldThrowException()
        {
            // Arrange
            var income = new IncomeBuilder()
                .WithAmount(1000)
                .WithDescription("")  // Empty!
                .Build();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await _fixture.IncomeService.AddAsync(income)
            );
        }

        [Fact]
        public async Task AddAsync_WithInvalidUserId_ShouldThrowException()
        {
            // Arrange
            var income = new IncomeBuilder()
                .WithAmount(1000)
                .WithDescription("Test")
                .WithUserId(0)  // Invalid!
                .Build();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await _fixture.IncomeService.AddAsync(income)
            );
        }

        [Fact]
        public async Task AddAsync_WithInvalidCategoryId_ShouldThrowException()
        {
            var income = new IncomeBuilder()
                .WithCategoryId(0)
                .Build();

            await Assert.ThrowsAsync<ArgumentException>(
                async () => await _fixture.IncomeService.AddAsync(income)
            );
        }

        // ==================== GET BY ID TESTS ====================

        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnIncome()
        {
            // Arrange
            var income = new IncomeBuilder().WithId(1).Build();
            MockHelpers.SetupIncomeGetById(_fixture.IncomeRepositoryMock, income);

            // Act
            var result = await _fixture.IncomeService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(income.Id, result.Id);
            Assert.Equal(income.Amount, result.Amount);
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ShouldThrowException()
        {
            // Arrange
            var invalidId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await _fixture.IncomeService.GetByIdAsync(invalidId)
            );
        }

        // ==================== GET BY USER TESTS ====================

        [Fact]
        public async Task GetByUserIdAsync_ShouldReturnOnlyUsersIncomes()
        {
            // Arrange
            var incomes = new IncomeBuilder()
                .BuildList(5);  // 5 gelir oluştur
            MockHelpers.SetupIncomeGetByUserId(_fixture.IncomeRepositoryMock, 1, incomes);

            // Act
            var result = await _fixture.IncomeService.GetByUserIdAsync(1);

            // Assert
            AssertHelpers.AssertIncomeCount(result, 5);
        }

        [Fact]
        public async Task GetByUserIdAsync_WithEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            MockHelpers.SetupIncomeGetByUserId(_fixture.IncomeRepositoryMock, 1, new());

            // Act
            var result = await _fixture.IncomeService.GetByUserIdAsync(1);

            // Assert
            Assert.Empty(result);
        }

        // ==================== UPDATE TESTS ====================

        [Fact]
        public async Task UpdateAsync_WithValidIncome_ShouldCallRepository()
        {
            // Arrange
            var income = new IncomeBuilder()
                .WithId(1)
                .WithAmount(2000)
                .WithDescription("Updated Income")
                .Build();

            MockHelpers.SetupIncomeUpdate(_fixture.IncomeRepositoryMock);

            // Act
            await _fixture.IncomeService.UpdateAsync(income);

            // Assert
            MockHelpers.VerifyUpdateAsyncCalledOnce(_fixture.IncomeRepositoryMock);
        }

        [Fact]
        public async Task UpdateAsync_WithNegativeAmount_ShouldThrowException()
        {
            // Arrange
            var income = new IncomeBuilder()
                .WithAmount(-500)
                .Build();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await _fixture.IncomeService.UpdateAsync(income)
            );
        }

        // ==================== DELETE TESTS ====================

        [Fact]
        public async Task DeleteAsync_WithValidIncome_ShouldCallRepository()
        {
            // Arrange
            var income = new IncomeBuilder().Build();
            MockHelpers.SetupIncomeDelete(_fixture.IncomeRepositoryMock);

            // Act
            await _fixture.IncomeService.DeleteAsync(income);

            // Assert
            MockHelpers.VerifyDeleteAsyncCalledOnce(_fixture.IncomeRepositoryMock);
        }

        [Fact]
        public async Task DeleteAsync_WithNullIncome_ShouldThrowException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _fixture.IncomeService.DeleteAsync(null!)
            );
        }

        // ==================== ADDITIONAL TESTS ====================

        [Theory]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(50000)]
        public async Task AddAsync_WithVariousAmounts_ShouldSucceed(decimal amount)
        {
            // Arrange
            var income = new IncomeBuilder()
                .WithAmount(amount)
                .Build();

            MockHelpers.SetupIncomeAdd(_fixture.IncomeRepositoryMock);

            // Act
            await _fixture.IncomeService.AddAsync(income);

            // Assert
            MockHelpers.VerifyAddAsyncCalledOnce(_fixture.IncomeRepositoryMock);
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
}
