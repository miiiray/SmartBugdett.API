using FluentValidation;

namespace SmartBudgett.DTO.ValidationRules
{
    public class ExpenseCreateDtoValidator : AbstractValidator<ExpenseCreateDto>
    {
        public ExpenseCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık alanı boş geçilemez.")
                .MinimumLength(2).WithMessage("Başlık en az 2 karakter olmalıdır.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Harcama tutarı 0'dan büyük olmalıdır.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir kategori seçilmelidir.");
        }
    }
}