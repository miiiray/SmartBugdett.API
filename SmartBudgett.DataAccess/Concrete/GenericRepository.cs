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
            return _dbSet.AsNoTracking().ToList();
        }

        public T? GetById(int id)
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

        
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
