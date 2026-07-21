using Microsoft.EntityFrameworkCore;
using SmartBudgett.Entities; // Proje adındaki çift 't'ye göre güncellendi

namespace SmartBudgett.DataAccess.Context
{
    public class SmartBudgetContext : DbContext
    {
        // Constructor kısmındaki options ve base tanımlamalarının doğruluğundan emin oluyoruz
        public SmartBudgetContext(DbContextOptions<SmartBudgetContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
    }
}