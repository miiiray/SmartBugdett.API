using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.Entities;
using SmartBudgett.DataAccess.Context;

namespace SmartBudgett.DataAccess.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {   
        public UserRepository(SmartBudgetContext context) : base(context)
        {
        }

    }


}
