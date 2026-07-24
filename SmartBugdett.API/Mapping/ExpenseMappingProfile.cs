using AutoMapper;
using SmartBudgett.DTO.Expenses;
using SmartBudgett.Entities;

public class ExpenseMappingProfile : Profile
{
    public ExpenseMappingProfile()
    {
        CreateMap<ExpenseCreateDto, Expense>();

        CreateMap<ExpenseUpdateDto, Expense>();

        CreateMap<Expense, ExpenseResponseDto>();
    }
}