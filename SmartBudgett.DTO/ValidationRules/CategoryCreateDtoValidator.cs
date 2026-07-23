using FluentValidation;
using SmartBudgett.DTO;
using SmartBudgett.DTO.Categories;
namespace SmartBudgett.DTO.ValidationRules
{
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        
        public CategoryCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş geçilemez.")
                .MinimumLength(2).WithMessage("Kategori adı en az 2 karakter olmalıdır.");
        }
        
    }
}
