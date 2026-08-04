using AutoMapper;
using SmartBudgett.DTO;
using SmartBudgett.DTO.Incomes;
using SmartBudgett.Entities;

namespace SmartBudgett.API.Mapping
{
    public class IncomeMappingProfile : Profile
    {
        public IncomeMappingProfile()
        {
            CreateMap<Income, IncomeResponseDto>();
            CreateMap<IncomeCreateDto, Income>();
            CreateMap<IncomeUpdateDto, Income>();
        }
    }
}
