using FluentValidation;
using SmartBudgett.DTO.Incomes;

namespace SmartBudgett.DTO.ValidationRules
{
    public class IncomeUpdateDtoValidator : AbstractValidator<IncomeUpdateDto>
    {
        public IncomeUpdateDtoValidator()
        {
            RuleFor(income => income.Amount)
                .GreaterThan(0).WithMessage("Gelir tutarı 0'dan büyük olmalıdır.");

            RuleFor(income => income.Description)
                .NotEmpty().WithMessage("Gelir açıklaması boş geçilemez.")
                .MaximumLength(500).WithMessage("Gelir açıklaması en fazla 500 karakter olabilir.");

            RuleFor(income => income.IncomeDate)
                .NotEmpty().WithMessage("Gelir tarihi boş geçilemez.");

            RuleFor(income => income.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir kategori seçilmelidir.");
        }
    }
}
