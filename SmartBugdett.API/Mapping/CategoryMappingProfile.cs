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
           
            CreateMap<Category, CategoryResponseDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
        }
    }
}