using Microsoft.EntityFrameworkCore;
using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.DataAccess.Context;
using SmartBudgett.Entities;

namespace SmartBudgett.DataAccess.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SmartBudgetContext _context;

        public UserRepository(SmartBudgetContext context) : base(context)
        {
            _context = context;
        }

        public Task<User?> GetByEmailAsync(string normalizedEmail)
        {
            return _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email.ToLower() == normalizedEmail);
        }
    }
}
