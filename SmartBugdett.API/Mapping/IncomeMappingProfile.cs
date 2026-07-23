using AutoMapper;
using SmartBudgett.DTO;
using SmartBudgett.Entities;

namespace SmartBudgett.API.Mapping
{
    public class IncomeMappingProfile : Profile
    {
        public IncomeMappingProfile()
        {
            CreateMap<Income, IncomeResponseDto>().ReverseMap();
            CreateMap<Income, IncomeCreateDto>().ReverseMap();
            CreateMap<Income, IncomeUpdateDto>().ReverseMap();
        }
    }
}