using FluentValidation;
using SmartBudgett.DTO.Expenses;

namespace SmartBudgett.DTO.ValidationRules
{
    public class ExpenseUpdateDtoValidator : AbstractValidator<ExpenseUpdateDto>
    {
        public ExpenseUpdateDtoValidator()
        {
            RuleFor(expense => expense.Amount)
                .GreaterThan(0).WithMessage("Harcama miktarı sıfırdan büyük olmalıdır.");

            RuleFor(expense => expense.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz.")
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(expense => expense.ExpenseDate)
                .NotEmpty().WithMessage("Harcama tarihi boş olamaz.");

            RuleFor(expense => expense.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir kategori seçilmelidir.");
        }
    }
}
