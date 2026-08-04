using FluentValidation;
using SmartBudgett.DTO.Auth;

namespace SmartBudgett.DTO.ValidationRules
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("E-posta adresi boş geçilemez.")
                .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi giriniz.")
                .MaximumLength(100).WithMessage("E-posta en fazla 100 karakter olabilir.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Şifre boş geçilemez.")
                .MaximumLength(72).WithMessage("Şifre en fazla 72 karakter olabilir.");
        }
    }
}
