using FluentValidation;
using SmartBudgett.DTO.Categories;

namespace SmartBudgett.DTO.ValidationRules
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(category => category.Name)
                .NotEmpty().WithMessage("Kategori adı boş geçilemez.")
                .MinimumLength(2).WithMessage("Kategori adı en az 2 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Kategori adı en fazla 100 karakter olabilir.");
        }
    }
}
