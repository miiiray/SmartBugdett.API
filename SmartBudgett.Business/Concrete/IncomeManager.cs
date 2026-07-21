using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartBudgett.Business.Abstract;
using SmartBudgett.Entities;
using SmartBudgett.DataAccess.Abstract;
using Microsoft.Identity.Client;

namespace SmartBudgett.Business.Concrete
{
    public class IncomeManager : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeManager(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }
        public Income GetById(int id)
        {
            return _incomeRepository.GetById(id);
        }

        public void Add(Income income)
        {
            if(income.Amount <= 0)
            {
                throw new ArgumentException("Income amount must be greater than zero.");
            }
        }
        public List<Income> GetAll()
        {
            return _incomeRepository.GetAll();
        }
        public void Update(Income income)
        {
            if (income.Amount <= 0)
            {
                throw new ArgumentException("Income amount must be greater than zero.");
            }
        }
        public void Delete(Income income) 
        { 
            _incomeRepository.Delete(income);
        }
    }
}
