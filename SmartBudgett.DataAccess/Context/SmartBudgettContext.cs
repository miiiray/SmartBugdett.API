using Microsoft.EntityFrameworkCore;
using SmartBudgett.Entities;

namespace SmartBudgett.DataAccess.Context
{
    public class SmartBudgetContext : DbContext
    {
        public SmartBudgetContext(
            DbContextOptions<SmartBudgetContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(SmartBudgetContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}