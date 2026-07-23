using SmartBudgett.Entities;

namespace SmartBudgett.Tests.Builders
{
    /// <summary>
    /// Test data builder for Income entity
    /// Fluent API pattern for creating test data
    /// </summary>
    public class IncomeBuilder
    {
        private int _id = 1;
        private decimal _amount = 1000;
        private string _description = "Test Income";
        private DateTime _incomeDate = DateTime.Now;
        private int _categoryId = 1;
        private int _userId = 1;
        private DateTime _createdDate = DateTime.Now;
        private DateTime _updatedDate = DateTime.Now;

        public IncomeBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public IncomeBuilder WithAmount(decimal amount)
        {
            _amount = amount;
            return this;
        }

        public IncomeBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public IncomeBuilder WithIncomeDate(DateTime date)
        {
            _incomeDate = date;
            return this;
        }

        public IncomeBuilder WithCategoryId(int categoryId)
        {
            _categoryId = categoryId;
            return this;
        }

        public IncomeBuilder WithUserId(int userId)
        {
            _userId = userId;
            return this;
        }

        public Income Build()
        {
            return new Income
            {
                Id = _id,
                Amount = _amount,
                Description = _description,
                IncomeDate = _incomeDate,
                CategoryId = _categoryId,
                UserId = _userId,
                CreatedDate = _createdDate,
                UpdatedDate = _updatedDate
            };
        }

        public List<Income> BuildList(int count)
        {
            var incomes = new List<Income>();
            for (int i = 0; i < count; i++)
            {
                incomes.Add(new Income
                {
                    Id = _id + i,
                    Amount = _amount + (i * 100),
                    Description = $"{_description} {i + 1}",
                    IncomeDate = _incomeDate.AddDays(i),
                    CategoryId = _categoryId,
                    UserId = _userId,
                    CreatedDate = _createdDate,
                    UpdatedDate = _updatedDate
                });
            }
            return incomes;
        }
    }

    /// <summary>
    /// Test data builder for Expense entity
    /// </summary>
    public class ExpenseBuilder
    {
        private int _id = 1;
        private decimal _amount = 500;
        private string _description = "Test Expense";
        private DateTime _expenseDate = DateTime.Now;
        private int _categoryId = 1;
        private int _userId = 1;
        private DateTime _createdDate = DateTime.Now;
        private DateTime _updatedDate = DateTime.Now;

        public ExpenseBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public ExpenseBuilder WithAmount(decimal amount)
        {
            _amount = amount;
            return this;
        }

        public ExpenseBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public ExpenseBuilder WithExpenseDate(DateTime date)
        {
            _expenseDate = date;
            return this;
        }

        public ExpenseBuilder WithCategoryId(int categoryId)
        {
            _categoryId = categoryId;
            return this;
        }

        public ExpenseBuilder WithUserId(int userId)
        {
            _userId = userId;
            return this;
        }

        public Expense Build()
        {
            return new Expense
            {
                Id = _id,
                Amount = _amount,
                Description = _description,
                ExpenseDate = _expenseDate,
                CategoryId = _categoryId,
                UserId = _userId,
                CreatedDate = _createdDate,
                UpdatedDate = _updatedDate
            };
        }

        public List<Expense> BuildList(int count)
        {
            var expenses = new List<Expense>();
            for (int i = 0; i < count; i++)
            {
                expenses.Add(new Expense
                {
                    Id = _id + i,
                    Amount = _amount + (i * 50),
                    Description = $"{_description} {i + 1}",
                    ExpenseDate = _expenseDate.AddDays(i),
                    CategoryId = _categoryId,
                    UserId = _userId,
                    CreatedDate = _createdDate,
                    UpdatedDate = _updatedDate
                });
            }
            return expenses;
        }
    }

    /// <summary>
    /// Test data builder for Category entity
    /// </summary>
    public class CategoryBuilder
    {
        private int _id = 1;
        private string _name = "Test Category";
        private int _userId = 1;
        private DateTime _createdDate = DateTime.Now;
        private DateTime _updatedDate = DateTime.Now;

        public CategoryBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public CategoryBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CategoryBuilder WithUserId(int userId)
        {
            _userId = userId;
            return this;
        }

        public Category Build()
        {
            return new Category
            {
                Id = _id,
                Name = _name,
                UserId = _userId,
                CreatedDate = _createdDate,
                UpdatedDate = _updatedDate
            };
        }

        public List<Category> BuildList(int count)
        {
            var categories = new List<Category>();
            for (int i = 0; i < count; i++)
            {
                categories.Add(new Category
                {
                    Id = _id + i,
                    Name = $"{_name} {i + 1}",
                    UserId = _userId,
                    CreatedDate = _createdDate,
                    UpdatedDate = _updatedDate
                });
            }
            return categories;
        }
    }

    /// <summary>
    /// Test data builder for User entity
    /// </summary>
    public class UserBuilder
    {
        private int _id = 1;
        private string _firstName = "John";
        private string _lastName = "Doe";
        private string _email = "john@example.com";
        private string _password = "TestPassword123"; // Default plain text, will hash in Build()
        private DateTime _createdDate = DateTime.Now;
        private DateTime _updatedDate = DateTime.Now;

        public UserBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public UserBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public UserBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UserBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public User Build()
        {
            return new User
            {
                Id = _id,
                FirstName = _firstName,
                LastName = _lastName,
                Email = _email,
                Password = _password, // Use plain password for testing - no hashing needed for test data
                CreatedDate = _createdDate,
                UpdatedDate = _updatedDate
            };
        }

        public List<User> BuildList(int count)
        {
            var users = new List<User>();
            for (int i = 0; i < count; i++)
            {
                users.Add(new User
                {
                    Id = _id + i,
                    FirstName = $"{_firstName}{i}",
                    LastName = $"{_lastName}{i}",
                    Email = $"user{i}@example.com",
                    Password = _password,
                    CreatedDate = _createdDate,
                    UpdatedDate = _updatedDate
                });
            }
            return users;
        }
    }
}
