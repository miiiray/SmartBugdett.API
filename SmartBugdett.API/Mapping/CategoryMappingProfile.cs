using AutoMapper;
using SmartBudgett.DTO;
using SmartBudgett.DTO.Categories;
using SmartBudgett.Entities;

namespace SmartBudgett.API.Mapping
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // ReverseMap() sayesinde hem Category -> DTO hem de DTO -> Category çalışır!
            CreateMap<Category, CategoryResponseDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
        }
    }
}