using FluentValidation;
using SmartBudgett.DTO.Incomes;

namespace SmartBudgett.DTO.ValidationRules
{
    public class IncomeCreateDtoValidator : AbstractValidator<IncomeCreateDto>
    {
        public IncomeCreateDtoValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Gelir açıklaması boş geçilemez.")
                .MinimumLength(2).WithMessage("Gelir açıklaması en az 2 karakter olmalıdır.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Gelir tutarı 0'dan büyük olmalıdır.");

          

           
        }
    }
}