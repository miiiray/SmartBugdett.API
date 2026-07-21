using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.DataAccess.Context;
using SmartBudgett.Entities;

namespace SmartBudgett.DataAccess.Concrete
{
    public class CategoryRepository : GenericRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(SmartBudgetContext context) : base(context)
        {
        }
    }
}
