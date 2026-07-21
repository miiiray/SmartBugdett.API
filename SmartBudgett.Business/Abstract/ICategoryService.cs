using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartBudgett.Entities;

namespace SmartBudgett.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int id);
        void Add(Category category);
        void Update(Category category); 
        void Delete(Category category);
    }
}
