using FluentValidation;
using SmartBudgett.DTO.Expenses;

namespace SmartBudgett.DTO.ValidationRules
{
    public class ExpenseCreateDtoValidator
     : AbstractValidator<ExpenseCreateDto>
    {
        public ExpenseCreateDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Harcama miktarı sıfırdan büyük olmalıdır.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Açıklama boş olamaz.")
                .MaximumLength(500)
                .WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(x => x.ExpenseDate)
                .NotEmpty()
                .WithMessage("Harcama tarihi boş olamaz.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("Geçerli bir kategori seçilmelidir.");
        }
    }
}
