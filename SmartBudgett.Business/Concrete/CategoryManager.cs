using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartBudgett.Business.Abstract;
using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.Entities;

namespace SmartBudgett.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        { 
            _categoryRepository= categoryRepository;
        }
        public void Add(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new Exception("Category name cannot be empty.");
            }
            _categoryRepository.Add(category);
        }
        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }
        public Category GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Invalid category ID.");
            }
            return _categoryRepository.GetById(id);
        }
        public void Update(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new Exception("Category name cannot be empty.");
            }
            _categoryRepository.Update(category);
        }
   
        public void Delete(Category category)
        {
            _categoryRepository.Delete(category);
        }
    }
}  
