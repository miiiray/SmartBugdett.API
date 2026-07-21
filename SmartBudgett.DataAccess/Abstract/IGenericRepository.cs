using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartBudgett.DataAccess.Abstract
{
    public interface IGenericRepository<T>
    {
        List<T> GetAll();

        T GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
