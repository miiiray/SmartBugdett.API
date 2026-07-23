using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartBudgett.DataAccess.Abstract
{
    public interface IGenericRepository<T>
    {
        // Sync methods (backward compatibility)
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        // Async methods
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
