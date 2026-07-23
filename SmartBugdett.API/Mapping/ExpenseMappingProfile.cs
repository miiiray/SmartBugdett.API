using AutoMapper;
using SmartBudgett.DTO;
using SmartBudgett.DTO.Expenses;
using SmartBudgett.Entities; 

namespace SmartBudgett.API.Profiles
{
   
    public class ExpenseMappingProfile : Profile
    {
        public ExpenseMappingProfile()
        {
           
            CreateMap<ExpenseCreateDto, Expense>();
            CreateMap<Expense, ExpenseResponseDto>();
        }
    }
}