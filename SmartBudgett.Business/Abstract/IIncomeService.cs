using  SmartBudgett.Entities;
 
namespace SmartBudgett.Business.Abstract
{
    public interface IIncomeService
    {
        List<Income> GetAll();
        Income GetById(int id);
        void Add(Income income);
        void Update(Income income);
        void Delete(Income income);
    }
}