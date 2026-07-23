using FluentValidation;
using SmartBudgett.DTO.Expenses;

namespace SmartBudgett.DTO.ValidationRules
{
    public class ExpenseCreateDtoValidator : AbstractValidator<ExpenseCreateDto>
    {
        public ExpenseCreateDtoValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Harcama açıklaması boş geçilemez.")
                .MinimumLength(2).WithMessage("Harcama açıklaması en az 2 karakter olmalıdır.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Harcama tutarı 0'dan büyük olmalıdır.");

            RuleFor(x => x.ExpenseDate)
                .NotEmpty().WithMessage("Harcama tarihi seçilmelidir.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir kategori seçilmelidir.");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("Geçerli bir kullanıcı seçilmelidir.");
        }
    }
}