using AutoMapper;
using SmartBudgett.DTO.Expenses;
using SmartBudgett.Entities;


namespace SmartBudgett.API.Mapping
{
    public class ExpenseMappingProfile : Profile
    {
        public ExpenseMappingProfile()
        {
            CreateMap<ExpenseCreateDto, Expense>();

            CreateMap<ExpenseUpdateDto, Expense>();

            CreateMap<Expense, ExpenseResponseDto>();
        }
    }
}