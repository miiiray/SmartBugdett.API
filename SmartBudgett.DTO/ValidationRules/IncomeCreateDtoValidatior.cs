using FluentValidation;

namespace SmartBudgett.DTO.ValidationRules
{
    public class IncomeCreateDtoValidator : AbstractValidator<IncomeCreateDto>
    {
        public IncomeCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Gelir başlığı boş geçilemez.")
                .MinimumLength(2).WithMessage("Gelir başlığı en az 2 karakter olmalıdır.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Gelir tutarı 0'dan büyük olmalıdır.");

            RuleFor(x => x.Source)
                .NotEmpty().WithMessage("Gelir kaynağı boş geçilemez.");
        }
    }
}