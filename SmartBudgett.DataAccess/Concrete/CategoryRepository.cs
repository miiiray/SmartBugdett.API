using Microsoft.EntityFrameworkCore;
using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.DataAccess.Context;
using SmartBudgett.Entities;

namespace SmartBudgett.DataAccess.Concrete
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly SmartBudgetContext _context;

        public CategoryRepository(SmartBudgetContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Category>> GetByUserIdAsync(int userId)
        {
            return _context.Categories
                .AsNoTracking()
                .Where(category => category.UserId == userId)
                .OrderBy(category => category.Name)
                .ToListAsync();
        }
    }
}
