using Microsoft.EntityFrameworkCore;
using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.DataAccess.Context;

namespace SmartBudgett.DataAccess.Concrete
{
    public class GenericRepository <T> : IGenericRepository<T> where T : class
    {
        private readonly SmartBudgetContext _context;
        private readonly DbSet<T> _dbSet;
        
        public GenericRepository(SmartBudgetContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    

    }
}
